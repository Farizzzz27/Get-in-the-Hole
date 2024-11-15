using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    public Transform cameraTransform; // Referensi kamera

    private AudioManager audioManager; // Menyimpan referensi ke AudioManager
    private bool isWalkingSFXPlaying = false; // Untuk mencegah SFX berjalan diputar berulang kali

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Mengambil referensi AudioManager di Awake
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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

        // Cek jika pemain memberikan input untuk bergerak
        if (verticalInput != 0 || horizontalInput != 0)
        {
            if (!isWalkingSFXPlaying)
            {
                isWalkingSFXPlaying = true;
                audioManager.PlaySFX(audioManager.walk); // Memutar SFX jalan
            }
        }
        else
        {
            isWalkingSFXPlaying = false; // Set status SFX jalan menjadi false jika tidak bergerak
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Memutar SFX walltouch saat tabrakan
        audioManager.PlaySFX(audioManager.walltouch); // Putar SFX walltouch saat terjadi tabrakan
    }

    // Metode untuk meningkatkan kecepatan player sementara
    public IEnumerator BoostSpeed(float boostAmount, float boostDuration)
    {
        speed += boostAmount; // Tambahkan kecepatan
        yield return new WaitForSeconds(boostDuration); // Tunggu selama boost berlangsung
        speed -= boostAmount; // Kembalikan kecepatan normal setelah durasi selesai
    }
}
