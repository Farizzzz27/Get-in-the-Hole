using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public GameObject bridgePrefab; // Prefab jembatan
    public Transform spawnPoint; // Titik di mana jembatan akan dimunculkan
    public Material offMaterial; // Material "off" yang akan digunakan setelah tombol ditekan
    private GameObject spawnedBridge; // Referensi untuk jembatan yang sudah dimunculkan
    private Renderer buttonRenderer; // Renderer untuk tombol

    void Start()
    {
        // Mengambil renderer tombol untuk mengubah material nanti
        buttonRenderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && spawnedBridge == null) // Pastikan hanya memunculkan sekali
        {
            spawnedBridge = Instantiate(bridgePrefab, spawnPoint.position, Quaternion.identity); // Spawn jembatan
            MovingPlatform platformScript = spawnedBridge.GetComponent<MovingPlatform>();
            platformScript.ActivatePlatform(); // Mengaktifkan pergerakan jembatan

            // Mengubah material tombol menjadi "off" setelah ditekan
            buttonRenderer.material = offMaterial;
        }
    }
}
