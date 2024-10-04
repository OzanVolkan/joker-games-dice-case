using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance;

    [SerializeField] private Transform _camTransform;

    #region DiceMinMaxValues

    private readonly float _diceYValue = 16.7f;
    private readonly float _diceZOffset = 10;

    #endregion

    private string[] _animTriggerLetters = new[] { "A", "B", "C" };
    private Dictionary<Transform, Animator> _diceDic = new();

    #region Properties

    public Dictionary<Transform, Animator> DiceDic
    {
        get => _diceDic;
        set => _diceDic = value;
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

                    break;
                }
            }
        }
    }

    private void DiceRandomPosition(Transform diceTrans)
    {
        var randX = Random.Range(-40f, -21.5f);

        var camZ = _camTransform.position.z;

        var diceZVal = camZ - _diceZOffset;

        var randZ = Random.Range(diceZVal, diceZVal + 25f);

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