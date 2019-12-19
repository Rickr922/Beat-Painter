using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rayTracker : MonoBehaviour
{

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchPrev;
    private RaycastHit hit;


    void Update()
    {

#if UNITY_EDITOR

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {

            touchPrev = new GameObject[touchList.Count];
            touchList.CopyTo(touchPrev);
            touchList.Clear();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction*10000, Color.green, 10, false);

            if (Physics.Raycast(ray, out hit))
            {

                GameObject recipient = hit.transform.gameObject;
                touchList.Add(recipient);

                if (Input.GetMouseButtonDown(0))
                {
                    recipient.SendMessage("touchBegan", hit.point, SendMessageOptions.DontRequireReceiver);

                }
                if (Input.GetMouseButtonUp(0))
                {
                    recipient.SendMessage("touchEnded", hit.point, SendMessageOptions.DontRequireReceiver);

                }
                if (Input.GetMouseButton(0))
                {
                    recipient.SendMessage("touchStay", hit.point, SendMessageOptions.DontRequireReceiver);

                }
            }

            foreach (GameObject g in touchPrev)
            {
                if (!touchList.Contains(g))
                {
                    g.SendMessage("touchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

#endif

        if (Input.touchCount > 0)
        {


            touchPrev = new GameObject[touchList.Count];
            touchList.CopyTo(touchPrev);
            touchList.Clear();

            foreach (Touch touch in Input.touches)
            {

                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit))
                {

                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);

                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("touchBegan", hit.point, SendMessageOptions.DontRequireReceiver);

                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("touchEnded", hit.point, SendMessageOptions.DontRequireReceiver);

                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        recipient.SendMessage("touchStay", hit.point, SendMessageOptions.DontRequireReceiver);

                    }
                    if (touch.phase == TouchPhase.Canceled)
                    {
                        recipient.SendMessage("touchExit", hit.point, SendMessageOptions.DontRequireReceiver);

                    }
                }
            }

            foreach (GameObject g in touchPrev)
            {
                if (!touchList.Contains(g))
                {
                    g.SendMessage("touchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}