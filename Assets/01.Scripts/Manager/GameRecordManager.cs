using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameRecordManager : MonoBehaviour
{
    public static GameRecordManager instance;
    private const string SAVE_KEY = "GameRecords_SaveData";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        
    }
    public void SaveCurrentRecord(float timeRecord, int killCount, List<Sprite> itemIcons)
    {
        List<GameRecord> currentRecords = LoadRecords();

        GameRecord newRecord = new GameRecord
        {
            recordDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            timeRecord = timeRecord,
            killCount = killCount,
        };
        
        if (itemIcons != null)
        {
            foreach (Sprite icon in itemIcons)
            {
                if (icon != null)
                {
                    newRecord.artifactSpriteNames.Add(icon.name);
                }
            }
        }
        currentRecords.Insert(0, newRecord);

        GameRecordList List = new GameRecordList { records = currentRecords };
        string json = JsonUtility.ToJson(List);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
        Debug.Log("기록 저장 완료");
    }

    public List<GameRecord> LoadRecords()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return new List<GameRecord>();
        }
        string json = PlayerPrefs.GetString(SAVE_KEY);
        GameRecordList list = JsonUtility.FromJson<GameRecordList>(json);
        if (list != null)
        {
            return list.records;
        } else return new List<GameRecord>();
    }
}
