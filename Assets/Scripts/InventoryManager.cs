using System;
using System.Collections;
using Blocks;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private int _appleCount;
    private int _pearCount;
    private int _strawberryCount;

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

    private void OnAppleReward(int rewardCount)
    {
        StartCoroutine(IncrementAppleCount(rewardCount));
    }

    private void OnPearPearReward()
    {
    }

    private void OnStrawberryReward()
    {
    }


    private IEnumerator IncrementAppleCount(int rewardCount)
    {
        var targetCount = _appleCount + rewardCount;
        while (_appleCount < targetCount)
        {
            _appleCount++;

            yield return new WaitForSeconds(0.1f);
        }
    }
}