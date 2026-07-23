using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Records : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDate;
    [SerializeField] private TextMeshProUGUI textTimeRecord;
    [SerializeField] private TextMeshProUGUI textKillCount;

    [SerializeField] private Transform artifactIconBox;
    [SerializeField] private Image artifactIconPrefab;

    public void Init(GameRecord record)
    {
        textDate.text = record.recordDate;

        int minutes = Mathf.FloorToInt(record.timeRecord / 60f);
        int seconds = Mathf.FloorToInt(record.timeRecord % 60f);
        textTimeRecord.text = $"생존 시간: {minutes:00}:{seconds:00}";

        textKillCount.text = $"처치한 적: {record.killCount}";

        foreach(Transform child in artifactIconBox)
        {
            Destroy(child.gameObject);
        }
        foreach(string spriteName in record.artifactSpriteNames)
        {
            Image iconImg = Instantiate(artifactIconPrefab, artifactIconBox);

            if (string.IsNullOrEmpty(spriteName)) continue;
            Sprite sprite = Resources.Load<Sprite>($"ArtifactIcons/{spriteName}");

            
            iconImg.sprite = sprite;
            
        }
    }
}
