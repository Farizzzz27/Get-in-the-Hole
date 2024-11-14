using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referensi bola
    public Vector3 offset = new Vector3(0, 5, -10); // Offset posisi kamera dari bola
    public float rotationSpeed = 50f; // Kecepatan rotasi kamera
    public float rotationX = 10f; // Variabel rotasi pada sumbu X untuk mengatur tinggi pandangan
    public float rotationY = 0f; // Variabel rotasi pada sumbu Y untuk mengatur rotasi horizontal
    public float rotationZ = 0f; // Variabel rotasi pada sumbu Z untuk mengatur rotasi roll
    public bool freezeCamera = false; // Status freeze kamera

    private float currentRotationY = 0f; // Menyimpan nilai rotasi saat ini pada sumbu Y

    private void LateUpdate()
    {
        if (target != null && !freezeCamera) // Tambahkan pengecekan freezeCamera
        {
            // Periksa input untuk rotasi pada sumbu Y (kiri dan kanan)
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                currentRotationY -= rotationSpeed * Time.deltaTime; // Rotasi ke kiri
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                currentRotationY += rotationSpeed * Time.deltaTime; // Rotasi ke kanan
            }

            // Terapkan rotasi pada offset dengan rotasi sumbu X, Y, dan Z
            Quaternion rotation = Quaternion.Euler(rotationX, currentRotationY + rotationY, rotationZ);
            Vector3 rotatedOffset = rotation * offset;

            // Atur posisi kamera berdasarkan offset yang sudah diputar
            transform.position = target.position + rotatedOffset;

            // Kamera selalu mengarah ke target
            transform.LookAt(target.position);
        }
    }
}
