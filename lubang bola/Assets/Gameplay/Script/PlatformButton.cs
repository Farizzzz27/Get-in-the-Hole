using UnityEngine;

public class PaltformButtonn : MonoBehaviour
{
    public GameObject bridgePrefab; // Prefab jembatan
    public Transform spawnPoint; // Titik di mana jembatan akan dimunculkan
    private GameObject spawnedBridge; // Referensi untuk jembatan yang sudah dimunculkan

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && spawnedBridge == null) // Pastikan hanya memunculkan sekali
        {
            spawnedBridge = Instantiate(bridgePrefab, spawnPoint.position, Quaternion.identity); // Spawn jembatan
            MovingPlatform platformScript = spawnedBridge.GetComponent<MovingPlatform>();
            platformScript.ActivatePlatform(); // Mengaktifkan pergerakan jembatan
        }
    }
}
