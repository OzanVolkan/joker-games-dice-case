using System;
using System.Collections;
using Blocks;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public event Action<int, int, int> OnUpdateRewardCount;

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
        Block.OnClaimReward += OnAppleReward;
    }

    private void OnDisable()
    {
        Block.OnClaimReward -= OnAppleReward;
    }

    private void OnAppleReward(int rewardCount, Vector3 blockPos)
    {
        print("girdii");
        StartCoroutine(IncrementAppleCount(rewardCount, blockPos));
    }

    private void OnPearPearReward()
    {
    }

    private void OnStrawberryReward()
    {
    }


    private IEnumerator IncrementAppleCount(int rewardCount, Vector3 blockPos)
    {
        var targetCount = _appleCount + rewardCount;
        while (_appleCount < targetCount)
        {
            var elementRectTrans = UIObjectPool.Instance.GetPooledUIElement(_appleType, blockPos);

            yield return UIObjectMover.Instance.MoveToTarget(elementRectTrans, UIManager.Instance.AppleUITrans);

            _appleCount++;

            OnUpdateRewardCount?.Invoke(_appleCount, _pearCount, _strawberryCount);

            //TODO: BURADA SES OYNATILACAK WUHUUUUUUUU **** 1!!!!11

            yield return new WaitForSeconds(0.1f);
        }
    }
}