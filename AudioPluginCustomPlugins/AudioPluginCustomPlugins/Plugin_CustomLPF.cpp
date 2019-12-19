/*
Riccardo Russo
Filter based on BiquadFilter class provided by Unity SDK
*/

#include "AudioPluginUtil.h"

namespace CustomLowPass
{
	enum Param
	{
		//this is an enum-->every parameter is associated with a number
		P_CUTOFF, //=0
		P_Q,		
		P_BYPASS,
		P_NUM		//Quantity of parameters
	};

	int InternalRegisterEffectDefinition(UnityAudioEffectDefinition& aDefinition)
	{	//Defining parameters name, range, description
		int vNumparams = P_NUM;

		//allocating new UnityAudioParameterDefinition
		aDefinition.paramdefs = new UnityAudioParameterDefinition[vNumparams];
		RegisterParameter(aDefinition, "Mod Frequency", "", 20.f, 20000.f, 3000.f, 1.f, 3.f, P_CUTOFF, "Modulator Frequency");
		RegisterParameter(aDefinition, "Mix amount", "", 0.1f, 20.0f, 0.5f, 1.0f, 1.0f, P_Q, "Ratio between input and modulated signal");
		RegisterParameter(aDefinition, "Bypass", "", 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, P_BYPASS, " if zero bypasses effect, if != zero effect works");
		return vNumparams;
	}

	struct EffectData
	{
		struct Data
		{
			float mParams[P_NUM]; // Parameters (members??), array of floats
			BiquadFilter* mpLPFFilter;
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
		vpData->mpLPFFilter = new BiquadFilter();
		vpData->mpLPFFilter->SetupLowpass(5000, state->samplerate, 0.5);

		return UNITY_AUDIODSP_OK;
	}
	UNITY_AUDIODSP_RESULT UNITY_AUDIODSP_CALLBACK ReleaseCallback(UnityAudioEffectState* state)
	{	//Called when plugin instance is released
		auto vpData = &state->GetEffectData<EffectData>()->mData;
		delete(vpData->mpLPFFilter);
		delete vpData;
		return UNITY_AUDIODSP_OK;
	}

	UNITY_AUDIODSP_RESULT UNITY_AUDIODSP_CALLBACK SetFloatParameterCallback(UnityAudioEffectState* state, int index, float value)
	{	//Simple parameter callback, gets the value from gui and sets the parameter
		auto vpData = &state->GetEffectData<EffectData>()->mData;
		if (index >= P_NUM)
			return UNITY_AUDIODSP_ERR_UNSUPPORTED;

		vpData->mParams[index] = value; //Sets "index" value of mParams to parameter value
		if ((index == P_CUTOFF) || (index == P_Q))
		{
			vpData->mpLPFFilter->SetupLowpass(vpData->mParams[P_CUTOFF], state->samplerate, vpData->mParams[P_Q]);
		}
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
		//auto vLFOFrequency = vpData->mParams[P_LFO_FREQ];
		//auto vDryWet = vpData->mParams[P_MIX];
		auto vpLPF = vpData->mpLPFFilter;

		for (unsigned int n = 0; n < length; n++)
		{
			//Actual audio processing
			for (int i = 0; i < outchannels; i++)
			{	//Interleaved buffer parsing
				if (vpData->mParams[P_BYPASS])
				{
					outbuffer[n * outchannels + i] = vpLPF->Process(inbuffer[n * outchannels + i]);
				}
				else
				{
					outbuffer[n * outchannels + i] = inbuffer[n * outchannels + i];
				}
			}
		}

		return UNITY_AUDIODSP_OK;
	}
}