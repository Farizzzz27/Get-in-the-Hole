using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider; // Slider untuk volume BGM
    [SerializeField] private AudioSource bgmAudioSource; // AudioSource untuk BGM

    private const string BGMVolumeKey = "BGMVolume";

    private void Start()
    {
        // Pastikan slider diatur sesuai nilai PlayerPrefs
        bgmSlider.value = PlayerPrefs.GetFloat(BGMVolumeKey, 1f);
        bgmAudioSource.volume = bgmSlider.value;

        // Mulai audio jika belum diputar
        if (!bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Play();
        }

        // Tambahkan listener ke slider
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
    }

    private void OnBGMVolumeChanged(float volume)
    {
        Debug.Log("Slider value changed: " + volume); // Untuk mengecek perubahan nilai
        bgmAudioSource.volume = volume;
    

    // Simpan nilai volume di PlayerPrefs
    PlayerPrefs.SetFloat(BGMVolumeKey, volume);
    }

    private void OnDisable()
    {
        // Hapus listener saat script dinonaktifkan untuk menghindari masalah
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
    }
}
