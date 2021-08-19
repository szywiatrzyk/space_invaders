using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject HighScoreTable;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void LoadScene()
    {
        Debug.Log("PlayGame");
        SceneManager.LoadScene("Game");
    }

    public void ShowHighscore()
    {
        Debug.Log("HighScore");
        HighScoreTable.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit(); 
    }
}
