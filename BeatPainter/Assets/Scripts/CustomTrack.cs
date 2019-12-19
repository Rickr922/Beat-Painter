using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Vuforia;

/*
  Tracking system. According to the image that has been tracked by Vuforia the
  script does something
*/
public class CustomTrack : MonoBehaviour, ITrackableEventHandler
{
    //Member veriables: Variables that define the object

    //Reference to the GUI
    public GameObject panel;

    public GameObject objectObtainedPanel;

    //Reference to the cursors of the effects 
    public UnityEngine.UI.Image cursorFx1;
    public UnityEngine.UI.Image cursorFx2;

    //Reference to the soundBank
    public SoundBank soundBank;

    string uno = "UI1";
    string due = "UI2";
    string tre = "UI3";
    string quattro = "UI4";

    //Creating two variables to store the x and y coordinates of the cursor of the effects
    Vector2 vectorFx1;
    float x1;
    float y1;

    Vector2 vectorFx2;
    float x2;
    float y2;

    bool firstTime = true;

	#region PROTECTED_MEMBER_VARIABLES

	protected TrackableBehaviour mTrackableBehaviour;
	protected TrackableBehaviour.Status m_PreviousStatus;
	protected TrackableBehaviour.Status m_NewStatus;

	#endregion // PROTECTED_MEMBER_VARIABLES

	#region UNITY_MONOBEHAVIOUR_METHODS

	protected virtual void Start()
	{
        cursorFx1.enabled = false;
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
	{
		if (mTrackableBehaviour)
			mTrackableBehaviour.UnregisterTrackableEventHandler(this);
	}

	#endregion // UNITY_MONOBEHAVIOUR_METHODS

	#region PUBLIC_METHODS

	/// <summary>
	///     Implementation of the ITrackableEventHandler function called when the
	///     tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		m_PreviousStatus = previousStatus;
		m_NewStatus = newStatus;

		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
				 newStatus == TrackableBehaviour.Status.NO_POSE)
		{
			OnTrackingLost();
		}
		else
		{
			OnTrackingLost();
		}
	}

	#endregion // PUBLIC_METHODS

	#region PROTECTED_METHODS

	protected virtual void OnTrackingFound()
	{
        if (mTrackableBehaviour)
		{
			var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
			var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
			var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

			// Enable rendering:
			foreach (var component in rendererComponents)
				component.enabled = true;

			// Enable colliders:
			foreach (var component in colliderComponents)
				component.enabled = true;

			// Enable canvas':
			foreach (var component in canvasComponents)
				component.enabled = true;
        }

        if (!firstTime)
        {
Debug.Log("NOT FIRST TIME ");
            cursorFx1.transform.position = vectorFx1;
Debug.Log("x1 " + x1);
Debug.Log("y1 " + y2);

            cursorFx2.transform.position = vectorFx2;
Debug.Log("x2 " + x1);
Debug.Log("y2 " + y2);
        }
        else
        {
Debug.Log("FIRST TIME ");

            //Getting the coordinates of the cursor
            cursorFx1.transform.position = vectorFx1;
            cursorFx2.transform.position = vectorFx1;
            firstTime = false;
        }

        //Set the associated panel active   
        panel.SetActive(true);

        objectObtainedPanel.SetActive(true);

        //Debug.Log("The panel.name is :" + panel.name);

        //Local variables
        string p = panel.name;
        /*
          Depending on the name of the pannel the program sends a number to the SoundBank class.
          Eventually a sound will play
        */  
        if (p.Equals(uno))
        {
Debug.Log("The panel.name is :" + panel.name);
            soundBank.PlayWhenTargetFound(0);
        }
        else if (p.Equals(due))
        {
Debug.Log("The panel.name is :" + panel.name);
            soundBank.PlayWhenTargetFound(1);
        }
        else if (p.Equals(tre))
        {
Debug.Log("The panel.name is :" + panel.name);
            soundBank.PlayWhenTargetFound(2);
        }
        else if (p.Equals(quattro))
        {
Debug.Log("The panel.name is :" + panel.name);
            soundBank.PlayWhenTargetFound(3);
        }
        else
        {
Debug.Log("Error");
        }

        cursorFx1.enabled = true;
        cursorFx2.enabled = true;
    }

	protected virtual void OnTrackingLost()
	{
		if (mTrackableBehaviour)
		{
			var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
			var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
			var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

			// Disable rendering:
			foreach (var component in rendererComponents)
				component.enabled = false;

			// Disable colliders:
			foreach (var component in colliderComponents)
				component.enabled = false;

			// Disable canvas':
			foreach (var component in canvasComponents)
				component.enabled = false;
		}

        x1 = cursorFx1.transform.position.x;
        y1 = cursorFx1.transform.position.y;
        vectorFx1  = new Vector2(x1, y1);
Debug.Log("x " + x1);
Debug.Log("y " + y1);
        cursorFx2.enabled = false;

        x2 = cursorFx2.transform.position.x;
        y2 = cursorFx2.transform.position.y;
        vectorFx2 = new Vector2(x2, y2);
Debug.Log("x " + x1);
Debug.Log("y " + y2);
        cursorFx1.enabled = false;

        //panel.transform.position = new Vector3(-2000, 512, 0);

        panel.SetActive(false);
        objectObtainedPanel.SetActive(false);

        soundBank.Stop();
    }
	#endregion // PROTECTED_METHODS
}