﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

	// Changes scenes as a public function
	public void SceneChange (int buildOrder) {
		SceneManager.LoadScene (buildOrder);
	}
	// Enables and disables objects as needed
	public void ObjectToggle (GameObject target) {
		target.SetActive (!target.activeSelf);
	}
	// Exits the game
	public void GameClose () {
		Application.Quit ();
	}
<<<<<<< HEAD
<<<<<<< HEAD
    public void Start()
=======
    public void Awake()
>>>>>>> parent of 066c025... improved title screen
=======
    public void Awake()
>>>>>>> parent of 0b12ee5... Revert "beat the bug of the title screen!"
    {
        Cursor.visible = true;
    }
}
