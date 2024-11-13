using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        UpdateLevelAccess();
    }

    private void UpdateLevelAccess()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            string currentLevelKey = "Level" + (i + 1); // Nama kunci untuk level, misalnya "Level1", "Level2", dst.
            bool isUnlocked = i == 0 || PlayerPrefs.GetInt("Level" + i, 0) == 1;

            levelButtons[i].interactable = isUnlocked;
            levelButtons[i].GetComponentInChildren<Text>().color = isUnlocked ? Color.white : Color.gray;
        }
    }
}
