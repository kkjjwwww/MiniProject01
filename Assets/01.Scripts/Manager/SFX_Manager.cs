using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager instance;

    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip playerHitSFX;
    [SerializeField] private AudioClip enemyHitSFX;
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip levelUpSFX;
    [SerializeField] private AudioClip gameStartSfx;

    private int lastFrame = -1;

    private const string SFX_VOLUME_KEY = "SFXVolume";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        float savedVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.5f);
        sfxSource.volume = savedVolume;
    }

    public void PlayPlayerHitSFX()
    {
        if (sfxSource != null && playerHitSFX != null)
        {
            sfxSource.PlayOneShot(playerHitSFX);
        }
    }
    public void PlayEnemyHitSFX()
    {
        if (sfxSource != null && enemyHitSFX != null)
        {
            if (Time.frameCount == lastFrame) return;
            lastFrame = Time.frameCount;
            sfxSource.PlayOneShot(enemyHitSFX);
        }
    }
    public void PlayButtonClickSFX()
    {
        if (sfxSource != null && buttonClickSFX != null)
        {
            sfxSource.PlayOneShot(buttonClickSFX);
        }
    }
    public void PlayLevelUpSFX()
    {
        if (sfxSource != null && levelUpSFX != null)
        {
            sfxSource.PlayOneShot(levelUpSFX);
        }
    }
    public void PlayGameStartSFX()
    {
        if (sfxSource != null && gameStartSfx != null)
        {
            sfxSource.PlayOneShot(gameStartSfx);
        }
    }
    public void PlayGameOverSFX()
    {
        if (sfxSource != null && gameOverSFX != null)
        {
            sfxSource.PlayOneShot(gameOverSFX);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    public void SetVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
            PlayerPrefs.Save();
        }
    }
    public float GetVolume()
    {
        return sfxSource != null ? sfxSource.volume : PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.5f);
    }
}
