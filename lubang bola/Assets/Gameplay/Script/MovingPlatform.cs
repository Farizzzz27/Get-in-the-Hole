using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Titik awal platform
    public Transform pointB; // Titik tujuan platform
    public float speed = 2f; // Kecepatan gerakan platform
    private bool isMoving = false; // Status apakah platform bergerak atau tidak

    public void ActivatePlatform()
    {
        isMoving = true; // Mengaktifkan pergerakan platform
    }

    void Update()
    {
        if (isMoving)
        {
            // Platform bergerak dari titik A ke titik B
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

            // Memeriksa jika platform sudah sampai di titik B
            if (transform.position == pointB.position)
            {
                isMoving = false; // Menonaktifkan pergerakan setelah sampai
            }
        }
    }
}
