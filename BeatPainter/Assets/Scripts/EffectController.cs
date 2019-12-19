using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour
{
	bool activate = false;
    public GameObject Fx1Cursor;
    public GameObject Fx2Cursor;



    // Start is called before the first frame update
    void Start()
    {
        Fx1Cursor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Fx1Cursor.active == true)
        {
            Fx2Cursor.active = false;
        }
        if(Fx2Cursor.active == true)
        {
            Fx1Cursor.active = false;
        }
    }

	public void SetAtcive()
	{
		if (activate == false)
		{
            Fx1Cursor.SetActive(true);

Debug.Log("Fx1 is Active");
			activate = true;

            Fx2Cursor.SetActive(false);
Debug.Log("Fx2 is not Active");

        }
		else if (activate == false)
		{
            Fx1Cursor.SetActive(false);
Debug.Log("Fx1 is not Active");
            activate = false;
        }
	}
}
