using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public ParticleSystem fireworksParticle;
    public AudioSource winSFX;
    public float delayBeforeNextScene = 3f;
    public string nextSceneName; // Masukkan "Level2" jika ini adalah Level1

    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;
            PlayFireworks();
            FreezeCamera(); // Memanggil fungsi freeze kamera
            SaveProgress();
        }
    }

    private void PlayFireworks()
    {
        if (fireworksParticle != null) fireworksParticle.Play();
        if (winSFX != null) winSFX.Play();
        Invoke("LoadNextScene", delayBeforeNextScene);
    }

    private void FreezeCamera()
    {
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.freezeCamera = true; // Menghentikan rotasi dan posisi kamera
        }
    }

    private void SaveProgress()
    {
        string currentLevel = SceneManager.GetActiveScene().name; // Nama level misalnya "Level1"
        PlayerPrefs.SetInt(currentLevel, 1); // Simpan status level selesai untuk nama scene saat ini
        PlayerPrefs.Save(); // Pastikan tersimpan
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName); // Pastikan nextSceneName sudah diisi, misalnya "Level2"
    }
}
