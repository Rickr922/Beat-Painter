using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EffectDraSystem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    //public static GameObject itemBeignDragged;
    //Vector3 startPosition;

    public GameObject gameObject;

    public Canvas xBounding;
    public Canvas yBounding;
    public Rect xBoundingTransform;
    public Rect yBoundingTransform;
    public AudioMixer mixer;

    Vector3 newPosition;

    public void OnBeginDrag(PointerEventData eventData){ }

    public void OnDrag(PointerEventData eventData)
    {

        if(Input.mousePosition.x >= xBoundingTransform.xMax &&
            Input.mousePosition.x <= Screen.width - xBoundingTransform.xMax &&
            Input.mousePosition.y >= yBoundingTransform.yMax &&
            Input.mousePosition.y <= Screen.height - yBoundingTransform.yMax)
        {
            newPosition.x = Input.mousePosition.x;
            newPosition.y = Input.mousePosition.y;
            gameObject.transform.position = newPosition;
        }
        else if(Input.mousePosition.y >= yBoundingTransform.yMax &&
            Input.mousePosition.y <= Screen.height - yBoundingTransform.yMax &&
            (Input.mousePosition.x < xBoundingTransform.xMax ||
            Input.mousePosition.x > Screen.width - xBoundingTransform.xMax))
        {
            newPosition.y = Input.mousePosition.y;
            gameObject.transform.position = newPosition;
        }
        else if(Input.mousePosition.x >= xBoundingTransform.xMax &&
            Input.mousePosition.x <= Screen.width - xBoundingTransform.xMax &&
            (Input.mousePosition.y < yBoundingTransform.yMax ||
            Input.mousePosition.y > Screen.height - yBoundingTransform.yMax))
        {
            newPosition.x = Input.mousePosition.x;
            gameObject.transform.position = newPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
//itemBeignDragged = null;
        transform.position = newPosition;

    }

    // Start is called before the first frame update
    void Start()
    {
        var vXBounds = xBounding.GetComponent<RectTransform>();
        var vYBottomBounds = yBounding.GetComponent<RectTransform>();

        xBoundingTransform = RectTransformUtility.PixelAdjustRect(vXBounds, xBounding);
        yBoundingTransform = RectTransformUtility.PixelAdjustRect(vYBottomBounds, yBounding);
    }

    // Update is called once per frame
    void Update()
    {
        var xMin = xBoundingTransform.xMax;
        var xMax = Screen.width - xBoundingTransform.xMax;
        var yMin = yBoundingTransform.yMax;
        var yMax = Screen.height - yBoundingTransform.yMax;

        if(gameObject.name == "Fx1Cursor_Drum")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(20f, 20000f, Mathf.InverseLerp(xMin, xMax, x));
            //var xLog = 20 * Mathf.Pow((20000 / 20), (xPar + xMin) / (xMax + xMin));
            mixer.SetFloat("LowPassCutOff_Drum", xPar);

            float y = (int)gameObject.transform.position.y;
            float yPar = Mathf.Lerp(0.1f, 20f, Mathf.InverseLerp(yMin + (yMax/2), yMax, y));
            mixer.SetFloat("LowPassQ_Drum", yPar);
        }
        else if(gameObject.name == "Fx2Cursor_Drum")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(0f, 18f, Mathf.InverseLerp(xMin, xMax, x));
            mixer.SetFloat("FlangerRate_Drum", xPar);

            float y = (int)gameObject.transform.position.y;
            float yPar = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(yMin, yMax, y));
            mixer.SetFloat("FlangerAmount_Drum", yPar);
        }
        else if(gameObject.name == "Fx1Cursor_Bass")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(20f, 20000f, Mathf.InverseLerp(xMin, xMax, x));
            mixer.SetFloat("LowPassCutOff_Bass", xPar);

            float y = (int)gameObject.transform.position.y;
            float yPar = Mathf.Lerp(0.1f, 20f, Mathf.InverseLerp(yMin + (yMax / 2), yMax, y));
            mixer.SetFloat("LowPassQ_Bass", yPar);
        }
        else if(gameObject.name == "Fx2Cursor_Bass")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(1f, 3000f, Mathf.InverseLerp(xMin, xMax, x));
            mixer.SetFloat("RingModFreq_Bass", xPar);

            float y = (int)gameObject.transform.position.y;
            float yPar = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(yMin, yMax, y));
            mixer.SetFloat("RingModAmount_Bass", yPar);
        }
        else if(gameObject.name == "Fx1Cursor_Synth")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(20f, 20000f, Mathf.InverseLerp(xMin, xMax, x));
            mixer.SetFloat("HiPassCutOff_Synth", xPar);

            float y = (int)gameObject.transform.position.y;
            float yPar = Mathf.Lerp(0.1f, 20f, Mathf.InverseLerp(yMin + (yMax / 2), yMax, y));
            mixer.SetFloat("HiPassQ_Synth", yPar);
        }
        else if(gameObject.name == "Fx2Cursor_Synth")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(0f, 18f, Mathf.InverseLerp(xMin, xMax, x));
            mixer.SetFloat("FlangerRate_Synth", xPar);

            float y = (int)gameObject.transform.position.y;
            float yPar = Mathf.Lerp(0.1f, 1f, Mathf.InverseLerp(yMin, yMax, y));
            mixer.SetFloat("FlangerAmount_Synth", yPar);
        }
        else if(gameObject.name == "Fx1Cursor_Brass")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(0f, 44100f, Mathf.InverseLerp(xMin, xMax, x));
            mixer.SetFloat("DelayTime_Brass", xPar);

            float y = (int)gameObject.transform.position.y;
            float yPar = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(yMin, yMax, y));
            mixer.SetFloat("DelayMixAmount_Brass", yPar);
        }
        else if(gameObject.name == "Fx2Cursor_Brass")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(1f, 3000f, Mathf.InverseLerp(xMin, xMax, x));
            mixer.SetFloat("RingModFreq_Brass", xPar);

            float y = (int)gameObject.transform.position.y;
            float yPar = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(yMin, yMax, y));
            mixer.SetFloat("RingModAmount_Brass", yPar);
        }
    }
}
