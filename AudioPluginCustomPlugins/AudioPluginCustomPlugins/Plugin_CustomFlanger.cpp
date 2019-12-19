/*
Riccardo Russo
Circular buffer based flanger: a circular buffer is a regular buffer which is kept filled with new samples
over time: when the writing position reaches the end it is reset to 0 and the filling starts from zero.
A circular buffer needs to keep track of:
- The WRITING position: since I need to save previous audio in order to use it in a delay, the writing
position simply tells where the delay buffer needs to be filled with new incoming audio.
- the READING position: this is what creates the actual delay: the reading position is equal to the
delay time converted in samples, this tells the position back in time where I have to pick the
delayed sample.
This is similar to a tape delay, where I have a circular tape which is moving and is overwritten by
the recording head, and the delayed audio is then read by the reading head, in this case though,
what is moving isn't the tape, but the heads.
Then a LFO is created by creating an angle counter which is updated every process call. The value
of the LFO gives the varying delay for the flanger. The LFO frequency is the rate of the flanger,
the amplitude is the depth. Usually a flanger has delay values less than 15 ms, so that is the max
amplitude value.
Further developments: interpolated delay line, feedback (regen)
*/

#include "AudioPluginUtil.h"

namespace CustomFlanger
{
	enum Param
	{
		//this is an enum-->every parameter is associated with a number
		P_LFO_FREQ,
		P_DEPTH,	
		P_MIX,
		P_BYPASS,
		P_NUM		//Quantity of parameters
	};

	int InternalRegisterEffectDefinition(UnityAudioEffectDefinition& aDefinition)
	{	//Defining parameters name, range, description
		int vNumparams = P_NUM;

		//allocating new UnityAudioParameterDefinition
		aDefinition.paramdefs = new UnityAudioParameterDefinition[vNumparams];
		RegisterParameter(aDefinition, "Rate", "", 0.f, 18.f, 5.f, 1.f, 1.f, P_LFO_FREQ, "Flanging frequency");
		RegisterParameter(aDefinition, "Depth", "", 1.f, 10.0f, 10.f, 1.0f, 1.0f, P_DEPTH, "Amount of max delay [ms]");
		RegisterParameter(aDefinition, "Mix amount", "", 0.0f, 1.0f, 0.5f, 1.0f, 1.0f, P_MIX, "Ratio between input and flanged signal");
		RegisterParameter(aDefinition, "Bypass", "", 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, P_BYPASS, " if zero bypasses effect, if != zero effect works");
		return vNumparams;
	}

	struct EffectData
	{	
		struct Data
		{	
			float mParams[P_NUM]; // Parameters (members??), array of floats
			int mSampleCounter;        // LFO Counter
			int mMaxDelayLength;
			float* mDelayBufferLeft;
			float* mDelayBufferRight;
			int mNumChannels;
			int mWritePosition;
			int mReadPosition;
		};

		union
		{	
			Data mData;
			unsigned char pad[(sizeof(Data) + 15) & ~15]; 
		};
	};

	UNITY_AUDIODSP_RESULT UNITY_AUDIODSP_CALLBACK CreateCallback(UnityAudioEffectState* state)
	{	//Called for every plugin instance
		EffectData* vpEffectdata = new EffectData;
		memset(vpEffectdata, 0, sizeof(EffectData));
		state->effectdata = vpEffectdata;
		InitParametersFromDefinitions(InternalRegisterEffectDefinition, vpEffectdata->mData.mParams);

		//Setting member variables
		auto vpData = &state->GetEffectData<EffectData>()->mData;
		vpData->mSampleCounter = 0;
		vpData->mNumChannels = 2; //ASSUMING STEREO
		vpData->mWritePosition = 0;
		
		//setting delay line length, maybe this is unnecessarily long
		const int vcMaxDelayLength = static_cast<int>(
			2 * state->samplerate + 2 * state->dspbuffersize);
		vpData->mMaxDelayLength = vcMaxDelayLength;
		vpData->mDelayBufferLeft = new float[vcMaxDelayLength];
		vpData->mDelayBufferRight = new float[vcMaxDelayLength];
		memset(vpData->mDelayBufferLeft, 0, vcMaxDelayLength * sizeof(float));
		memset(vpData->mDelayBufferRight, 0, vcMaxDelayLength * sizeof(float));
		return UNITY_AUDIODSP_OK;
	}
	UNITY_AUDIODSP_RESULT UNITY_AUDIODSP_CALLBACK ReleaseCallback(UnityAudioEffectState* state)
	{	//Called when plugin instance is released
		delete[] state->GetEffectData<EffectData>()->mData.mDelayBufferLeft;
		delete[] state->GetEffectData<EffectData>()->mData.mDelayBufferRight;
		auto vpData = &state->GetEffectData<EffectData>()->mData;
		delete vpData;
		return UNITY_AUDIODSP_OK;
	}

	UNITY_AUDIODSP_RESULT UNITY_AUDIODSP_CALLBACK SetFloatParameterCallback(UnityAudioEffectState* state, int index, float value)
	{	//Simple parameter callback, gets the value from gui and sets the parameter
		auto vpData = &state->GetEffectData<EffectData>()->mData;
		if (index >= P_NUM)
			return UNITY_AUDIODSP_ERR_UNSUPPORTED;
		vpData->mParams[index] = value; //Sets "index" value of mParams to parameter value
		return UNITY_AUDIODSP_OK;
	}

	UNITY_AUDIODSP_RESULT UNITY_AUDIODSP_CALLBACK GetFloatParameterCallback(UnityAudioEffectState* state, int index, float* value, char *valuestr)
	{	//GetParameter callback, called when gui wants to know parameter value
		EffectData::Data* data = &state->GetEffectData<EffectData>()->mData;
		if (index >= P_NUM)
			return UNITY_AUDIODSP_ERR_UNSUPPORTED;
		if (value != NULL)
			*value = data->mParams[index];
		if (valuestr != NULL)
			valuestr[0] = 0;
		return UNITY_AUDIODSP_OK;
	}

	int UNITY_AUDIODSP_CALLBACK GetFloatBufferCallback(UnityAudioEffectState* state, const char* name, float* buffer, int numsamples)
	{	//No idea what this does
		//Used for displaying analysis data from the runtime.
		return UNITY_AUDIODSP_OK;
	}

	UNITY_AUDIODSP_RESULT UNITY_AUDIODSP_CALLBACK ProcessCallback(UnityAudioEffectState* state, float* inbuffer, float* outbuffer, unsigned int length, int inchannels, int outchannels)
	{
		auto vpData = &state->GetEffectData<EffectData>()->mData;
		auto vSampleRate = state->samplerate;
		auto vLFOFrequency = vpData->mParams[P_LFO_FREQ]; 
		const int vcMaxDelayLength = vpData->mMaxDelayLength;
		auto vpWritePosition = &(vpData->mWritePosition);
		auto vpDelayBufferLeft = vpData->mDelayBufferLeft;
		auto vpDelayBufferRight = vpData->mDelayBufferRight;
		auto vpReadPosition = &(vpData->mReadPosition);
		auto vDryWet = vpData->mParams[P_MIX];
		auto vDepth = static_cast<int>(vpData->mParams[P_DEPTH] * 0.001f * vSampleRate); //to seconds then conversion in samples

		for (unsigned int n = 0; n < length; n++)
		{
			//LFO Operations
			auto vCounter = vpData->mSampleCounter;

			//the LFO can't become negative, otherwise the read position will be set at the opposite side of the buffer!!
			float vLFO = static_cast<int>(vDepth * (1 + sinf(2.f * kPI * vLFOFrequency * vCounter / vSampleRate)) / 2.f);
			++vCounter;
			auto vAngle = 2.f * kPI * vLFOFrequency * vCounter / vSampleRate;
			if (vAngle >= 2.f * kPI)
			{
				vpData->mSampleCounter = 0;
			}
			else
			{
				vpData->mSampleCounter = vCounter;
			}

			//Updating read position
			(*vpReadPosition) = static_cast<int>((*vpWritePosition) - vLFO + vcMaxDelayLength) % vcMaxDelayLength;

			//Actual audio processing
			for (int i = 0; i < outchannels; i++)
			{	//Interleaved buffer parsing
				if (vpData->mParams[P_BYPASS])
				{
					//Circular buffer filling
					if (vcMaxDelayLength > *vpWritePosition)
					{	//strictly major because of 0-indexing (can't write on vpDelayBuffer[vcDelayLength]->overflow)
						if (!i)
						{
							vpDelayBufferLeft[*vpWritePosition] = inbuffer[n * outchannels + i];
							auto vSound = inbuffer[n * outchannels + i] + vDryWet * vpDelayBufferLeft[(*vpReadPosition)];
							vSound = (vSound / 2);
							outbuffer[n * outchannels + i] = vSound;
						}
						else if (i)
						{
							vpDelayBufferRight[*vpWritePosition] = inbuffer[n * outchannels + i];
							auto vSound = inbuffer[n * outchannels + i] + vDryWet * vpDelayBufferRight[(*vpReadPosition)];
							vSound = (vSound / 2);
							outbuffer[n * outchannels + i] = vSound;
						}
					}
				}
				else
				{
					//Circular buffer filling
					if (vcMaxDelayLength > *vpWritePosition)
					{	//strictly major because of 0-indexing (can't write on vpDelayBuffer[vcDelayLength]->overflow)
						if (!i)
						{
							vpDelayBufferLeft[*vpWritePosition] = inbuffer[n * outchannels + i];
							outbuffer[n * outchannels + i] = inbuffer[n * outchannels + i];
						}
						else if (i)
						{
							vpDelayBufferRight[*vpWritePosition] = inbuffer[n * outchannels + i];
							outbuffer[n * outchannels + i] = inbuffer[n * outchannels + i];
						}
					}
				}
			}
			(*vpWritePosition)++;
			if ((*vpWritePosition) >= (vcMaxDelayLength - 1)) (*vpWritePosition) = 0;
		}

		return UNITY_AUDIODSP_OK;
	}
}