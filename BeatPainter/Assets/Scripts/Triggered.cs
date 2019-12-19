using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public class Triggered : MonoBehaviour
{

    public Image img;
    public Transform t;
    float distance = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed");

            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Debug.Log("Mouse Position: " + mousePosition);


        }

    }

    public void OnMouseDown()
    {
        /*img.GetComponent<Image>().color = Color.black;
        Debug.Log("Ciao");
        
        Vector3 objPosition = Camera.main.ScreenToViewportPoint(mousePosition);
        transform.position = objPosition; */
    }
}
