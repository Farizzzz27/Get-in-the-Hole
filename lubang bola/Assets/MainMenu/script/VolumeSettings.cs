using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSettings : MonoBehaviour
{
    [Header("BGM Settings")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioSource gameplayAudioSource1;
    [SerializeField] private AudioSource gameplayAudioSource2;
    private const string BGMVolumeKey = "BGMVolume";

    [Header("SFX Settings")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioSource sfxAudioSource;
    private const string SFXVolumeKey = "SFXVolume";

    private AudioSource activeGameplayAudioSource;
    private bool wasMutedBefore = false; // Flag untuk memastikan musik tidak langsung bermain saat diatur ke 0
    private float lastMusicTime = 0f;

    private void Start()
    {
        // Muat nilai slider dari PlayerPrefs
        bgmSlider.value = PlayerPrefs.GetFloat(BGMVolumeKey, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);

        // Update volume sesuai nilai slider
        UpdateVolume();
        UpdateSFXVolume(sfxSlider.value);

        // Tambahkan listener ke slider
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

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
        AudioListener.volume = bgmSlider.value;

        if (bgmSlider.value == 0)
        {
            wasMutedBefore = true;
            if (activeGameplayAudioSource != null && activeGameplayAudioSource.isPlaying)
                lastMusicTime = activeGameplayAudioSource.time;
            else if (mainMenuAudioSource.isPlaying)
                lastMusicTime = mainMenuAudioSource.time;

            StopAllMusic();
        }
        else
        {
            if (wasMutedBefore)
            {
                PlayMusicBasedOnScene(); // Putar musik dari posisi terakhir jika di-mute sebelumnya
                wasMutedBefore = false;
            }
            else
            {
                // Update volume source yang aktif jika musik tidak berhenti
                if (activeGameplayAudioSource != null && activeGameplayAudioSource.isPlaying)
                    activeGameplayAudioSource.volume = bgmSlider.value;
                else if (mainMenuAudioSource.isPlaying)
                    mainMenuAudioSource.volume = bgmSlider.value;
            }
        }
    }

    private void UpdateSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
        if (volume == 0)
            sfxAudioSource.Stop();
    }

    private void PlayMusicBasedOnScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Mainmenu")
        {
            StopAllMusic();
            if (bgmSlider.value > 0)
            {
                mainMenuAudioSource.time = wasMutedBefore ? lastMusicTime : 0f;
                mainMenuAudioSource.volume = bgmSlider.value;
                mainMenuAudioSource.Play();
            }
            sfxAudioSource.Stop();
        }
        else
        {
            StopAllMusic();

            if (bgmSlider.value > 0)
            {
                activeGameplayAudioSource = (Random.value > 0.5f) ? gameplayAudioSource1 : gameplayAudioSource2;
                activeGameplayAudioSource.time = wasMutedBefore ? lastMusicTime : 0f;
                activeGameplayAudioSource.volume = bgmSlider.value;
                activeGameplayAudioSource.Play();
            }

            if (sfxSlider.value > 0 && !sfxAudioSource.isPlaying)
            {
                sfxAudioSource.Play();
            }
        }

        lastMusicTime = 0f;
        wasMutedBefore = false;
    }

    private void StopAllMusic()
    {
        if (mainMenuAudioSource.isPlaying)
            mainMenuAudioSource.Stop();

        if (activeGameplayAudioSource != null && activeGameplayAudioSource.isPlaying)
            activeGameplayAudioSource.Stop();

        sfxAudioSource.Stop();
    }

    private void OnDisable()
    {
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }
}
