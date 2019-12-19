using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
 //   private Transform cursorPosition;
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    private bool isHeld = false;
    private float x, y;

    private void Update()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        this.gameObject.transform.localPosition = new Vector3(mousePos.x,mousePos.y,0);
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }
 
    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x - x, mousePosition.y - y);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;

            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            x = mousePosition.x - this.transform.localPosition.x;
            y = mousePosition.y - this.transform.localPosition.y;

            isHeld = true;
        }

        Debug.Log(mousePosition);
    }

    private void OnMouseUp()
    {
        isHeld = false;

    }

}
