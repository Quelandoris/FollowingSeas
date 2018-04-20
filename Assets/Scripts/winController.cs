using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class winController : MonoBehaviour {

    public GameObject Continue;
    public GameObject Exit;
    public GameObject Win;
    public bool Won;


    void Start () {
		
	}
	
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Won = true;
            Continue.SetActive(true);
            Exit.SetActive(true);
            Win.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
