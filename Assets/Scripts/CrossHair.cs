using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    public LayerMask grappleLayer;
    public LayerMask harpoonLayer;
    public LayerMask waterLayer;
    public LayerMask currentLayer;
    public LayerMask playerLayer;
    LayerMask solidLayers;
    public GameObject GCrossHair;
    public GameObject crossHair;

    void Start()
    {
        solidLayers = ~(waterLayer | playerLayer | currentLayer);
    }

    void Update()
    {
        RaycastHit mouseHit;
        if (Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out mouseHit, 100000, solidLayers))
        {
            if (mouseHit.collider.CompareTag("Grapple"))
            {
                GCrossHair.SetActive(true);
                GCrossHair.transform.Rotate((Vector3.forward * -90) * Time.deltaTime);
            }
            else
            {
                GCrossHair.SetActive(false);
            }
        }
        else
        {
            GCrossHair.SetActive(false);
        }

        if (Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out mouseHit, 100000, solidLayers))
        {
            if (mouseHit.collider.CompareTag("Harpoon"))
            {
                crossHair.GetComponent<Image>().color = Color.red;
                GCrossHair.GetComponent<Image>().color = Color.red;
            }
            else
            {
                crossHair.GetComponent<Image>().color = Color.white;
                GCrossHair.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            crossHair.GetComponent<Image>().color = Color.white;
            GCrossHair.GetComponent<Image>().color = Color.white;
        }
    }
}
