using System.Collections.Generic;
using UnityEngine;

namespace Dice
{
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
                var diceCollider = diceParent.GetComponentInChildren<Collider>();

                DiceManager.Instance.DiceDic.Add(diceParentTrans, diceAnim);
                DiceManager.Instance.DiceColliders.Add(diceCollider);
            }
        }
    }
}