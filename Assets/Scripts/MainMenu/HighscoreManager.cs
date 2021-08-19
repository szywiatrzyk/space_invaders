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
        HighScoreTable loadedRecords = JsonUtility.FromJson<HighScoreTable>(J);

        //RecordList = new List<int>();
        //RecordList.Add(1);
        //RecordList.Add(0);
        //RecordList.Add(0);
        //RecordList.Add(4);
        //RecordList.Add(0);
        //RecordList.Add(0);
        //RecordList.Add(0);
        //RecordList.Add(0);
        //RecordList.Add(0);
        //RecordList.Add(0);
        //RecordList.Sort((p1, p2) => p2.CompareTo(p1));
        //HighScoreTable table = new HighScoreTable { list = RecordList };
        //string jn = JsonUtility.ToJson(table);
        //PlayerPrefs.SetString("HighscoreTable", jn);
        //PlayerPrefs.Save();
        //Debug.Log(PlayerPrefs.GetString("HighscoreTable"));
        
        
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
