using UnityEngine;
using DG.Tweening;

public class Animasi1 : MonoBehaviour
{
    private Vector3 fromPosition = new Vector3(0, -80, 0); // Posisi awal
    private float duration = 0.5f; // Durasi perpindahan
    private float delay = 0.1f; // Delay sebelum animasi mulai
    public Ease easeType = Ease.OutQuad; // Ease function

    private Vector3 originalPosition; // Menyimpan posisi awal object di scene
    private Tween moveTween; // Menyimpan Tween yang dibuat

    void Start()
    {
        // Simpan posisi awal object di scene
        originalPosition = transform.localPosition;

        // Ubah posisi y saja, tetapi pertahankan posisi x dan z dari object di scene
        fromPosition = new Vector3(originalPosition.x, -80, originalPosition.z);

        // Pindahkan object ke fromPosition
        transform.localPosition = fromPosition;

        // Buat dan simpan Tween animasi perpindahan
        moveTween = transform.DOLocalMove(originalPosition, duration)
                                .SetDelay(delay)        // Menambahkan delay
                                .SetEase(easeType)      // Menggunakan ease OutQuad
                                .SetAutoKill(false);    // Jangan auto-kill agar bisa digunakan lagi
    }


    // Fungsi untuk memutar kembali animasi secara mundur
    public void PlayBackward()
    {
        if (moveTween != null)
        {
            moveTween.PlayBackwards();
        }
    }
}
