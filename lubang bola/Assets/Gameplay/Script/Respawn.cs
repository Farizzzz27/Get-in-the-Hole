using System.Collections;
using UnityEngine;
using DG.Tweening; // Pastikan Anda menambahkan ini untuk mengakses DoTween

public class RespawnZone : MonoBehaviour
{
    public Transform player;             // Referensi ke bola/player
    public Transform respawnPoint;       // Titik spawn atau posisi awal bola
    public CameraFollow cameraFollow;    // Referensi ke script CameraFollow
    public float respawnDuration = 1.0f; // Durasi transisi saat kembali ke posisi spawn

    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah yang masuk ke zona respawn adalah bola (Player)
        if (other.CompareTag("Player"))
        {
            // Panggil fungsi untuk memindahkan bola ke titik respawn dengan animasi
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        // Hentikan semua kecepatan atau gaya pada Rigidbody bola
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Gunakan DoTween untuk memindahkan bola ke titik respawn dengan smooth
        player.DOMove(respawnPoint.position, respawnDuration)
            .SetEase(Ease.InOutQuad); // Efek easing untuk perpindahan yang lebih halus

        // Opsional: Atur ulang rotasi bola ke rotasi awal dengan DoTween
        player.DORotateQuaternion(respawnPoint.rotation, respawnDuration)
            .SetEase(Ease.InOutQuad);
    }
}
