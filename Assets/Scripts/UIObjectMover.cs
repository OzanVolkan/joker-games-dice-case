using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectMover : MonoBehaviour
{
    public static UIObjectMover Instance;
    
    public float moveDuration = 1.0f;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public IEnumerator MoveToTarget(RectTransform rectTransform, RectTransform target)
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

        rectTransform.anchoredPosition = targetPosition;
    }
}