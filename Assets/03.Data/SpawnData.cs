using JetBrains.Annotations;
using System;
using UnityEngine;
using System.Collections.Generic;


[Serializable]
public class SpawnElement
{
    public Enemy enemyPrefab;
    public int spawnWeight = 10;
}
[CreateAssetMenu(fileName = "NewSpawnData" , menuName = "ScriptableObjects/SpawnData")]
public class SpawnData : ScriptableObject
{
    public float spawnInterval = 1.5f;
    public int maxCountEnemy = 300;

    public List<SpawnElement> spawnList;

    public Enemy GetRandomEnemyPrefab()
    {
        if (spawnList == null || spawnList.Count == 0) return null;
        int totalWeight = 0;
        foreach (var element in spawnList)
        {
            totalWeight += element.spawnWeight;
        }
        int randomValue = UnityEngine.Random.Range(0, totalWeight);
        int currentWeightSum = 0;

        foreach (var element in spawnList)
        {
            currentWeightSum += element.spawnWeight;
            if (randomValue < currentWeightSum)
            {
                return element.enemyPrefab;
            }
        }

        return spawnList[0].enemyPrefab;
    }


}
