using UnityEngine;

public class SkinSelectionManager : MonoBehaviour
{
    public GameObject[] skinPrefabs;
    public Quaternion[] previewRotations;
    public Transform skinPreviewContainer;
    private GameObject currentPreview;
    private int selectedSkinIndex = 0;

    private void Start()
    {
        // Cek apakah ada skin yang sudah disimpan, jika tidak, set skin default ke index 0
        if (!PlayerPrefs.HasKey("SelectedSkin"))
        {
            selectedSkinIndex = 0;
            PlayerPrefs.SetInt("SelectedSkin", selectedSkinIndex);
            PlayerPrefs.Save();
        }
        else
        {
            // Ambil skin yang disimpan jika sudah ada
            selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin");
        }

        // Tampilkan preview skin default atau skin yang sudah dipilih
        UpdatePreview(selectedSkinIndex);
    }

    public void UpdatePreview(int index)
    {
        if (currentPreview != null)
            Destroy(currentPreview);

        currentPreview = Instantiate(skinPrefabs[index], skinPreviewContainer);
        currentPreview.transform.localPosition = Vector3.zero;

        if (previewRotations.Length > index)
        {
            currentPreview.transform.localRotation = previewRotations[index];
        }
        else
        {
            currentPreview.transform.localRotation = Quaternion.identity;
        }

        selectedSkinIndex = index;
    }

    public void SelectSkin()
    {
        PlayerPrefs.SetInt("SelectedSkin", selectedSkinIndex);
        PlayerPrefs.Save();
        Debug.Log("Skin dengan index " + selectedSkinIndex + " telah dipilih dan disimpan.");
    }
}
