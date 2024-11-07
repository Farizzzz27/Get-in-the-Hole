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
        UpdateBGMVolume();

        // Atur slider SFX dan volume dari PlayerPrefs
        sfxSlider.value = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);
        sfxAudioSource.volume = sfxSlider.value;

        // Tambahkan listener ke slider
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        // Mainkan musik sesuai scene
        PlayMusicBasedOnScene();
    }

    private void OnBGMVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
        UpdateBGMVolume();
    }

    private void OnSFXVolumeChanged(float volume)
    {
        sfxAudioSource.volume = volume;
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }

    private void UpdateBGMVolume()
    {
        // Atur volume berdasarkan slider untuk setiap AudioSource
        mainMenuAudioSource.volume = bgmSlider.value;

        if (activeGameplayAudioSource != null)
        {
            activeGameplayAudioSource.volume = bgmSlider.value;
        }
    }

    private void PlayMusicBasedOnScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // Cek apakah sedang di scene Main Menu
        if (currentScene == "Mainmenu")
        {
            // Hentikan musik gameplay jika ada yang sedang aktif
            if (activeGameplayAudioSource != null && activeGameplayAudioSource.isPlaying)
            {
                activeGameplayAudioSource.Stop();
                activeGameplayAudioSource = null;  // Reset untuk gameplay scene nanti
            }

            // Mulai mainkan musik Main Menu jika belum diputar
            if (!mainMenuAudioSource.isPlaying)
            {
                mainMenuAudioSource.Play();
            }
        }
        else
        {
            // Hentikan musik Main Menu jika sedang diputar
            if (mainMenuAudioSource.isPlaying)
            {
                mainMenuAudioSource.Stop();
            }

            // Pilih antara gameplayAudioSource1 atau gameplayAudioSource2 secara acak jika belum ada yang aktif
            if (activeGameplayAudioSource == null)
            {
                activeGameplayAudioSource = (Random.value > 0.5f) ? gameplayAudioSource1 : gameplayAudioSource2;
                activeGameplayAudioSource.Play();
            }
        }

        // Update volume untuk memastikan volume sesuai slider
        UpdateBGMVolume();
    }


    private void OnDisable()
    {
        // Hapus listener saat script dinonaktifkan
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }
}
