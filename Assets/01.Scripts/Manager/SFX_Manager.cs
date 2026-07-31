using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public enum SFXType
{
    PlayerHit,
    EnemyHit,
    GameOver,
    ButtonClick,
    LevelUp,
    GameStart,
    Exp,
    HuaXiong_Attack
}

public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager instance;

    [SerializeField] private AudioSource sfxSource;

    [Serializable]
    public struct SFXData
    {
        public SFXType type;
        public AudioClip clip;
    }

    [SerializeField] private List<SFXData> sfxList;

    private Dictionary<SFXType, AudioClip> sfxDictionary = new Dictionary<SFXType, AudioClip>();

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
            return;
        }
        float savedVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.5f);
        sfxSource.volume = savedVolume;

        InitDictianary();
    }

    private void InitDictianary()
    {
        sfxDictionary.Clear();
        foreach (var sfx in sfxList)
        {
            if (sfx.clip == null) continue;

            if (!sfxDictionary.ContainsKey(sfx.type))
            {
                sfxDictionary.Add(sfx.type, sfx.clip);
            }
            else
            {
                Debug.Log("SFXType ม฿บน.");
            }

        }
    }

     
    public void PlaySFX(SFXType type)
    {
        if (sfxSource == null) return;

        if (type == SFXType.EnemyHit)
        {
            if (Time.frameCount == lastFrame) return;
            lastFrame = Time.frameCount;
        }
        if (sfxDictionary.TryGetValue(type, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX_Manager {type} SFXType is not founded.");
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    public void PlayPlayerHitSFX() => PlaySFX(SFXType.PlayerHit);
    public void PlayEnemyHitSFX() => PlaySFX(SFXType.EnemyHit);
    public void PlayButtonClickSFX() => PlaySFX(SFXType.ButtonClick);
    public void PlayLevelUpSFX() => PlaySFX(SFXType.LevelUp);
    public void PlayGameStartSFX() => PlaySFX(SFXType.GameStart);
    public void PlayGameOverSFX() => PlaySFX(SFXType.GameOver);
    public void PlayExpSFX() => PlaySFX(SFXType.Exp);
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
