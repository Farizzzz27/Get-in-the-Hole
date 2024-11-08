using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSettings : MonoBehaviour
{
    [Header("BGM Settings")]
    [SerializeField] private Slider bgmSlider; // Slider untuk volume BGM
    [SerializeField] private AudioSource mainMenuAudioSource; // AudioSource untuk BGM di MainMenu
    [SerializeField] private AudioSource gameplayAudioSource1; // AudioSource untuk BGM gameplay 1
    [SerializeField] private AudioSource gameplayAudioSource2; // AudioSource untuk BGM gameplay 2
    private const string BGMVolumeKey = "BGMVolume";

    [Header("SFX Settings")]
    [SerializeField] private Slider sfxSlider; // Slider untuk volume SFX
    [SerializeField] private AudioSource sfxAudioSource; // AudioSource untuk SFX
    private const string SFXVolumeKey = "SFXVolume";

    private AudioSource activeGameplayAudioSource; // Untuk menyimpan BGM gameplay yang aktif

    private void Start()
    {
        // Atur slider BGM dan volume dari PlayerPrefs
        bgmSlider.value = PlayerPrefs.GetFloat(BGMVolumeKey, 1f);
        UpdateVolume();

        // Atur slider SFX dan volume dari PlayerPrefs
        sfxSlider.value = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);
        UpdateSFXVolume(sfxSlider.value);

        // Tambahkan listener ke slider
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        // Mainkan musik sesuai scene
        PlayMusicBasedOnScene();
    }

    private void OnBGMVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
        UpdateVolume();
    }

    private void OnSFXVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        UpdateSFXVolume(volume);
    }

    private void UpdateVolume()
    {
        // Gunakan AudioListener untuk mengubah volume global
        AudioListener.volume = bgmSlider.value;

        if (activeGameplayAudioSource != null && activeGameplayAudioSource.volume > 0)
        {
            activeGameplayAudioSource.volume = bgmSlider.value;
        }

        // Jika volume BGM 0, stop semua musik
        if (bgmSlider.value == 0)
        {
            StopAllMusic();
        }
    }

    private void UpdateSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;

        // Jika volume SFX 0, stop semua SFX
        if (volume == 0)
        {
            sfxAudioSource.Stop();
        }
    }

    private void PlayMusicBasedOnScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentScene);

        if (currentScene == "Mainmenu")
        {
            // Stop gameplay music and play main menu music
            StopAllMusic();
            if (!mainMenuAudioSource.isPlaying)
            {
                mainMenuAudioSource.Play();
                Debug.Log("Main menu Music Started");
            }

            // Nonaktifkan SFX di main menu
            sfxAudioSource.Stop();
        }
        else
        {
            // Stop main menu music and play gameplay music
            if (mainMenuAudioSource.isPlaying)
            {
                mainMenuAudioSource.Stop();
            }

            if (activeGameplayAudioSource == null || !activeGameplayAudioSource.isPlaying)
            {
                // Pilih secara acak antara gameplayAudioSource1 atau gameplayAudioSource2
                activeGameplayAudioSource = (Random.value > 0.5f) ? gameplayAudioSource1 : gameplayAudioSource2;
                activeGameplayAudioSource.Play();
                Debug.Log("Gameplay Music Started: " + activeGameplayAudioSource.name);
            }

            // Aktifkan SFX di scene gameplay
            if (!sfxAudioSource.isPlaying)
            {
                sfxAudioSource.Play();
            }
        }
    }

    private void StopAllMusic()
    {
        if (mainMenuAudioSource.isPlaying)
        {
            mainMenuAudioSource.Stop();
        }

        if (activeGameplayAudioSource != null && activeGameplayAudioSource.isPlaying)
        {
            activeGameplayAudioSource.Stop();
        }

        // Stop semua audio
        sfxAudioSource.Stop();
    }

    private void OnDisable()
    {
        // Hapus listener saat script dinonaktifkan
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }
}
