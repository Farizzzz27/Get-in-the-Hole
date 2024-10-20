using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f; // Kecepatan bola
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Ambil input dari pemain
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D atau Left/Right Arrow
        float moveVertical = Input.GetAxis("Vertical");     // W/S atau Up/Down Arrow

        // Buat vektor gerakan
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Panggil fungsi untuk menggerakkan bola
        MoveBall(movement);
    }

    void MoveBall(Vector3 movement)
    {
        // Hanya gerakkan bola jika ada input
        if (movement.magnitude > 0)
        {
            // Tambahkan gaya ke bola
            rb.AddForce(movement * speed);
        }
    }
}
