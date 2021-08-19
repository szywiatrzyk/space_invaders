using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    private Transform ScoreContainer;
    private Transform Record;
    public float hsSpace;
    private List<Transform> RecordList_Transform;
    private List<int> RecordList;

    private void Awake()
    {

        RecordList_Transform = new List<Transform>();

        ScoreContainer = transform.Find("ScoreContainer");
        Record = ScoreContainer.Find("HighScoreRecord");
        Record.gameObject.SetActive(false);
        
        string J = PlayerPrefs.GetString("HighscoreTable");
        
        if (string.IsNullOrEmpty(J))
        {
            RecordList = new List<int>();
            for(int i =0; i<10; i++) RecordList.Add(0);
     
            HighScoreTable table = new HighScoreTable { list = RecordList };
            J = JsonUtility.ToJson(table);
            PlayerPrefs.SetString("HighscoreTable", J);
            PlayerPrefs.Save();
            J = PlayerPrefs.GetString("HighscoreTable");

        }
        HighScoreTable loadedRecords = JsonUtility.FromJson<HighScoreTable>(J);



        foreach ( int a in loadedRecords.list)
        {
            BuildRecord(a, RecordList_Transform);
        }
    }


    private class HighScoreTable
    {
        public List<int> list;
    }


    void BuildRecord(int score, List<Transform> recordList)
    {
        Transform recordTemp = Instantiate(Record, ScoreContainer);
        RectTransform currentRecord = recordTemp.GetComponent<RectTransform>();
        currentRecord.anchoredPosition = new Vector2(0, -hsSpace * recordList.Count);
        currentRecord.gameObject.SetActive(true);

        currentRecord.Find("Position").GetComponent<Text>().text = "#" + (recordList.Count + 1).ToString();
        currentRecord.Find("Score").GetComponent<Text>().text = score.ToString();

        recordList.Add(currentRecord);
    }
}
