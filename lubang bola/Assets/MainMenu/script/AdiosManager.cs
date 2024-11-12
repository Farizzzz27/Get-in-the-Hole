using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;

public class AdiosManager : MonoBehaviour
{
    public static AdiosManager Instance { get; private set; }  // Singleton instance

    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource mainMenuMusicSource;  // AudioSource untuk musik MainMenu
    [SerializeField] private AudioSource gameplayMusicSource;  // Satu AudioSource untuk gameplay
    [SerializeField] public AudioSource SFXSource;  // AudioSource untuk SFX

    [Header("---------- Audio Clip ----------")]
    public AudioClip mainMenuMusicClip;  // AudioClip untuk musik MainMenu
    public AudioClip[] gameplayMusicClips;  // Array untuk musik gameplay

    public AudioClip walk;
    public AudioClip walltouch;
    public AudioClip win;
    public AudioClip lose;

    [Header("---------- Audio Mixer ----------")]
    [SerializeField] private AudioMixer myMixer;  // AudioMixer untuk pengaturan volume

    // Parameter untuk volume masing-masing kategori
    private const string mainMenuVolumeParam = "MainMenuMusic"; // Nama parameter volume untuk main menu
    private const string gameplayVolumeParam = "GameplayMusic"; // Nama parameter volume untuk gameplay

    private void Awake()
    {
        // Singleton pattern dan jangan hancurkan objek saat load scene baru
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Pastikan tidak hancur saat pindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Jika di scene MainMenu
        if (SceneManager.GetActiveScene().name == "Mainmenu")
        {
            PlayMainMenuMusic();  // Memutar musik MainMenu
        }
        else
        {
            PlayRandomGameplayMusic();  // Memutar musik gameplay acak
        }

        // Pastikan volume sesuai dengan pengaturan sebelumnya (jika ada)
        SetMusicVolume(PlayerPrefs.GetFloat("musicVolume", 0.5f));  // Gunakan nilai volume dari PlayerPrefs
    }

    // Method untuk memutar musik MainMenu
    private void PlayMainMenuMusic()
    {
        StopAllMusic(); // Hentikan musik lainnya sebelum memutar MainMenu music
        mainMenuMusicSource.clip = mainMenuMusicClip;  // Set musik MainMenu
        mainMenuMusicSource.Play();  // Putar musik MainMenu
    }

    // Method untuk merandom dan memutar musik gameplay
    private void PlayRandomGameplayMusic()
    {
        StopAllMusic();  // Hentikan musik lainnya sebelum memutar gameplay music
        int randomIndex = Random.Range(0, gameplayMusicClips.Length);  // Pilih musik secara acak
        gameplayMusicSource.clip = gameplayMusicClips[randomIndex];  // Set clip yang terpilih
        gameplayMusicSource.Play();  // Putar musik gameplay
    }

    // Menghentikan semua musik yang sedang diputar
    private void StopAllMusic()
    {
        mainMenuMusicSource.Stop();
        gameplayMusicSource.Stop();
    }

    // Method untuk memainkan SFX
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip); // Memainkan SFX
    }

    // Fungsi untuk mengatur volume musik menggunakan AudioMixer
    public void SetMusicVolume(float volume)
    {
        // Konversi volume slider ke dB dan set di kedua grup di AudioMixer
        volume = Mathf.Clamp(volume, 0.0001f, 1f);  // Menghindari log(0)
        float volumeInDb = Mathf.Log10(volume) * 20;
        myMixer.SetFloat(mainMenuVolumeParam, volumeInDb);  // Set volume untuk main menu
        myMixer.SetFloat(gameplayVolumeParam, volumeInDb);  // Set volume untuk gameplay
    }
}
