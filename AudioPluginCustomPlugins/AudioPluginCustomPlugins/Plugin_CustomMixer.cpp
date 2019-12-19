/*Riccardo Russo*/

#include "AudioPluginUtil.h"

namespace CustomMixer
{
	enum Param
	{
		//this is an enum-->every parameter is associated with a number
		P_PAN,		//Panning
		P_LEVEL,	//Track Level
		P_NUM		
	};

	int InternalRegisterEffectDefinition(UnityAudioEffectDefinition& aDefinition)
	{	//Defining parameters name, range, description
		int vNumparams = P_NUM;

		//allocating new UnityAudioParameterDefinition
		aDefinition.paramdefs = new UnityAudioParameterDefinition[vNumparams];
		RegisterParameter(aDefinition, "Panning", "", -1.f, 1.f, 0.f, 1.f, 1.f, P_PAN, "Track Panning");
		RegisterParameter(aDefinition, "Level", "", 0.0f, 1.0f, 0.5f, 1.0f, 1.0f, P_LEVEL, "Track Level");
		return vNumparams;
	}

	struct EffectData
	{
		struct Data
		{
			float mParams[P_NUM]; // Parameters (members??), array of floats
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

		return UNITY_AUDIODSP_OK;
	}
	UNITY_AUDIODSP_RESULT UNITY_AUDIODSP_CALLBACK ReleaseCallback(UnityAudioEffectState* state)
	{	//Called when plugin instance is released
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
		auto vPanning = (vpData->mParams[P_PAN] + 1) / 2;
		auto vLevel = vpData->mParams[P_LEVEL];

		for (unsigned int n = 0; n < length; n++)
		{
			//Actual audio processing
			for (int i = 0; i < outchannels; i++)
			{	//Interleaved buffer parsing
				if (i)
				{
					outbuffer[n * outchannels + i] = inbuffer[n * outchannels + i] * vLevel * vPanning;
				}
				else if (!i)
				{
					outbuffer[n * outchannels + i] = inbuffer[n * outchannels + i] * vLevel * (1 - vPanning);
				}
			}
		}

		return UNITY_AUDIODSP_OK;
	}
}