using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{
    //Long term memory that stores the sound to play.
    public AudioFile currentLoadedDrum;
    public AudioFile currentLoadedBass;
    public AudioFile currentLoadedSynth;
    public AudioFile currentLoadedBrass;

    //List of AudioFile objects containing the sounds to play
    public List <AudioFile> mDrum  = new List<AudioFile>();
    public List <AudioFile> mBass  = new List<AudioFile>();
    public List <AudioFile> mSynth = new List<AudioFile>();
    public List <AudioFile> mBrass = new List<AudioFile>();

    //Temporary array of AudioSource objects 
    public AudioSource [] drumTemp  = new AudioSource [4];
    public AudioSource [] bassTemp  = new AudioSource [4];
    public AudioSource [] synthTemp = new AudioSource[4];
    public AudioSource [] brassTemp = new AudioSource[4];

    //Array containing a reference to the 16 interactive buttoms 
    public Transform[] transforms = new Transform[16];

    //Start is called before the first frame update
    void Start()
    {
        /*
          The for loops runs according to the lenght of the elements contained
          in the various arrays. 
        */
        for (int i = 0; i < drumTemp.Length; i ++)
        {
            //The full name of the element contained in the array is stored
            string temp = drumTemp[i].name;

            /*
              The word loop1 is choped and we only get the name of the instrument
              In this case "drum"
            */
            string name = temp.Substring(0, temp.Length - 5);

Debug.Log("The name is: " + name);
            
            /*
              A new AudioFile object (an instance of the AudioFile class) is created having as variable:
              the AudioSource contained at drumTemp[i] and its boolean properties initialise to be false
            */
            AudioFile aF = new AudioFile (drumTemp[i],false, false);

            //The object is then inserted in a List of AudioFile objects
            mDrum.Add(aF);
        }

        for (int i = 0; i < bassTemp.Length; i ++)
        {
            string temp = bassTemp[i].name;
            string name = temp.Substring(0, temp.Length - 5);

Debug.Log("The name is: " + name);

            AudioFile bF = new AudioFile(bassTemp[i], false, false);
            mBass.Add(bF);
        }

        for (int i = 0; i < synthTemp.Length; i++)
        {
            string temp = synthTemp[i].name;
            string name = temp.Substring(0, temp.Length - 5);

Debug.Log("The name is: " + name);

            AudioFile bF = new AudioFile(synthTemp[i], false, false);
            mSynth.Add(bF);
        }

        for (int i = 0; i < brassTemp.Length; i++)
        {
            string temp = brassTemp[i].name;
            string name = temp.Substring(0, temp.Length - 5);

Debug.Log("The name is: " + name);

            AudioFile bF = new AudioFile(brassTemp[i], false, false);
            mBrass.Add(bF);
        }

        /*
          The AudioFile objects are initialised to the null. This will be used to keep track
          of the sound to play
        */

        currentLoadedDrum = new AudioFile(null, false, false);
        currentLoadedBass = new AudioFile(null, false, false);
        currentLoadedSynth = new AudioFile(null, false, false);
        currentLoadedBrass = new AudioFile(null, false, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IdentifyLoop(string aInstrument, int aButtonNumber)
    {
Debug.Log("Instrument " + aInstrument + ". Button " + aButtonNumber);

        if (aInstrument == "Drum")
        {
Debug.Log("It's a drum");
            //AudioSource vCurrentLoop = currentLoadedDrum.mAudioFile;
            //If THERES IS an AudioFile for the currenLoadedDrum 
            if (currentLoadedDrum.mAudioFile != null)
            {
Debug.Log("CurrentLoadDrum is not null");

                //If the player presses the buttom containing the same mAudioFile the sound stops
                if (currentLoadedDrum.mAudioFile == mDrum[aButtonNumber].mAudioFile && currentLoadedDrum.mIsLoaded == true)
                {
Debug.Log("CurrentLoadDrum is the same let's stop it");
                    currentLoadedDrum.mIsPlaying = false;
                    currentLoadedDrum.mIsLoaded = false;
                    currentLoadedDrum.mAudioFile.Stop();
                }
/*                else if (currentLoadedDrum.mAudioFile == mDrum[aButtonNumber].mAudioFile && currentLoadedDrum.mIsPlaying == true)
                {
Debug.Log("CurrentLoadDrum is the same let's stop it");
                    currentLoadedDrum.mIsPlaying = false;
                    currentLoadedDrum.mIsLoaded = false;
                    currentLoadedDrum.mAudioFile.Stop();
                }
*/
                /*If the player presses a button containing a different mAudioFile the program stopes
                  the currentLoadedDrum.mAudioFile and plays the new mAudioFile 
                */
                else if (currentLoadedDrum.mAudioFile != mDrum[aButtonNumber].mAudioFile)
                {
Debug.Log("CurrentLoadDrum is not the same let's stop it and play the new one");
                    //Stop();
                    currentLoadedDrum.mIsPlaying = false;
                    currentLoadedDrum.mIsLoaded = true;
                    currentLoadedDrum.mAudioFile.Stop();

                    currentLoadedDrum = mDrum[aButtonNumber];
                    currentLoadedDrum.mIsPlaying = true;
                    currentLoadedDrum.mIsLoaded = true;
                    Play("Drum");
                }

                //If we want to play the same mAudioFile that has just been stopped 
                else
                {
 Debug.Log("ELSE");
                    currentLoadedDrum = mDrum[aButtonNumber];
                    currentLoadedDrum.mIsPlaying = true;
                    currentLoadedDrum.mIsLoaded = true;
                    Play("Drum");
                }
            }
            //If THERES IS NOT an AudioFile for the currenLoadedDrum 
            else if (currentLoadedDrum.mAudioFile == null)
            {
Debug.Log("CurrentLoadDrum is null let's add something new");
                currentLoadedDrum = mDrum[aButtonNumber];
                currentLoadedDrum.mIsPlaying = true;
                currentLoadedDrum.mIsLoaded = true;
                Play("Drum");
            }
        }
        else if (aInstrument == "Bass")
        {
Debug.Log("It's a Bass");
            //AudioSource vCurrentLoop = currentLoadedDrum.mAudioFile;
            if (currentLoadedBass.mAudioFile != null)
            {
Debug.Log("CurrentLoadBass is not null");
                if (currentLoadedBass.mAudioFile == mBass[aButtonNumber].mAudioFile && currentLoadedBass.mIsLoaded == true)
                {
Debug.Log("CurrentLoadBass is the same let's stop it");
                    currentLoadedBass.mIsPlaying = false;
                    currentLoadedBass.mIsLoaded = false;
                    currentLoadedBass.mAudioFile.Stop();
                }
                else if (currentLoadedBass.mAudioFile == mBass[aButtonNumber].mAudioFile && currentLoadedBass.mIsPlaying == true)
                {
Debug.Log("CurrentLoadDrum is the same let's stop it");
                    currentLoadedBass.mIsPlaying = false;
                    currentLoadedBass.mIsLoaded = false;
                    currentLoadedBass.mAudioFile.Stop();
                }
                else if (currentLoadedBass.mAudioFile != mBass[aButtonNumber].mAudioFile)
                {
Debug.Log("CurrentLoadBass is not the same let's stop it and play the new one");
                    //Stop();
                    currentLoadedBass.mIsPlaying = false;
                    currentLoadedBass.mIsLoaded = true;
                    currentLoadedBass.mAudioFile.Stop();

                    currentLoadedBass = mBass[aButtonNumber];
                    currentLoadedBass.mIsPlaying = true;
                    currentLoadedBass.mIsLoaded = true;
                    Play("Bass");
                }
                else
                {
                    currentLoadedBass = mBass[aButtonNumber];
                    currentLoadedBass.mIsPlaying = true;
                    currentLoadedBass.mIsLoaded = true;
                    Play("Bass");
                }
            }
            else if (currentLoadedBass.mAudioFile == null)
            {
Debug.Log("CurrentLoadBass is null let's add something new");
                currentLoadedBass = mBass[aButtonNumber];
                currentLoadedBass.mIsPlaying = true;
                currentLoadedBass.mIsLoaded = true;
                Play("Bass");
            }
        }
        else if (aInstrument == "Synth")
        {
Debug.Log("It's a Synth");
            //AudioSource vCurrentLoop = currentLoadedDrum.mAudioFile;
            if (currentLoadedSynth.mAudioFile != null)
            {
Debug.Log("CurrentLoadBass is not null");
                if (currentLoadedSynth.mAudioFile == mSynth[aButtonNumber].mAudioFile && currentLoadedSynth.mIsLoaded == true)
                {
Debug.Log("CurrentLoadBass is the same let's stop it");
                    currentLoadedSynth.mIsPlaying = false;
                    currentLoadedSynth.mIsLoaded = false;
                    currentLoadedSynth.mAudioFile.Stop();
                }
                else if (currentLoadedSynth.mAudioFile == mSynth[aButtonNumber].mAudioFile && currentLoadedSynth.mIsPlaying == true)
                {
Debug.Log("currentLoadedSynth is the same let's stop it");
                    currentLoadedSynth.mIsPlaying = false;
                    currentLoadedSynth.mIsLoaded = false;
                    currentLoadedSynth.mAudioFile.Stop();
                }

                else if (currentLoadedSynth.mAudioFile != mSynth[aButtonNumber].mAudioFile)
                {
Debug.Log("currentLoadedSynth is not the same let's stop it and play the new one");
                    //Stop();
                    currentLoadedSynth.mIsPlaying = false;
                    currentLoadedSynth.mIsLoaded = true;
                    currentLoadedSynth.mAudioFile.Stop();

                    currentLoadedSynth = mSynth[aButtonNumber];
                    currentLoadedSynth.mIsPlaying = true;
                    currentLoadedSynth.mIsLoaded = true;
                    Play("Synth");
                }
                else
                {
                    currentLoadedSynth = mSynth[aButtonNumber];
                    currentLoadedSynth.mIsPlaying = true;
                    currentLoadedSynth.mIsLoaded = true;
                    Play("Synth");
                }

            }
            else if (currentLoadedSynth.mAudioFile == null)
            {
    Debug.Log("currentLoadedSynth is null let's add something new");
                currentLoadedSynth = mSynth[aButtonNumber];
                currentLoadedSynth.mIsPlaying = true;
                currentLoadedSynth.mIsLoaded = true;
                Play("Synth");
            }
        }
        else if (aInstrument == "Brass")
        {
Debug.Log("It's a Brass");
            //AudioSource vCurrentLoop = currentLoadedDrum.mAudioFile;
            if (currentLoadedBrass.mAudioFile != null)
            {
Debug.Log("currentLoadedBrass is not null");
                if (currentLoadedBrass.mAudioFile == mBrass[aButtonNumber].mAudioFile && currentLoadedBrass.mIsLoaded == true)
                {
Debug.Log("currentLoadedBrass is the same let's stop it");
                    currentLoadedBrass.mIsPlaying = false;
                    currentLoadedBrass.mIsLoaded = false;
                    currentLoadedBrass.mAudioFile.Stop();
                }
                else if (currentLoadedBrass.mAudioFile == mBrass[aButtonNumber].mAudioFile && currentLoadedBrass.mIsPlaying == true)
                {
Debug.Log("currentLoadedBrass is the same let's stop it");
                    currentLoadedBrass.mIsPlaying = false;
                    currentLoadedBrass.mIsLoaded = false;
                    currentLoadedBrass.mAudioFile.Stop();
                }

                else if (currentLoadedBrass.mAudioFile != mBrass[aButtonNumber].mAudioFile)
                {
Debug.Log("currentLoadedBrass is not the same let's stop it and play the new one");
                    //Stop();
                    currentLoadedBrass.mIsPlaying = false;
                    currentLoadedBrass.mIsLoaded = true;
                    currentLoadedBrass.mAudioFile.Stop();

                    currentLoadedBrass = mBrass[aButtonNumber];
                    currentLoadedBrass.mIsPlaying = true;
                    currentLoadedBrass.mIsLoaded = true;
                    Play("Brass");
                }
                else
                {
                    currentLoadedBrass = mBrass[aButtonNumber];
                    currentLoadedBrass.mIsPlaying = true;
                    currentLoadedBrass.mIsLoaded = true;
                    Play("Brass");
                }

            }
            else if (currentLoadedBrass.mAudioFile == null)
            {
Debug.Log("currentLoadedBrass is null let's add something new");
                currentLoadedBrass = mBrass[aButtonNumber];
                currentLoadedBrass.mIsPlaying = true;
                currentLoadedBrass.mIsLoaded = true;
                Play("Brass");
            }
        }
    }

    public void OnButtonClick(Transform aButton)
    {  

Debug.Log("The name of the button is: " + aButton.tag);

        string vButtonTag = aButton.tag;

Debug.Log(vButtonTag.Substring(2));

        //Parsing the string: taking loop number in vNumb and
        //Instrument in vButtonInstrument according to button tag
        var vNumb = System.Int32.Parse(vButtonTag.Substring(0, 1));
        var vButtonInstrument = vButtonTag.Substring(2);

        IdentifyLoop(vButtonInstrument, vNumb);
    }

    //If an mAudioFile has been selected it will be played according to the target image
    public void PlayWhenTargetFound(int i)
    {

        if (i == 0 && currentLoadedDrum.mIsLoaded == true)
        {
            currentLoadedDrum.mAudioFile.Play();
        }

        else if (i == 1 && currentLoadedBass.mIsLoaded == true)
        {
            currentLoadedBass.mAudioFile.Play();
        }

        else if (i == 2 && currentLoadedSynth.mIsLoaded == true)
        {
            currentLoadedSynth.mAudioFile.Play();
        }

        else if (i == 3 && currentLoadedBrass.mIsLoaded == true)
        {
            currentLoadedBrass.mAudioFile.Play();
        }

        //If nothing was selected the program returns
        else
        {
            return;
        }
    }

    public void Play(string aInstrument)
    {
        if(aInstrument == "Drum")
        {
            if (currentLoadedDrum != null)
            {
Debug.Log("currentLoadedDrum is not equal to null so let's play  it");
                currentLoadedDrum.mIsPlaying = true;
                currentLoadedDrum.mIsLoaded = true;
                currentLoadedDrum.mAudioFile.Play();
            }
        }
        else if (aInstrument == "Bass")
        {
            if (currentLoadedBass != null)
            {
                currentLoadedBass.mIsPlaying = true;
                currentLoadedBass.mIsLoaded = true;
                currentLoadedBass.mAudioFile.Play();
            }
        }
        else if (aInstrument == "Synth")
        {
            if (currentLoadedSynth != null)
            {
                currentLoadedSynth.mIsPlaying = true;
                currentLoadedSynth.mIsLoaded = true;
                currentLoadedSynth.mAudioFile.Play();
            }
        }
        else if (aInstrument == "Brass")
        {
            if (currentLoadedBrass != null)
            {
                currentLoadedBrass.mIsPlaying = true;
                currentLoadedBrass.mIsLoaded = true;
                currentLoadedBrass.mAudioFile.Play();
            }
        }
    }

    public void Play()
    {
        if (currentLoadedDrum != null & currentLoadedDrum.mIsLoaded == true)
        {
            Debug.Log("currentLoadedDrum is not equal to null so let's play  it");
            currentLoadedDrum.mIsPlaying = true;
            currentLoadedDrum.mIsLoaded = true;
            currentLoadedDrum.mAudioFile.Play();
        }
        if (currentLoadedBass != null & currentLoadedBass.mIsLoaded == true)
        {
            currentLoadedBass.mIsPlaying = true;
            currentLoadedBass.mIsLoaded = true;
            currentLoadedBass.mAudioFile.Play();
        }
        if (currentLoadedSynth != null & currentLoadedSynth.mIsLoaded == true)
        {
            currentLoadedSynth.mIsPlaying = true;
            currentLoadedSynth.mIsLoaded = true;
            currentLoadedSynth.mAudioFile.Play();
        }
        if (currentLoadedBrass != null & currentLoadedBrass.mIsLoaded == true)
        {
            currentLoadedBrass.mIsPlaying = true;
            currentLoadedBrass.mIsLoaded = true;
            currentLoadedBrass.mAudioFile.Play();
        }
    }

    public void Stop()
    {
        if (currentLoadedDrum.mAudioFile != null)
        {
Debug.Log("Let's stop the Drum");
            currentLoadedDrum.mAudioFile.Stop();
            currentLoadedDrum.mIsPlaying = false;
        }

        if (currentLoadedBass.mAudioFile != null)
        {
Debug.Log("Let's stop the Bass");
            currentLoadedBass.mAudioFile.Stop();
            currentLoadedBass.mIsPlaying = false;
        }

        if (currentLoadedSynth.mAudioFile != null)
        {
Debug.Log("Let's stop the Synth");
            currentLoadedSynth.mAudioFile.Stop();
            currentLoadedSynth.mIsPlaying = false;
        }

        if (currentLoadedBrass.mAudioFile != null)
        {
Debug.Log("Let's stop the Brass");
            currentLoadedBrass.mAudioFile.Stop();
            currentLoadedBrass.mIsPlaying = false;
        }
    }
}

public class AudioFile
{
    public AudioSource mAudioFile;
    public bool mIsPlaying;
    public bool mIsLoaded;

    public AudioFile(AudioSource aC, bool mC, bool mL)
    {
        this.mAudioFile = aC;
        this.mIsPlaying = mC;
        this.mIsLoaded = mL;
    }
}