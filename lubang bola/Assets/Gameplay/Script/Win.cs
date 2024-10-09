using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public ParticleSystem fireworksParticle; // Referensi ke partikel kembang api
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
        }
    }

    private void PlayFireworks()
    {
        // Aktifkan partikel kembang api
        if (fireworksParticle != null)
        {
            fireworksParticle.Play();
        }

        // Pindah ke scene berikutnya setelah delay
        Invoke("LoadNextScene", delayBeforeNextScene);
    }

    private void LoadNextScene()
    {
        // Pindah ke scene berikutnya
        SceneManager.LoadScene(nextSceneName);
    }
}
