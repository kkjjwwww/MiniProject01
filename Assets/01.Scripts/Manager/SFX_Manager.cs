using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager instance;

    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip playerHitSFX;
    [SerializeField] private AudioClip enemyHitSFX;

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
