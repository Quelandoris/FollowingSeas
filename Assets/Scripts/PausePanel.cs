using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour {

    private CanvasGroup pausePanel;
    public CanvasGroup levelSelect;
    public CanvasGroup regularPanal;
    private bool activated;
    // Use this for initialization 
    void Awake()
    {
        pausePanel = GetComponent<CanvasGroup>();
        Time.timeScale = 1;
        pausePanel.alpha = 0;
        pausePanel.interactable = false;

    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

           
            activated = !activated;

        }
        if (activated)
        {
            pausePanel.alpha = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pausePanel.interactable = true;
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.alpha = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pausePanel.interactable = false;
            Time.timeScale = 1;
            
        }
    }
    public void LevelSelect()
    {
        
        levelSelect.interactable = true;
        levelSelect.blocksRaycasts = true;
        levelSelect.alpha = 1;
        
        regularPanal.interactable = false;
        regularPanal.blocksRaycasts = false;
        regularPanal.alpha = 0;
    }
    public void Back()
    {
        
        levelSelect.interactable = false;
        levelSelect.blocksRaycasts = false;
        levelSelect.alpha = 0;
       
        regularPanal.interactable = true;
        regularPanal.blocksRaycasts = true;
        regularPanal.alpha = 1;
    }
    public void UnPause()
    {

        activated = !activated;
    }

}