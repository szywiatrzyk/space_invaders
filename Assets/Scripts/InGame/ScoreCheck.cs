using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCheck : MonoBehaviour
{
    GameManager gameManager;
    Text scoreText;
    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
        gameManager = GameObject.FindGameObjectWithTag("gameManager").gameObject.GetComponent<GameManager>();
    }

   
    void Update()
    {
        scoreText.text = "Score: "  + gameManager.CheckScore().ToString();
    }
}
