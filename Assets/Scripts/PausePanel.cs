using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {
    private CanvasGroup pausePanel;
    public CanvasGroup levelSelect;
    public CanvasGroup regularPanal;
    private bool activated;
	// Use this for initialization
	void Awake () {
        pausePanel = GetComponent<CanvasGroup>();

        pausePanel.alpha = 0;
        pausePanel.interactable = false;
       
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            activated = !activated;

        }
        if (activated)
        {
            pausePanel.alpha = 1;
            
            pausePanel.interactable = true;
        }
        else
        {
            pausePanel.alpha = 0;
            pausePanel.interactable = false;
        }
    }
    public void LevelSelect()
    {
        levelSelect.alpha = 1;
        levelSelect.interactable = true;
        regularPanal.alpha = 0;
        regularPanal.interactable = false;
    }
    public void Back()
    {
        levelSelect.alpha = 0;
        levelSelect.interactable = false;
        regularPanal.alpha = 1;
        regularPanal.interactable = true;
    }

}
