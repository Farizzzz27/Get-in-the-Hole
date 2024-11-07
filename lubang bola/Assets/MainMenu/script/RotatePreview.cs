using UnityEngine;

public class RotatePreview : MonoBehaviour
{
    public float rotationSpeed = 50f; // Kecepatan rotasi, bisa disesuaikan

    void Update()
    {
        // Rotasi objek di sumbu Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
