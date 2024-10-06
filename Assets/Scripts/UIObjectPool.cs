using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectPool : MonoBehaviour
{
    public static UIObjectPool Instance;
    
    [SerializeField] private GameObject _appleImagePefab;
    [SerializeField] private GameObject _pearImagePefab;
    [SerializeField] private GameObject _strawberryImagePefab;
    [SerializeField] private RectTransform _canvasParent;

    private List<GameObject> _applePool = new();
    private List<GameObject> _pearPool = new();
    private List<GameObject> _strawberryPool = new();

    private readonly int _poolSize = 5;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    void Start()
    {
        PoolObjects(_poolSize);
    }

    private void PoolObjects(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            var appleImageInstance = Instantiate(_appleImagePefab, _canvasParent);
            appleImageInstance.SetActive(false);
            _applePool.Add(appleImageInstance);
            
            var pearImageInstance = Instantiate(_pearImagePefab, _canvasParent);
            pearImageInstance.SetActive(false);
            _pearPool.Add(pearImageInstance);

            var strawberryImageInstance = Instantiate(_strawberryImagePefab, _canvasParent);
            strawberryImageInstance.SetActive(false);
            _strawberryPool.Add(strawberryImageInstance);

        }
    }

    public RectTransform GetPooledUIElement(string type, Vector3 spawnPos3D)
    {
        List<GameObject> pool = new List<GameObject>();

        switch (type)
        {
            case "Apple":
                pool = _applePool;
                break;
            case "Pear":
                pool = _pearPool;
                break;
            case "Strawberry":
                pool = _strawberryPool;
                break;
        }

        foreach (var item in pool)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);

                RectTransform rectTrans = item.GetComponent<RectTransform>();
                
                SetUIElementPos(rectTrans, _canvasParent, spawnPos3D);
                
                return rectTrans;
            }
        }

        return null;
    }

    private void SetUIElementPos(RectTransform objRectTrans, RectTransform parentRectTrans, Vector3 spawnPos)
    {
        // Vector2 uiElementPos;
        //
        // if (Camera.main != null)
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTrans,
        //         Camera.main.WorldToScreenPoint(spawnPos), null, out uiElementPos);
        //
        // objRectTrans.anchoredPosition = uiElementPos;
        
        Vector2 uiElementPos;

        if (Camera.main == null) return;
        
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(spawnPos);

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTrans, screenPoint, null, out uiElementPos))
        {
            objRectTrans.anchoredPosition = uiElementPos;
        }
    }
}
