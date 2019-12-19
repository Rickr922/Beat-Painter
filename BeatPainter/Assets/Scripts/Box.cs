using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private float startPosx;
    private float startPosy;
    private bool isHeld = false;

    

    // Update is called once per frame
    void Update()
    {
        if(isHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToViewportPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
        }

//------Testing

        Vector3 mP;
        mP = Input.mousePosition;
        mP = Camera.main.ScreenToViewportPoint(mP);

    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            isHeld = true;
        }
        
    }

    private void OnMouseUp()
    {
        isHeld = false;
    }
}
