using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // Ukuran saat hover
    public float animationSpeed = 0.2f; // Kecepatan transisi animasi

    private Vector3 originalScale; // Skala awal tombol

    private void Start()
    {
        originalScale = transform.localScale; // Simpan skala awal
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale));
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 targetScale)
    {
        float elapsedTime = 0f;
        Vector3 startingScale = transform.localScale;

        while (elapsedTime < animationSpeed)
        {
            transform.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / animationSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
