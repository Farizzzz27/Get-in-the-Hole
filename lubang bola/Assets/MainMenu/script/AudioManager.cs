using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource mainMenuMusicSource;
    [SerializeField] private AudioSource gameplayMusicSource1;
    [SerializeField] private AudioSource gameplayMusicSource2;
    [SerializeField] public AudioSource sfxSource;

    [Header("---------- Audio Mixer ----------")]
    [SerializeField] private AudioMixer audioMixer;

    private AudioSource activeGameplayMusicSource;
    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetBGMVolume(bgmVolume); // Pastikan BGM volume diterapkan
        SetSFXVolume(sfxVolume); // Pastikan SFX volume diterapkan
        PlayMusicBasedOnScene();
    }

    private void PlayMusicBasedOnScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        StopAllMusic();

        if (sceneName == "Mainmenu")
        {
            if (!mainMenuMusicSource.isPlaying && bgmVolume > 0)
            {
                mainMenuMusicSource.Play();
            }
        }
        else
        {
            activeGameplayMusicSource = (Random.value > 0.5f) ? gameplayMusicSource1 : gameplayMusicSource2;
            if (!activeGameplayMusicSource.isPlaying && bgmVolume > 0)
            {
                activeGameplayMusicSource.Play();
            }
        }
    }

    private void StopAllMusic()
    {
        if (mainMenuMusicSource.isPlaying)
        {
            mainMenuMusicSource.Stop();
        }
        if (gameplayMusicSource1.isPlaying)
        {
            gameplayMusicSource1.Stop();
        }
        if (gameplayMusicSource2.isPlaying)
        {
            gameplayMusicSource2.Stop();
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;

        mainMenuMusicSource.volume = volume;
        if (activeGameplayMusicSource != null)
        {
            activeGameplayMusicSource.volume = volume;

            if (volume == 0)
            {
                activeGameplayMusicSource.Stop();
            }
        }

        if (volume == 0 && mainMenuMusicSource.isPlaying)
        {
            mainMenuMusicSource.Stop();
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = volume;

        if (volume == 0 && sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }
}
