using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public SoundBank soundBank;

    public Image[] temp_bwImages;
    public Image[] temp_coloredImages;
 
    List<ImageTarg> bw_images = new List<ImageTarg>();
    List<ImageTarg> colored_images = new List<ImageTarg>();

    //public GameObject[] imageArr;

    int arrLenght;

    int l;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < temp_bwImages.Length; i++)
        {
            ImageTarg icon = new ImageTarg(true, temp_bwImages[i]);
            bw_images.Add(icon);

            arrLenght = temp_bwImages.Length;
        }
        for (int i = 0; i < temp_bwImages.Length; i++)
        {
            ImageTarg icon = new ImageTarg(false, temp_coloredImages[i]);
            colored_images.Add(icon);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonClick(Transform aButton)
    {
        string id = aButton.name;
        l = 0;

        switch (id)
        {
            case "Drum":

                Debug.Log("CASE: Drum");

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

    public int getArrSize()
    {
        int m = arrLenght;
        return m;
    }
    public ImageTarg getCompAt(int i)
    {
        return colored_images[i].getIcon();
    }
}

public class ImageTarg
{
    public bool isActived;
    public Image image;

    public ImageTarg (bool b, Image i)
    {
         isActived = b;
         image = i;
    }
    public ImageTarg getIcon()
    {
        return this;
    }
    public void setImagePosition(Vector2 v)
    {
        image.transform.position = v;
    }
}
