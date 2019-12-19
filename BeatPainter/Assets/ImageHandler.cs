using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{

    public Image[] temp_bwImages;
    public Image[] temp_coloredImages;

    List <ImageTarget> bwImages = new List <ImageTarget>();
    List <ImageTarget> coloredImages = new List<ImageTarget>(); 

    Hashtable bwImagesHash = new Hashtable();
    Hashtable coloredImagesHash = new Hashtable();

    int current = -1;
    
    void Start()
    {
        for (int i = 0; i < temp_bwImages.Length; i++)
        {
            ImageTarget imageTarget = new ImageTarget(true, temp_bwImages[i]);
            bwImages.Add(imageTarget);
        }
        for (int i = 0; i < temp_coloredImages.Length; i++)
        {
            ImageTarget imageTarget = new ImageTarget(false, temp_coloredImages[i]);
            coloredImages.Add(imageTarget);
        }

        for (int i = 0; i < temp_bwImages.Length; i ++)
        {
            bwImagesHash.Add(i, bwImages[i]);
        }
        for(int i = 0; i < temp_coloredImages.Length; i ++)
        {
            coloredImagesHash.Add(i, coloredImages[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Controller(int i)
    {
 /*     if (i == current)
        {
            colored_imageTarget.isActive = false;
            colored_imageTarget.img.gameObject.SetActive(false);
            bw_imageTarget.isActive = true;
            bw_imageTarget.img.gameObject.SetActive(true);
            current = -1;
        } */

        for (int l = 0; l < 4; l ++)
        {
            if(l != i)
            {
                ImageTarget bw_imageT = (ImageTarget)bwImagesHash[l];
                ImageTarget colored_imageT = (ImageTarget)coloredImagesHash[l];

                bw_imageT.isActive = true;
                bw_imageT.img.gameObject.SetActive(true);
                colored_imageT.isActive = false;
                colored_imageT.img.gameObject.SetActive(false);
            }
            else
            {
                ImageTarget bw_imageTarget = (ImageTarget)bwImagesHash[i];
                ImageTarget colored_imageTarget = (ImageTarget)coloredImagesHash[i];

                if (bw_imageTarget.isActive == true)
                {
                    bw_imageTarget.isActive = false;
                    bw_imageTarget.img.gameObject.SetActive(false);
                    colored_imageTarget.isActive = true;
                    colored_imageTarget.img.gameObject.SetActive(true);
                }

                else
                {
                    bw_imageTarget.isActive = true;
                    bw_imageTarget.img.gameObject.SetActive(true);
                    colored_imageTarget.isActive = false;
                    colored_imageTarget.img.gameObject.SetActive(false);
                }
            }
        }
        current = i;
    }
}

public class ImageTarget
{
    public bool isActive;
    public Image img;

    public ImageTarget(bool b, Image i)
    {
        this.isActive = b;
        this.img = i;
    }
}
