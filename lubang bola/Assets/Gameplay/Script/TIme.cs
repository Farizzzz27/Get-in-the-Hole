using UnityEngine;
using UnityEngine.UI;

public class TIme : MonoBehaviour
{
    public Text stopwatchText; // Referensi ke UI Text
    private float timeElapsed = 0f; // Menyimpan waktu yang telah berlalu
    private bool isRunning = true; // Langsung berjalan saat game dimulai

    private void Start()
    {
        // Pastikan stopwatch mulai berjalan saat game dimulai
        isRunning = true;
    }

    private void Update()
    {
        // Jika stopwatch sedang berjalan, tambah waktu yang telah berlalu
        if (isRunning)
        {
            timeElapsed += Time.deltaTime; // Time.deltaTime untuk waktu per frame
            UpdateStopwatchDisplay(timeElapsed);
        }
    }

    // Fungsi untuk menghentikan atau melanjutkan stopwatch
    public void ToggleStopwatch()
    {
        isRunning = !isRunning;
    }

    // Fungsi untuk mereset stopwatch
    public void ResetStopwatch()
    {
        timeElapsed = 0f;
        UpdateStopwatchDisplay(timeElapsed);
    }

    // Update tampilan stopwatch dengan format MM:SS:MS
    private void UpdateStopwatchDisplay(float timeToDisplay)
    {
        // Menghitung menit, detik, dan milidetik
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = Mathf.FloorToInt((timeToDisplay * 100) % 100);

        // Update UI Text dengan format "MM:SS:MS"
        stopwatchText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
