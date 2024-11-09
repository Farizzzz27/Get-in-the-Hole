using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    public GameObject[] skinPrefabs; // Array prefab skin
    public Quaternion[] skinRotations; // Array rotasi untuk setiap skin

    private GameObject currentSkin; // Objek skin yang sedang digunakan
    private int selectedSkinIndex;

    private void Start()
    {
        // Ambil index skin yang dipilih dari PlayerPrefs
        selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);

        // Pasang skin pada awal gameplay
        ApplySkin(selectedSkinIndex);
    }

    private void ApplySkin(int index)
    {
        // Hapus skin yang sedang ada
        if (currentSkin != null)
        {
            Destroy(currentSkin);
        }

        // Instansiasi prefab skin yang dipilih sebagai child dari pemain
        currentSkin = Instantiate(skinPrefabs[index], transform);
        currentSkin.transform.localPosition = Vector3.zero;

        // Terapkan rotasi jika ada di array skinRotations, atau gunakan rotasi default
        if (skinRotations.Length > index)
        {
            currentSkin.transform.localRotation = skinRotations[index];
        }
        else
        {
            currentSkin.transform.localRotation = Quaternion.identity; // Rotasi default
        }

        // Pastikan hanya model yang berubah, komponen utama pemain tetap ada di GameObject ini
    }
}
