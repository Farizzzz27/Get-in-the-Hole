using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlatform : MonoBehaviour
{
    public float boostAmount = 20f;  // Jumlah boost kecepatan
    public float boostDuration = 1f; // Lama boost berlangsung

    // Trigger untuk mendeteksi bola
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.StartCoroutine(playerScript.BoostSpeed(boostAmount, boostDuration));
            }
        }
    }
}
