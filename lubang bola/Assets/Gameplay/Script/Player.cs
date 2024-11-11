using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    public Transform cameraTransform; // Referensi kamera

    public float minWalkSpeed = 0.1f; // Kecepatan minimum untuk memutar SFX jalan
    private bool isOnPlatform = false; // Menyimpan status apakah player berada di atas platform
    private bool walkSFXPlaying = false; // Menyimpan status apakah SFX jalan sedang dimainkan

    private AdiosManager adiosManager; // Referensi ke AdiosManager

    private void Awake()
    {
        // Mencari AdiosManager di scene menggunakan tag "Audio"
        adiosManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AdiosManager>();

        // Jika AdiosManager tidak ditemukan, tampilkan pesan error
        if (adiosManager == null)
        {
            Debug.LogError("AdiosManager belum ditemukan di scene. Pastikan terdapat GameObject dengan tag 'Audio' dan memiliki komponen AdiosManager.");
        }
    }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public IEnumerator BoostSpeed(float boostAmount, float boostDuration)
    {
        speed += boostAmount; // Tambahkan boost ke kecepatan
        yield return new WaitForSeconds(boostDuration); // Tunggu selama boost berlangsung
        speed -= boostAmount; // Kembalikan kecepatan normal
    }

    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0; right.y = 0;
        forward.Normalize(); right.Normalize();

        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;
        playerRb.AddForce(moveDirection * speed);

        // Cek apakah player bergerak di atas platform dengan kecepatan minimum
        if (isOnPlatform && playerRb.velocity.magnitude > minWalkSpeed)
        {
            if (!walkSFXPlaying && adiosManager != null)
            {
                adiosManager.PlaySFX(adiosManager.walk);
                walkSFXPlaying = true;
            }
        }
        else
        {
            walkSFXPlaying = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;

            // Mainkan SFX untuk walltouch saat menyentuh platform
            if (adiosManager != null)
            {
                adiosManager.PlaySFX(adiosManager.walltouch);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
            walkSFXPlaying = false;
        }
    }
}
