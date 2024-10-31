using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }  // Singleton instance

    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource mainMenuMusicSource;
    [SerializeField] private AudioSource gameplayMusicSource;
    [SerializeField] public AudioSource sfxSource;  // AudioSource untuk SFX

    private void Awake()
    {
        // Singleton pattern
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
        SceneManager.sceneLoaded += OnSceneLoaded;  // Event listener untuk scene change
        PlayMusicBasedOnScene(); // Play music on start based on the current scene
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Hapus event listener saat script dihancurkan
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicBasedOnScene();  // Atur musik sesuai dengan scene
    }

    private void PlayMusicBasedOnScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MainMenu")
        {
            StopAllMusic();  // Stop semua musik sebelum memutar yang baru
            if (!mainMenuMusicSource.isPlaying)
            {
                mainMenuMusicSource.Play();
            }
        }
        else
        {
            StopAllMusic();  // Stop semua musik sebelum memutar yang baru
            if (!gameplayMusicSource.isPlaying)
            {
                gameplayMusicSource.Play();
            }
        }
    }

    private void StopAllMusic()
    {
        if (mainMenuMusicSource.isPlaying)
        {
            mainMenuMusicSource.Stop();
        }
        if (gameplayMusicSource.isPlaying)
        {
            gameplayMusicSource.Stop();
        }
        // Jangan menghentikan SFX
    }

    // Set volume SFX dari VolumeSettings
    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
        }
    }

    public bool IsSFXPlaying()
    {
        return sfxSource.isPlaying;
    }

    public void PlaySFX(AudioClip clip)
    {
        StartCoroutine(PlaySFXCoroutine(clip));
    }

    private IEnumerator PlaySFXCoroutine(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.clip = clip;
            sfxSource.Play();

            // Tunggu sampai SFX selesai diputar
            yield return new WaitForSeconds(clip.length);
        }
    }
}
