using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    private int score;
    private int enemyInColumn;
    public GameObject enemyControler;
    public GameObject EndScreen;
    private bool isEndGame;

    void Awake()
    {
        isEndGame = false;
        score = 0;
    }
    private void Start()
    {
        enemyInColumn = enemyControler.GetComponent<EnemyManager>().howManyEnemyInColumn;
    }


    public void ScoreChange(string forWhat)
    {
        if(forWhat == "EnemyDestruction")
        {
            score += 1;
        }

        if (forWhat == "PlayerHit")
        {
            score -= 2*enemyInColumn;
            if (score < 0) score = 0;
        }

    }

    public int CheckScore() 
    {

        return score;

    }



    public void EndGame(int mode) 
    {
        if (isEndGame == false)
        {
            isEndGame = true;
            int scoreTemp = score;
            EndScreen.gameObject.SetActive(true);
            if (mode == 2)
            {
                EndScreen.transform.Find("EndText").GetComponent<Text>().text = "You win!";
            }
            string J = PlayerPrefs.GetString("HighscoreTable");
            HighScoreTable loadedRecords = JsonUtility.FromJson<HighScoreTable>(J);
            List<int> recordList = loadedRecords.list;

            recordList.Add(scoreTemp);
            recordList.Sort((p1, p2) => p2.CompareTo(p1));
            recordList.RemoveAt(recordList.Count - 1);



            if (recordList.Find(x => x == scoreTemp) == scoreTemp)
            {
                int pos = recordList.IndexOf(scoreTemp);
                if (pos != -1)
                {
                    Transform betterscore = EndScreen.transform.Find("Betterscore");
                    betterscore.gameObject.gameObject.SetActive(true);
                    betterscore.gameObject.GetComponent<Text>().text = "#" + (pos + 1).ToString() + " Place!!!";
                }
            }

            Transform scoreContainer = EndScreen.transform.Find("Score");
            scoreContainer.gameObject.GetComponent<Text>().text = "Your score: \n" + scoreTemp;

            HighScoreTable table = new HighScoreTable { list = recordList };
            string jn = JsonUtility.ToJson(table);
            PlayerPrefs.SetString("HighscoreTable", jn);
            PlayerPrefs.Save();
        }
    }

    private class HighScoreTable
    {
        public List<int> list;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
