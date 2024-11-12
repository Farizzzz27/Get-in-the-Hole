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
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Method untuk memutar musik MainMenu
    private void PlayMainMenuMusic()
    {
        mainMenuMusicSource.clip = mainMenuMusicClip;  // Set musik MainMenu
        mainMenuMusicSource.Play();  // Putar musik MainMenu
    }

    // Method untuk merandom dan memutar musik gameplay
    private void PlayRandomGameplayMusic()
    {
        int randomIndex = Random.Range(0, gameplayMusicClips.Length);  // Pilih musik secara acak
        gameplayMusicSource.clip = gameplayMusicClips[randomIndex];  // Set clip yang terpilih
        gameplayMusicSource.Play();  // Putar musik gameplay
    }
}
