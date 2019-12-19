using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drug : MonoBehaviour
{
    float distance = 10;

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Debug.Log(mousePosition);
        Vector3 objPosition = Camera.main.ScreenToViewportPoint(mousePosition);
        transform.position = objPosition;
    }
}
