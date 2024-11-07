using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject pause;

    private void Awake()
    {
        pause.SetActive(false);
    }

    public void PauseGame(bool status)
    {
        pause.SetActive(status);

        if (status)
        {
            Time.timeScale = 0;
            // Optionally pause audio here if needed
        }
        else
        {
            Time.timeScale = 1;
            // Optionally resume audio here if needed
        }
    }

    // Update is called once per frame
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
        Time.timeScale = 1;
    }

}
