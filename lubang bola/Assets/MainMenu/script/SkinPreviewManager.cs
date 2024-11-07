using UnityEngine;

public class SkinPreviewManager : MonoBehaviour
{
    public GameObject[] skinPreviews; // Array dari objek preview 3D untuk setiap skin
    private int currentPreviewIndex = -1; // Indeks untuk skin yang sedang ditampilkan

    // Fungsi untuk menampilkan preview skin berdasarkan indeks
    public void ShowSkinPreview(int index)
    {
        // Nonaktifkan skin preview yang sebelumnya aktif
        if (currentPreviewIndex >= 0 && currentPreviewIndex < skinPreviews.Length)
        {
            skinPreviews[currentPreviewIndex].SetActive(false);
        }

        // Aktifkan skin preview yang baru dipilih
        if (index >= 0 && index < skinPreviews.Length)
        {
            skinPreviews[index].SetActive(true);
            currentPreviewIndex = index;
        }
    }
}
