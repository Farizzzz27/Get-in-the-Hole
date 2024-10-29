using UnityEngine;
using UnityEngine.UI;

public class BackgroundThemeChanger : MonoBehaviour
{
    public Image backgroundImage; // Komponen Image di Canvas untuk background
    public Sprite[] themes;       // Array untuk 4 gambar tema (Sprite)

    void Start()
    {
        // Memastikan bahwa ada 4 tema dan backgroundImage sudah diassign
        if (themes.Length == 4 && backgroundImage != null)
        {
            // Memilih satu tema secara acak dari array
            int randomIndex = Random.Range(0, themes.Length);
            backgroundImage.sprite = themes[randomIndex]; // Mengubah sprite Image
        }
        else
        {
            Debug.LogWarning("Pastikan ada tepat 4 tema di array dan komponen Image sudah diisi.");
        }
    }
}
