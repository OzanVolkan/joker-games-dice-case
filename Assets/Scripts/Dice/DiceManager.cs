using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dice
{
    public class DiceManager : MonoBehaviour
    {
        public static DiceManager Instance;

        [SerializeField] private Transform _camTransform;

        #region DiceMinMaxValues

        private readonly float _diceYValue = 16.7f;
        private readonly float _diceZOffset = 5;
        private readonly float _diceZMaxOffset = 15f;
        private readonly float _minX = -40f;
        private readonly float _maxX = -21.5f;

        #endregion

        private string[] _animTriggerLetters = new[] { "A", "B", "C" };
        private Dictionary<Transform, Animator> _diceDic = new();
        private List<Collider> _diceColliders = new();
        private int _tempDiceIndex;

        #region Properties

        public Dictionary<Transform, Animator> DiceDic
        {
            get => _diceDic;
            set => _diceDic = value;
        }

        public List<Collider> DiceColliders
        {
            get => _diceColliders;
            set => _diceColliders = value;
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
            UIManager.OnDiceRoll += GetPooledDice;
        }

        private void OnDisable()
        {
            UIManager.OnDiceRoll -= GetPooledDice;
        }

        private void GetPooledDice(int diceCount, List<int> diceValues)
        {
            for (int i = 0; i < diceCount; i++)
            {
                foreach (KeyValuePair<Transform, Animator> entry in _diceDic)
                {
                    var key = entry.Key;
                    var value = entry.Value;

                    if (!key.gameObject.activeInHierarchy)
                    {
                        key.gameObject.SetActive(true);

                        DiceRandomPosition(key);

                        value.SetTrigger(GenerateDiceAnims(diceValues[i]));

                        _diceColliders[_tempDiceIndex].enabled = true;

                        _tempDiceIndex++;

                        break;
                    }
                }
            }

            _tempDiceIndex = 0;
        }

        private void DiceRandomPosition(Transform diceTrans)
        {
            var randX = Random.Range(_minX, _maxX);

            var camZ = _camTransform.position.z;

            var diceZVal = camZ - _diceZOffset;

            var randZ = Random.Range(diceZVal, diceZVal + _diceZMaxOffset);

            var dicePos = new Vector3(randX, _diceYValue, randZ);

            diceTrans.position = dicePos;
        }

        private string GenerateDiceAnims(int value)
        {
            var randomLetter =
                _animTriggerLetters[Random.Range(0, _animTriggerLetters.Length)]; // A, B or C

            var animTriggerName = value + randomLetter;

            return animTriggerName;
        }
    }
}