using UnityEngine;

public class tes : MonoBehaviour
{
    public GameObject newPrefab; // Prefab baru yang akan menggantikan objek lama
    private GameObject currentObject; // Objek lama yang akan diganti

    void Start()
    {
        currentObject = this.gameObject; // Mengambil referensi objek saat ini
        ReplaceObject();
    }

    public void ReplaceObject()
    {
        if (newPrefab != null && currentObject != null)
        {
            // Simpan posisi dan rotasi objek lama
            Vector3 oldPosition = currentObject.transform.position;
            Quaternion oldRotation = currentObject.transform.rotation;

            // Hancurkan objek lama
            Destroy(currentObject);

            // Buat instance dari prefab baru di posisi dan rotasi yang sama
            GameObject newObject = Instantiate(newPrefab, oldPosition, oldRotation);

            // Jika Anda ingin menerapkan komponen atau script yang sama dari objek lama ke yang baru,
            // pastikan script tersebut ada pada prefab yang baru.
        }
    }
}
