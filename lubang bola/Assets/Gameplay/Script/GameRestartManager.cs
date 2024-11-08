using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameRestartManager : MonoBehaviour
{
    public Transform playerTransform;  // Referensi ke transformasi player
    public Image fadeImage;            // UI Image untuk fade-out
    public Text attemptText;           // UI Text untuk menampilkan jumlah percobaan
    public float fadeDuration = 1f;    // Durasi fade-out
    public float fallThreshold = -50f; // Batas nilai Y sebelum restart
    public AudioSource loseSFX;        // AudioSource untuk SFX kalah

    private bool isRestarting = false; // Pengaman agar restart hanya terjadi sekali
    private int attemptCount = 0;      // Menghitung jumlah percobaan

    private void Start()
    {
        // Pastikan fadeImage transparan di awal
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        // Mengambil nilai attemptCount dari PlayerPrefs, mulai dari 0 jika pertama kali
        attemptCount = PlayerPrefs.GetInt("AttemptCount", 1);

        // Menampilkan attempt count di UI
        UpdateAttemptText();
    }

    private void Update()
    {
        // Cek apakah player jatuh di bawah threshold dan belum dalam proses restart
        if (playerTransform.position.y < fallThreshold && !isRestarting)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        isRestarting = true; // Tandai bahwa proses restart sudah dimulai

        // Menambah nilai percobaan
        attemptCount++;
        PlayerPrefs.SetInt("AttemptCount", attemptCount); // Simpan nilai baru di PlayerPrefs

        // Perbarui teks attempt di UI
        UpdateAttemptText();

        // Mainkan SFX kalah
        if (loseSFX != null)
        {
            loseSFX.Play();
        }

        // Play backward semua animasi DoTween di scene
        DOTween.PlayBackwardsAll();

        // Mulai fade-out layar
        fadeImage.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            // Setelah fade-out selesai, restart game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    private void UpdateAttemptText()
    {
        // Update teks jumlah percobaan di UI Text
        if (attemptText != null)
        {
            attemptText.text = "Attempt: " + attemptCount.ToString();
        }
    }

    private void OnApplicationQuit()
    {
        // Hapus nilai attempt saat aplikasi ditutup
        PlayerPrefs.DeleteKey("AttemptCount");
    }
}
