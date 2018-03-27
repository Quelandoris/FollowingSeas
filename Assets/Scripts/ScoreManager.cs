using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score = 0;
    private Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
    }
    void Update () {

       scoreText.text = ("Loot: " + score);
    }
}
