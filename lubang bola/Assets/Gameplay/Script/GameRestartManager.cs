using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameRestartManager : MonoBehaviour
{
    public Transform playerTransform; // Referensi ke transformasi player
    public Image fadeImage;           // UI Image untuk fade-out
    public float fadeDuration = 1f;   // Durasi fade-out
    public float fallThreshold = -50f; // Batas nilai Y sebelum restart

    private bool isRestarting = false; // Pengaman agar restart hanya terjadi sekali
    private AudioManager audioManager; // Referensi ke AudioManager

    private void Awake()
    {
        // Mengambil referensi AudioManager
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        // Pastikan fadeImage transparan di awal
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
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

        // Mainkan SFX kalah menggunakan AudioManager
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.lose); // Memainkan SFX kalah
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
}
