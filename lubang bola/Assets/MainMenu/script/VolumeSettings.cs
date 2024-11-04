using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("BGM Settings")]
    [SerializeField] private Slider bgmSlider; // Slider untuk volume BGM
    [SerializeField] private AudioSource bgmAudioSource; // AudioSource untuk BGM
    private const string BGMVolumeKey = "BGMVolume";

    [Header("SFX Settings")]
    [SerializeField] private Slider sfxSlider; // Slider untuk volume SFX
    [SerializeField] private AudioSource sfxAudioSource; // AudioSource untuk SFX
    private const string SFXVolumeKey = "SFXVolume";

    private void Start()
    {
        // Atur slider BGM dan AudioSource sesuai nilai PlayerPrefs
        bgmSlider.value = PlayerPrefs.GetFloat(BGMVolumeKey, 1f);
        bgmAudioSource.volume = bgmSlider.value;

        // Mulai audio BGM jika belum diputar
        if (!bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Play();
        }

        // Atur slider SFX dan AudioSource sesuai nilai PlayerPrefs
        sfxSlider.value = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);
        sfxAudioSource.volume = sfxSlider.value;

        // Tambahkan listener ke slider
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnBGMVolumeChanged(float volume)
    {
        Debug.Log("BGM volume changed: " + volume); // Debug log
        bgmAudioSource.volume = volume;

        // Simpan nilai volume BGM di PlayerPrefs
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
    }

    private void OnSFXVolumeChanged(float volume)
    {
        Debug.Log("SFX volume changed: " + volume); // Debug log
        sfxAudioSource.volume = volume;

        // Simpan nilai volume SFX di PlayerPrefs
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }

    private void OnDisable()
    {
        // Hapus listener saat script dinonaktifkan untuk menghindari masalah
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }
}
