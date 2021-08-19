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
        SceneManager.LoadScene("Game");
    }

    public void ShowHighscore()
    {
        HighScoreTable.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
