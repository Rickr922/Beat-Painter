using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour
{
    public Material[] materials;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
    }

    void touchBegan()
    {
        rend.sharedMaterial = materials[1];
    }

    void touchEnded()
    {
        rend.sharedMaterial = materials[0];
    }

    public void isActive()
    {

    }

    void touchStay()
    {

    }

    void touchExit()
    {
        rend.sharedMaterial = materials[0];
    }

}