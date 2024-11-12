using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;              // AudioMixer utama
    [SerializeField] private Slider musicSlider;
    private const string mainMenuVolumeParam = "MainMenuMusic"; // Nama parameter volume untuk main menu
    private const string gameplayVolumeParam = "GameplayMusic"; // Nama parameter volume untuk gameplay

    private const string volumePrefKey = "musicVolume";       // Key untuk PlayerPrefs

    private void Start()
    {
        // Muat nilai volume yang tersimpan di PlayerPrefs, default 0.5 jika belum ada
        float savedVolume = PlayerPrefs.GetFloat(volumePrefKey, 0.5f);
        musicSlider.value = savedVolume;  // Set slider ke nilai yang tersimpan
        SetMusicVolume(savedVolume);  // Terapkan volume yang tersimpan ke kedua grup di AudioMixer
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f); // Batas minimum untuk mencegah log(0)
        PlayerPrefs.SetFloat(volumePrefKey, volume); // Simpan nilai slider ke PlayerPrefs
        PlayerPrefs.Save();

        // Konversi volume slider ke dB dan set di kedua grup di AudioMixer
        float volumeInDb = Mathf.Log10(volume) * 20;
        myMixer.SetFloat(mainMenuVolumeParam, volumeInDb);
        myMixer.SetFloat(gameplayVolumeParam, volumeInDb);

        // Pastikan AdiosManager mengupdate volume sesuai dengan slider
        AdiosManager.Instance.SetMusicVolume(volume); // Update volume di AdiosManager
    }
}
