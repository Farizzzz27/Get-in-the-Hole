using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    public Material transparentMaterial; // Material transparan yang sudah kamu buat
    public Material originalMaterial; // Material asli tembok
    private Renderer wallRenderer; // Renderer tembok

    void Start()
    {
        wallRenderer = GetComponent<Renderer>();
        wallRenderer.material = originalMaterial; // Set material awal ke original
    }

    void OnTriggerEnter(Collider other)
    {
        // Cek jika yang masuk trigger adalah player
        if (other.CompareTag("Player"))
        {
            wallRenderer.material = transparentMaterial; // Ubah material menjadi transparan
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Kembali ke material asli saat player keluar dari area trigger
        if (other.CompareTag("Player"))
        {
            wallRenderer.material = originalMaterial;
        }
    }
}
