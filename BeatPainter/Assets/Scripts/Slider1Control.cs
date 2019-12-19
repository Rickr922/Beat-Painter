using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEditor;

public class Slider1Control : MonoBehaviour
{

//public Slider s1;
//public AudioMixer mix;

    // Start is called before the first frame update
    void Start()
    {
     /*
        s1.minValue = 0;
        s1.maxValue = 127;
        s1.wholeNumbers = true;
        s1.value = 0;
     */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnvalueChange(float value)
    {
//Debug.Log("new Vlue " + value);
//mix.SetFloat("PlayState",value);
    }
}
