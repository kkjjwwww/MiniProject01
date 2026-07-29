using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class BGM_Manager : MonoBehaviour
{
    public static BGM_Manager instance;

    [SerializeField] AudioClip titleBGM;
    [SerializeField] AudioClip inGameBGM;

    [SerializeField] private string titleSceneName = "TitleScene";
    [SerializeField] private string inGameSceneName = "InGameScene";
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>(); 

            if(audioSource == null)
            {
                audioSource = gameObject.GetComponent<AudioSource>();
            }
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == titleSceneName)
        {
            PlayBGM(titleBGM);
        }
        else if (scene.name == inGameSceneName)
        {
            PlayBGM(inGameBGM);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        if (audioSource.clip == clip && audioSource.isPlaying) return;
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
