using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixerManager : MonoBehaviour
{

    public SoundBank soundBank;

    public Image[] temp_bwImages;
    public Image[] temp_coloredImages;

    List<ImageTarg> bw_images = new List<ImageTarg>();
    List<ImageTarg> colored_images = new List<ImageTarg>();

    int l;

    bool notYetStart = true;

    public CanvasManager canvasManager;
    public DragSystem[] dragSystems;

    List<ImageTarg> objects = new List<ImageTarg>();
    List<Vector2> targets = new List<Vector2>();

    Hashtable originalPos = new Hashtable();

    int size;
        
    bool firstTime = true;
    bool firstUpdate = true;

    double originalX;
    double originalY;

    float speed = 1000.0f;
    float step;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < temp_bwImages.Length; i++)
        {
            ImageTarg icon = new ImageTarg(true, temp_bwImages[i]);
            bw_images.Add(icon);
        }
        for (int i = 0; i < temp_bwImages.Length; i++)
        {
            ImageTarg icon = new ImageTarg(false, temp_coloredImages[i]);
            colored_images.Add(icon);
        }
    }

    // Update is called once per frame
    void Update()
    {   /*
        for(int i = 0; i < size; i ++)
        {
            if (objects[i] != null)
            {
                    if (objects[i].isActived == true)
                    {
                        Debug.Log("The drum is active");
                        objects[i].image.transform.position = Vector3.MoveTowards(objects[i].image.transform.position, targets[i], step);
                    }
                    else
                    {
                        Debug.Log("The drum is not active");
                    }
            }
        }
        */
    }

    public void OnButtonClick(Transform aButton)
    {
        if (notYetStart == true )
        {
            Start();
            notYetStart = false;
        }
        string id = aButton.name;
        l = 0;

        switch (id)
        {
            case "Drum":

                Debug.Log("Mixer Manager CASE: Drum");

                if (soundBank.currentLoadedDrum.mIsLoaded)
                {
                    bw_images[l].image.gameObject.SetActive(false);
                    bw_images[l].isActived = false;
                    colored_images[l].image.gameObject.SetActive(true);
                    colored_images[l].isActived = true;
                }
                else
                {
                    colored_images[l].image.gameObject.SetActive(false);
                    colored_images[l].isActived = false;
                    bw_images[l].image.gameObject.SetActive(true);
                    bw_images[l].isActived = true;
                }

                // Debug.Log("Case1");
                break;

            case "Bass":

                Debug.Log("CASE: Bass");

                l = 1;

                if (soundBank.currentLoadedBass.mIsLoaded)
                {
                    bw_images[l].image.gameObject.SetActive(false);
                    bw_images[l].isActived = false;
                    colored_images[l].image.gameObject.SetActive(true);
                    colored_images[l].isActived = true;
                }
                else
                {
                    colored_images[l].image.gameObject.SetActive(false);
                    colored_images[l].isActived = false;
                    bw_images[l].image.gameObject.SetActive(true);
                    bw_images[l].isActived = true;
                }


                break;

            case "Synth":

                Debug.Log("CASE: Synth");

                l = 2;

                if (soundBank.currentLoadedSynth.mIsLoaded)
                {
                    bw_images[l].image.gameObject.SetActive(false);
                    bw_images[l].isActived = false;
                    colored_images[l].image.gameObject.SetActive(true);
                    colored_images[l].isActived = true;
                }
                else
                {
                    colored_images[l].image.gameObject.SetActive(false);
                    colored_images[l].isActived = false;
                    bw_images[l].image.gameObject.SetActive(true);
                    bw_images[l].isActived = true;
                }

                break;

            case "Brass":

                Debug.Log("CASE: Brass");

                l = 3;

                if (soundBank.currentLoadedBrass.mIsLoaded)
                {
                    bw_images[l].image.gameObject.SetActive(false);
                    bw_images[l].isActived = false;
                    colored_images[l].image.gameObject.SetActive(true);
                    colored_images[l].isActived = true;
                }
                else
                {
                    colored_images[l].image.gameObject.SetActive(false);
                    colored_images[l].isActived = false;
                    bw_images[l].image.gameObject.SetActive(true);
                    bw_images[l].isActived = true;
                }

                break;

            default:
                Debug.Log("Something wrong");
                break;
        }
    }

    /*
    public void SetUp()
    {
        size = canvasManager.getArrSize();

        if (firstTime)
        {
            //Initial x position of the images

            var initial_y_Coordinates = new [] { 200f, 300f, 400f, 600f};


            for (int i = 0; i < size; i++)
            {

Debug.Log("Getting image " + i);
                //We get a reference of the colored image 
                ImageTarg n = canvasManager.getCompAt(i);         
                objects.Add(n);

                //We get the original position and we store it in an Hashtable
                Vector2 v = new Vector2(n.image.transform.position.x, n.image.transform.position.y);
                originalPos.Add(i, v);

                //We get the initial target position and we store it into an array
                Vector2 target = new Vector2(500f, initial_y_Coordinates[i]);
                targets.Add(target);

            }
            firstTime = false;
        }
        else
        {
            return;
        }
    }
    
    public void EnableMovements()
    {
        //Enable movements
        for (int i = 0; i < size; i++)
        {
            dragSystems[i].EnableMovements();
        }
    }

    public void ResetPosition()
    {        //Disable movements
        for (int i = 0; i < size; i++)
        {
            dragSystems[i].DisableMovements();
        }

        for (int i = 0; i < size; i ++)
        {
            Vector2 v = (Vector2)originalPos[i];
            objects[i].setImagePosition(v);
        }
    }
    */
}
