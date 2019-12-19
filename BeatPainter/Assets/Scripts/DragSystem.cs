using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DragSystem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector2 startPosition;
    Vector2 newPosition;
    float startY;
    public GameObject gameObject;
    public Canvas canvas;
    Vector2 screenBounds;
    public AudioMixer mixer;


    public void OnBeginDrag(PointerEventData eventData) { }
    

    public void OnDrag(PointerEventData eventData)
    {
        newPosition.y = startY;
        newPosition.x = gameObject.transform.position.x;
        //var vWidth = canvas.GetComponent<RectTransform>().rect.width;

        RectTransform rt = (RectTransform)gameObject.transform;
        var objectWidth = rt.rect.width;

        if (Input.mousePosition.x <= Screen.width - (objectWidth / 2) &&
            Input.mousePosition.x >= objectWidth / 2)
        {
            newPosition.x = Input.mousePosition.x;
        }
        gameObject.transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.transform.position = newPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        startY = gameObject.transform.position.y;
        screenBounds = Camera.main.ScreenToWorldPoint(
        new Vector3(
            Screen.width,
            Screen.height,
            Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform rt = (RectTransform)gameObject.transform;
        var objectWidth = rt.rect.width;
        var maxX = Screen.width - (objectWidth / 2);
        var minX = objectWidth / 2;

        if (gameObject.name == "Drum_Colored")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(-1, 1, Mathf.InverseLerp(minX, maxX, x));
            mixer.SetFloat("Panning_Drum", xPar);
        }
        else if(gameObject.name == "Bass_Colored")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(-1, 1, Mathf.InverseLerp(minX, maxX, x));
            mixer.SetFloat("Panning_Bass", xPar);
        }
        else if (gameObject.name == "Synth_Colored")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(-1, 1, Mathf.InverseLerp(minX, maxX, x));
            mixer.SetFloat("Panning_Synth", xPar);
        }
        else if (gameObject.name == "Brass_Colored")
        {
            float x = (int)gameObject.transform.position.x;
            float xPar = Mathf.Lerp(-1, 1, Mathf.InverseLerp(minX, maxX, x));
            mixer.SetFloat("Panning_Brass", xPar);
        }

    }

    public void EnableMovements()
    {
        gameObject.GetComponent<DragSystem>().enabled = true;
    }

    public void DisableMovements()
    {
        gameObject.GetComponent<DragSystem>().enabled = false;
    }
}
