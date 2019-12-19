using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
//SENDS VALUES FOR THE EFFECTS 

public class UiManager : MonoBehaviour
{
    public GameObject Fx1Cursor;
    public GameObject Fx2Cursor;

    public GameObject Fx1Controller;
    public GameObject Fx2Controller;

    public GameObject fx1Controller_White;
    public GameObject fx1Controller_Colored;

    public GameObject fx2Controller_White;
    public GameObject fx2Controller_Colored;


    private bool Fx1CursorisActive = false;
    private bool Fx2CursorisActive = false;

    public AudioMixer mixer;

    void Start()
    {
        Fx1Cursor.SetActive(false);
        Fx2Cursor.SetActive(false);
    }   

    // Update is called once per frame
    // CONVERT THE INPUT X AND MAPS  -- BELOW, 0 AND 300 ARE MAPPED TO 1000 TO 22000

    void Update()
    {


    }

    public void setFX1Active()
    {
        if (Fx1CursorisActive == false)
        {
            if(Fx2CursorisActive == true)
            {
                Fx2Cursor.SetActive(false);
                Fx2CursorisActive = false;
            }
Debug.Log("Fx1 Active");

            if(Fx1Controller.name == "Fx1ControllerDrum")
            {
                mixer.SetFloat("LoPassByPass_Drum", 1f);
            }
            else if(Fx1Controller.name == "Fx1ControllerBass")
            {
                mixer.SetFloat("LoPassByPass_Bass", 1);
            }
            else if(Fx1Controller.name == "Fx1ControllerSynth")
            {
                mixer.SetFloat("HiPassByPass_Synth", 1);
            }
            else if(Fx1Controller.name == "Fx1ControllerBrass")
            {
                mixer.SetFloat("DelayByPass_Brass", 1);
            }

            Fx1Cursor.SetActive(true);
            Fx1CursorisActive = true;
            fx1Controller_White.SetActive(false);
            fx1Controller_Colored.SetActive(true);
        }
        else
        {
Debug.Log("Fx1 DeActive");

            if (Fx1Controller.name == "Fx1ControllerDrum")
            {
                mixer.SetFloat("LoPassByPass_Drum", 0);
            }
            else if (Fx1Controller.name == "Fx1ControllerBass")
            {
                mixer.SetFloat("LoPassByPass_Bass", 0);
            }
            else if (Fx1Controller.name == "Fx1ControllerSynth")
            {
                mixer.SetFloat("HiPassByPass_Synth", 0);
            }
            else if (Fx1Controller.name == "Fx1ControllerBrass")
            {
                mixer.SetFloat("DelayByPass_Brass", 0);
            }

            Fx1Cursor.SetActive(false);
            Fx1CursorisActive = false;
            fx1Controller_White.SetActive(true);
            fx1Controller_Colored.SetActive(false);
        }
    }

    public void setFX2Active()
    {
        if (Fx2CursorisActive == false)
        {
            if (Fx1CursorisActive == true)
            {
                Fx1Cursor.SetActive(false);
                Fx1CursorisActive = false;
            }


Debug.Log("Fx2 Active");
            if (Fx2Controller.name == "Fx2ControllerDrum")
            {
                mixer.SetFloat("FlangerByPass_Drum", 1);
            }
            else if (Fx2Controller.name == "Fx2ControllerBass")
            {
                mixer.SetFloat("RingModByPass_Bass", 1);
            }
            else if (Fx2Controller.name == "Fx2ControllerSynth")
            {
                mixer.SetFloat("FlangerByPass_Synth", 1);
            }
            else if (Fx2Controller.name == "Fx2ControllerBrass")
            {
                mixer.SetFloat("RingModByPass_Brass", 1);
            }

            Fx2Cursor.SetActive(true);
            Fx2CursorisActive = true;
            fx2Controller_White.SetActive(false);
            fx2Controller_Colored.SetActive(true);
        }
        else
        {
Debug.Log("Fx2 DeActive");

            if (Fx2Controller.name == "Fx2ControllerDrum")
            {
                mixer.SetFloat("FlangerByPass_Drum", 0);
            }
            else if (Fx2Controller.name == "Fx2ControllerBass")
            {
                mixer.SetFloat("RingModByPass_Bass", 0);
            }
            else if (Fx2Controller.name == "Fx2ControllerSynth")
            {
                mixer.SetFloat("FlangerByPass_Synth", 0);
            }
            else if (Fx2Controller.name == "Fx2ControllerBrass")
            {
                mixer.SetFloat("RingModByPass_Brass", 0);
            }

            Fx2Cursor.SetActive(false);
            Fx2CursorisActive = false;
            fx2Controller_White.SetActive(true);
            fx2Controller_Colored.SetActive(false);
        }
    }
}
