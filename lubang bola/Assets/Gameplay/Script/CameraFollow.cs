using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referensi bola
    public Vector3 offset = new Vector3(0, 5, -10); // Offset posisi kamera dari bola
    public float rotationSpeed = 50f; // Kecepatan rotasi kamera

    private float currentRotation = 0f; // Menyimpan nilai rotasi saat ini

    private void LateUpdate()
    {
        if (target != null)
        {
            // Periksa input untuk rotasi dengan kontrol yang dibalik
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                currentRotation -= rotationSpeed * Time.deltaTime; // Rotasi ke kiri
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                currentRotation += rotationSpeed * Time.deltaTime; // Rotasi ke kanan
            }

            // Terapkan rotasi pada offset
            Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
            Vector3 rotatedOffset = rotation * offset;

            // Atur posisi kamera berdasarkan offset yang sudah diputar
            transform.position = target.position + rotatedOffset;

            // Kamera selalu mengarah ke bola
            transform.LookAt(target.position);
        }
    }
}
