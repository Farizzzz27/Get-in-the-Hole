using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;    // Panel untuk Main Menu
    public GameObject settingsPanel;    // Panel untuk Pengaturan

    private void Start()
    {
        // Pastikan kursor terlihat di menu utama
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Membebaskan kursor
    }

    // Fungsi untuk membuka Panel Pengaturan
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // Fungsi untuk menutup Panel Pengaturan dan kembali ke Main Menu
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
