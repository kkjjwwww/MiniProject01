using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
{
    public static UI_Pause instance;

    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button giveUpButton;

    [SerializeField] private string titleSceneName = "TitleScene";

    private bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (resumeButton != null) resumeButton.onClick.AddListener(ResumeGame);
        if (giveUpButton != null) giveUpButton.onClick.AddListener(GiveUpGame);

        if (bgmVolumeSlider != null)
        {
            if (BGM_Manager.instance != null)
            {
                bgmVolumeSlider.value = BGM_Manager.instance.GetVolume();
            }
            else
            {
                bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
            }
            bgmVolumeSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        }
        if (sfxVolumeSlider != null)
        {
            if (SFX_Manager.instance != null)
            {
                sfxVolumeSlider.value = SFX_Manager.instance.GetVolume();
            }
            else
            {
                sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            }
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isPaused && Time.timeScale == 0f) return;
            TogglePause();
        }
    }
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null) pausePanel.SetActive(false);
    }

    private void OnBGMVolumeChanged(float value)
    {
        if (BGM_Manager.instance != null)
        {
            BGM_Manager.instance.SetVolume(value);
        }
    }
    private void OnSFXVolumeChanged(float value)
    {
        if (SFX_Manager.instance != null)
        {
            SFX_Manager.instance.SetVolume(value);
        }
    }
    public void GiveUpGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(titleSceneName);
    }
}
