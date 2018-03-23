using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public float minDistance = 1.0f;
    public float maxDistance = 15.0f;
    public float smooth = 10.0f;
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;
    //float chaseLength = 15;
    //float scrollSpeed = -6;

    public GameObject UnderwaterEffect;

    LayerMask UnPhaseableLayers;
    public LayerMask UnTargetedLayers;

    void Start()
    {
        UnPhaseableLayers = ~(UnTargetedLayers);
        
        UnderwaterEffect.SetActive(false);
    }
    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }
    private void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        if(Physics.Linecast(transform.parent.position,desiredCameraPos,out hit,UnPhaseableLayers))
        {
            distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);

        }
        else
        {
            distance = maxDistance;
        }
        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit, UnTargetedLayers))
        {
            UnderwaterEffect.SetActive(true);
        }
        else
        {
            UnderwaterEffect.SetActive(false);
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
        

    }


}