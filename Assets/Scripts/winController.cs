using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class winController : MonoBehaviour {

    public GameObject Continue;
    public GameObject Exit;
    public GameObject Win;


    void Start () {
		
	}
	
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Continue.SetActive(true);
            Exit.SetActive(true);
            Win.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
