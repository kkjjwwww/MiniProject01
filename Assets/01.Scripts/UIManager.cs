using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TMP_Text hpText;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
           
    }
    private void Start()
    {
        UpdateExpUI(0f, 100f, 1);
        
    }
    private void Update()
    {
        UpdateTimerUI();
    }

    public void UpdateExpUI(float currentExp, float maxExp, int currentLevel)
    {
        if (expSlider != null)
        {
            expSlider.value = currentExp / maxExp;
        }

        if (levelText != null)
        {
            levelText.text = $"현재 레벨 : {currentLevel}";
        }
    }
    private void UpdateTimerUI()
    {
        if (timerText == null) return;
        if (SpawnManager.instance != null)
        {
            float time = SpawnManager.instance.totalGameTime;
            int min = Mathf.FloorToInt(time / 60);
            int sec = Mathf.FloorToInt(time % 60);

            timerText.text = $"{min:D2}:{sec:D2}";
        }
    }
    public void UpdateHpBarUI(float currentHp,float maxHp)
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value = Mathf.Clamp(currentHp, 0, maxHp);
        }
        if (hpText != null)
        {
            hpText.text = $"{Mathf.CeilToInt(currentHp)}/{Mathf.CeilToInt(maxHp)}";
        }
    }
}

