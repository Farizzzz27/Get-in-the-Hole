using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public ParticleSystem fireworksParticle; // Referensi ke partikel kembang api
    public AudioSource winSFX;               // AudioSource untuk SFX menang
    public float delayBeforeNextScene = 3f;  // Waktu tunggu sebelum pindah scene
    public string nextSceneName;             // Nama scene berikutnya

    private bool hasWon = false; // Pengaman agar hanya satu kali menang

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika bola memasuki platform
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;
            PlayFireworks();
            FreezeCamera(); // Tambahkan fungsi untuk freeze kamera
        }
    }

    private void PlayFireworks()
    {
        // Aktifkan partikel kembang api
        if (fireworksParticle != null)
        {
            fireworksParticle.Play();
        }

        // Mainkan SFX menang
        if (winSFX != null)
        {
            winSFX.Play();
        }

        // Pindah ke scene berikutnya setelah delay
        Invoke("LoadNextScene", delayBeforeNextScene);
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
