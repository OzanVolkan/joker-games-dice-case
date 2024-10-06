using UnityEngine;
using UnityEngine.UI;

public class UIObjectMover : MonoBehaviour
{
    public GameObject uiImagePrefab; // UI Image prefab'ı
    public RectTransform targetUIPosition; // Hedef UI pozisyonu
    public Vector3 spawnPosition3D; // 3D dünyada oluşturulacak pozisyon
    public float moveDuration = 1.0f; // Hareket süresi
    public RectTransform canvasparent;

    private void Start()
    {
        // 3D pozisyonda UI Image objesini oluştur
        GameObject uiImageInstance = Instantiate(uiImagePrefab, canvasparent);
        RectTransform rectTransform = uiImageInstance.GetComponent<RectTransform>();

        // 3D pozisyonda oluştur
        Vector3 worldPosition = spawnPosition3D;
        Vector2 uiPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasparent, Camera.main.WorldToScreenPoint(worldPosition), null, out uiPosition);
        rectTransform.anchoredPosition = uiPosition;

        // Hedefe doğru hareket et
        StartCoroutine(MoveToTarget(rectTransform, targetUIPosition));
    }

    private System.Collections.IEnumerator MoveToTarget(RectTransform rectTransform, RectTransform target)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 targetPosition = target.anchoredPosition;
        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition; // Hedefe ulaştığında pozisyonu ayarla
    }
}