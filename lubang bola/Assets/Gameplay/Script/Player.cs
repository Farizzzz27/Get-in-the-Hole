using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    public Transform cameraTransform; // Referensi kamera

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
    }
}
