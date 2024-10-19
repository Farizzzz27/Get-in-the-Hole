using UnityEngine;
using DG.Tweening;

public class MovingObstacle : MonoBehaviour
{
    public float moveDistance = 5f;   // Jarak yang ditempuh object (kiri-kanan)
    public float moveDuration = 2f;   // Durasi waktu untuk bergerak dari satu sisi ke sisi lainnya
    public float startDelay = 0.5f;   // Delay sebelum animasi mulai
    public Ease moveEase = Ease.Linear; // Tipe ease untuk pergerakan

    private Vector3 originalPosition; // Posisi awal object

    void Start()
    {
        // Simpan posisi awal object
        originalPosition = transform.position;

        // Mulai animasi pergerakan kiri-kanan setelah delay
        MoveLeftRight();
    }

    // Fungsi untuk menggerakkan object dari kiri ke kanan dan sebaliknya
    void MoveLeftRight()
    {
        // Pindahkan object ke kiri terlebih dahulu, lalu ke kanan secara berulang
        transform.DOMoveX(originalPosition.x - moveDistance, moveDuration)
            .SetEase(moveEase)                 // Atur ease function untuk animasi
            .SetLoops(-1, LoopType.Yoyo)       // Set animasi untuk looping tanpa batas (ping-pong)
            .SetRelative()                     // Gerakkan relatif terhadap posisi saat ini
            .SetDelay(startDelay);             // Tambahkan delay sebelum mulai animasi
    }
}
