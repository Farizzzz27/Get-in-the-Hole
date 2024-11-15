using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public ParticleSystem fireworksParticle; // Referensi ke partikel kembang api
    public float delayBeforeNextScene = 3f;  // Waktu tunggu sebelum pindah scene
    public string nextSceneName;             // Nama scene berikutnya

    private bool hasWon = false; // Pengaman agar hanya satu kali menang
    private AudioManager audioManager; // Referensi ke AudioManager

    private void Awake()
    {
        // Mengambil referensi AudioManager
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika bola memasuki platform
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;
            PlayFireworks();
            SaveProgress();
            FreezeCamera();
        }
    }

    private void PlayFireworks()
    {
        // Aktifkan partikel kembang api
        if (fireworksParticle != null)
        {
            fireworksParticle.Play();
        }

        // Mainkan SFX menang menggunakan AudioManager
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.win); // Menggunakan AudioManager untuk memutar SFX menang
        }

        // Pindah ke scene berikutnya setelah delay
        Invoke("LoadNextScene", delayBeforeNextScene);
    }

    private void SaveProgress()
    {
        // Simpan progress level selesai
        string currentLevel = SceneManager.GetActiveScene().name; // Nama level, misalnya "Level1"
        PlayerPrefs.SetInt(currentLevel, 1);                      // Tandai level ini selesai
        PlayerPrefs.Save();                                       // Pastikan tersimpan
    }

    private void FreezeCamera()
    {
        // Mengakses komponen CameraFollow pada kamera utama dan mengaktifkan freeze
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.freezeCamera = true; // Menghentikan rotasi dan posisi kamera
        }
    }

    private void LoadNextScene()
    {
        // Pindah ke scene berikutnya
        SceneManager.LoadScene(nextSceneName);
    }
}
