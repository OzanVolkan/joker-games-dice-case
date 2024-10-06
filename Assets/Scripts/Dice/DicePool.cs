using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePool : MonoBehaviour
{
    [SerializeField] private GameObject _dicePrefab;

    private List<GameObject> _dicePool = new();
    private readonly int _poolSize = 20;

    private void Start()
    {
        PoolDice(_poolSize);
    }


    private void PoolDice(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            var diceParent = Instantiate(_dicePrefab);
            diceParent.SetActive(false);
            _dicePool.Add(diceParent);

            var diceAnim = diceParent.GetComponentInChildren<Animator>();
            var diceParentTrans = diceParent.transform;

            DiceManager.Instance.DiceDic.Add(diceParentTrans, diceAnim);
        }
    }
}