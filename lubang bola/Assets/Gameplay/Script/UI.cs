using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject pause;

    private void Awake()
    {
        pause.SetActive(false);

        // Pastikan kursor terkunci dan tidak terlihat saat masuk gameplay
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame(bool status)
    {
        pause.SetActive(status);

        if (status)
        {
            Time.timeScale = 0;

            // Tampilkan kursor saat game di-pause
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;

            // Sembunyikan kursor saat kembali ke gameplay
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pause.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

        // Pastikan waktu kembali normal dan kursor terlihat di main menu
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
