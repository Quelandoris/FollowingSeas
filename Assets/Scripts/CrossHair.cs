using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    public LayerMask grappleLayer;
    public LayerMask harpoonLayer;
    public GameObject GCrossHair;
    public GameObject crossHair;

    void Update()
    {
        RaycastHit mouseHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out mouseHit, 100000, grappleLayer))
        {
            GCrossHair.SetActive(true);
            GCrossHair.transform.Rotate((Vector3.forward * -90) * Time.deltaTime);
        }
        else
        {
            GCrossHair.SetActive(false);
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out mouseHit, 100000, harpoonLayer))
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
}
