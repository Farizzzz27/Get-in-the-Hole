using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    public Transform cameraTransform; // Referensi kamera

    // Audio
    public AudioSource walkSFX; // AudioSource untuk SFX jalan
    public AudioSource collisionSFX; // AudioSource untuk SFX tabrakan
    public float minWalkSpeed = 0.1f; // Kecepatan minimum untuk memutar SFX jalan

    private bool isOnPlatform = false; // Menyimpan status apakah player berada di atas platform

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        if (walkSFX != null)
        {
            walkSFX.loop = true; // Set audio jalan agar di-loop
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Input untuk pergerakan vertikal dan horizontal
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Ambil arah forward dan right dari kamera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Normalisasi arah forward dan right untuk memastikan panjang vektornya 1
        forward.y = 0;  // Set Y ke 0 agar player hanya bergerak di bidang horizontal
        right.y = 0;    // Set Y ke 0 untuk hal yang sama

        forward.Normalize();
        right.Normalize();

        // Tentukan arah gerakan berdasarkan input player dan orientasi kamera
        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;

        // Tambahkan gaya ke player untuk pergerakan
        playerRb.AddForce(moveDirection * speed);

        // Cek apakah player bergerak di atas platform dengan kecepatan minimum untuk memainkan SFX jalan
        if (isOnPlatform && playerRb.velocity.magnitude > minWalkSpeed)
        {
            if (!walkSFX.isPlaying)
            {
                walkSFX.Play(); // Mulai SFX jalan jika tidak sedang diputar
            }
        }
        else
        {
            if (walkSFX.isPlaying)
            {
                walkSFX.Stop(); // Hentikan SFX jalan jika tidak bergerak atau tidak di atas platform
            }
        }
    }

    public IEnumerator BoostSpeed(float boostAmount, float boostDuration)
    {
        speed += boostAmount; // Tambahkan boost ke kecepatan
        yield return new WaitForSeconds(boostDuration); // Tunggu selama boost berlangsung
        speed -= boostAmount; // Kembalikan kecepatan normal
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Cek jika SFX sedang bermain, maka hentikan dulu untuk memungkinkan pemutaran ulang
        if (collisionSFX != null)
        {
            collisionSFX.Play(); // Putar ulang SFX tabrakan
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Cek apakah player sedang berada di atas platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Set isOnPlatform ke false ketika player tidak lagi di atas platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
        }
    }
}
