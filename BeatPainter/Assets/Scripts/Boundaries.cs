using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boundaries : MonoBehaviour
{
    public RectTransform central_Pannel;
    Vector2 vr;

    public RectTransform leftP;
    Vector2 lV;


    float central_Pannel_height;
    float central_Pannel_width;

    float left_Pannel_height;
    float left_Pannel_width;


    // Start is called before the first frame update
    void Start()
    { 
        GetSize(central_Pannel, leftP);
        
    }

    // Update is called once per frame
    void Update()
    {
        float xMin = central_Pannel_width - ((central_Pannel_width / 6) * 5);
        float xMax = central_Pannel_width - xMin;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, left_Pannel_width + 30, central_Pannel_width - left_Pannel_width),
        Mathf.Clamp(transform.position.y, 0, left_Pannel_height), transform.position.z);

    }

    void GetSize(RectTransform c, RectTransform l)
    {
        RectTransform ct = c.GetComponent<RectTransform>();
        central_Pannel_height = ct.rect.height;
        central_Pannel_width = ct.rect.width;

        RectTransform lt = l.GetComponent<RectTransform>();
        left_Pannel_height = lt.rect.height;
Debug.Log("The height is " + left_Pannel_height);
        left_Pannel_width = lt.rect.width;
Debug.Log("The width is " + left_Pannel_width);
    }
}
