using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public float respawnDelay = 2.0f;             // Waktu tunggu sebelum restart
    public GameObject fragmentPrefab;             // Prefab pecahan bola
    public int fragmentCount = 10;                // Jumlah kepingan bola
    public float explosionForce = 500.0f;         // Kekuatan ledakan untuk pecahan bola
    public float explosionRadius = 2.0f;          // Radius ledakan pecahan bola

    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang memasuki collider adalah bola (Player)
        if (other.CompareTag("Player"))
        {
            // Panggil fungsi untuk menghancurkan bola dengan efek pecahan dan restart level
            StartCoroutine(DestroyAndRestart(other.gameObject));
        }
    }

    private IEnumerator DestroyAndRestart(GameObject player)
    {
        // Tampilkan efek pecahan
        CreateFragments(player.transform.position);

        // Hancurkan bola asli (Player)
        Destroy(player);

        // Tunggu beberapa detik sebelum restart
        yield return new WaitForSeconds(respawnDelay);

        // Restart level saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CreateFragments(Vector3 position)
    {
        // Buat beberapa pecahan di posisi bola
        for (int i = 0; i < fragmentCount; i++)
        {
            // Spawn pecahan bola di posisi bola
            GameObject fragment = Instantiate(fragmentPrefab, position, Random.rotation);

            // Tambahkan gaya ledakan ke pecahan
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, position, explosionRadius);
            }

            // Hancurkan pecahan setelah beberapa detik
            Destroy(fragment, 3.0f); // Menghancurkan pecahan setelah 3 detik untuk efek yang bersih
        }
    }
}
