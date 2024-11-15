using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Singleton instance

    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource; // AudioSource yang akan digunakan untuk musik
    [SerializeField] AudioSource sfxSource;   // AudioSource untuk SFX

    [Header("---------- Audio Clip ----------")]
    public AudioClip[] mainMenuMusic;          // Array untuk musik main menu (acak)
    public AudioClip[] gameplayMusic;         // Array untuk musik gameplay

    [Header("---------- SFX ----------")]
    public AudioClip walk;     // SFX untuk jalan
    public AudioClip walltouch; // SFX untuk tabrakan
    public AudioClip win;      // SFX untuk menang
    public AudioClip lose;     // SFX untuk kalah

    private void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Membuat AudioManager tetap ada saat berpindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusicBasedOnScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded; // Event untuk mendeteksi perubahan scene
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicBasedOnScene(scene.name);
    }

    private void PlayMusicBasedOnScene(string sceneName)
    {
        if (sceneName == "Mainmenu")
        {
            PlayRandomMainMenuMusic();
        }
        else if (sceneName.StartsWith("Level"))
        {
            PlayRandomGameplayMusic();
        }
    }

    private void PlayRandomMainMenuMusic()
    {
        int randomIndex = Random.Range(0, mainMenuMusic.Length);
        if (musicSource.clip != mainMenuMusic[randomIndex] || !musicSource.isPlaying)
        {
            musicSource.Stop();
            musicSource.clip = mainMenuMusic[randomIndex];
            musicSource.loop = true; // Looping untuk main menu music
            musicSource.Play();
        }
    }

    private void PlayRandomGameplayMusic()
    {
        // Memeriksa apakah musik gameplay sedang diputar atau belum
        if (!musicSource.isPlaying || !gameplayMusic.Contains(musicSource.clip))
        {
            musicSource.Stop();
            musicSource.clip = gameplayMusic[Random.Range(0, gameplayMusic.Length)];
            musicSource.loop = true; // Agar musik gameplay diputar terus menerus
            musicSource.Play();
        }
    }

    private void Update()
    {
        // Cek jika musik gameplay telah selesai diputar, ganti dengan musik lainnya
        if (!musicSource.isPlaying && gameplayMusic.Contains(musicSource.clip))
        {
            PlayRandomGameplayMusic();  // Panggil ulang untuk memilih musik baru
        }
    }

    // Memutar SFX sesuai clip yang dipilih
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
