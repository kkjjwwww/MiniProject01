using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class GameRecord
{
    public string recordDate;
    public float timeRecord;
    public int killCount;
    public List<String> artifactSpriteNames = new List<string>();

}
[Serializable]

public class GameRecordList
{
    public List<GameRecord> records = new List<GameRecord>();
}

