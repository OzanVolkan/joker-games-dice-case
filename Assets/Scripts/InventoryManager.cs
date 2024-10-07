using System;
using System.Collections;
using Blocks;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public event Action<int, int, int> OnUpdateRewardCount;
    public event Action OnMovementEnd;


    private int _appleCount;
    private int _pearCount;
    private int _strawberryCount;

    #region Type

    private readonly string _appleType = "Apple";
    private readonly string _pearType = "Pear";
    private readonly string _strawberryType = "Strawberry";

    #endregion

    #region Properties

    public int AppleCount
    {
        get => _appleCount;
        set => _appleCount = value;
    }

    public int PearCount
    {
        get => _pearCount;
        set => _pearCount = value;
    }

    public int StrawberryCount
    {
        get => _strawberryCount;
        set => _strawberryCount = value;
    }

    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void OnEnable()
    {
        AppleBlock.OnAppleCollect += OnAppleReward;
        PearBlock.OnPearCollect += OnPearPearReward;
        StrawberryBlock.OnStrawberryCollect += OnStrawberryReward;
        EmptyBlock.OnEmptyCollect += OnEmpty;
    }

    private void OnDisable()
    {
        AppleBlock.OnAppleCollect -= OnAppleReward;
        PearBlock.OnPearCollect -= OnPearPearReward;
        StrawberryBlock.OnStrawberryCollect -= OnStrawberryReward;
        EmptyBlock.OnEmptyCollect -= OnEmpty;
    }

    private void OnAppleReward(int rewardCount, Vector3 blockPos)
    {
        StartCoroutine(IncrementItemCount(rewardCount, blockPos, () => _appleCount, val => _appleCount = val,
            UIManager.Instance.AppleUITrans, _appleType));
    }

    private void OnPearPearReward(int rewardCount, Vector3 blockPos)
    {
        StartCoroutine(IncrementItemCount(rewardCount, blockPos, () => _pearCount, val => _pearCount = val,
            UIManager.Instance.PearUITrans, _pearType));
    }

    private void OnStrawberryReward(int rewardCount, Vector3 blockPos)
    {
        StartCoroutine(IncrementItemCount(rewardCount, blockPos, () => _strawberryCount, val => _strawberryCount = val,
            UIManager.Instance.StrawberryUITrans, _strawberryType));
    }

    private void OnEmpty()
    {
        OnMovementEnd?.Invoke();
    }


    private IEnumerator IncrementItemCount(int rewardCount, Vector3 blockPos, Func<int> getCount, Action<int> setCount,
        RectTransform targetTrans, string type)
    {
        var targetCount = getCount() + rewardCount;
        while (getCount() < targetCount)
        {
            var elementRectTrans = UIObjectPool.Instance.GetPooledUIElement(type, blockPos);

            yield return UIObjectMover.Instance.MoveToTarget(elementRectTrans, targetTrans);
            StartCoroutine(UIObjectMover.Instance.PopEffect(targetTrans));

            elementRectTrans.gameObject.SetActive(false);

            setCount(getCount() + 1);

            OnUpdateRewardCount?.Invoke(_appleCount, _pearCount, _strawberryCount);

            //TODO: BURADA SES OYNATILACAK WUHUUUUUUUU **** 1!!!!11

            yield return new WaitForSeconds(0.1f);
        }
        
        OnMovementEnd?.Invoke();
    }
}