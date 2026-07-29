using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager instance;

    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip playerHitSFX;
    [SerializeField] private AudioClip enemyHitSFX;

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
}
