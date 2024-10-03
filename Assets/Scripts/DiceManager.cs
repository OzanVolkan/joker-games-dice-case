using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance;

    [SerializeField] private Transform _camTransform;
    [SerializeField] private List<int> _tempValues = new();

    #region DiceMinMaxValues

    private readonly float _diceYValue = 16.7f;
    private readonly float _diceZOffset = 22.5f;

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
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetPooledDice();
        }
    }

    private void GetPooledDice()
    {
        for (int i = 0; i < _tempValues.Count; i++)
        {
            foreach (KeyValuePair<Transform, Animator> entry in _diceDic)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (!key.gameObject.activeInHierarchy)
                {
                    key.gameObject.SetActive(true);

                    DiceRandomPosition(key);
                    
                    //TODO: ANİMASYONLARI ATAMA YAP. ANİMATION CONTROLLERDAN TRIGGERLARI BAGLA
                    
                     GenerateDiceAnims(_tempValues[i]);

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

    private void RollDice(List<int> diceValues)
    {
        var diceCount = diceValues.Count;

        for (int i = 0; i < diceCount; i++)
        {
        }
    }

    public string GenerateDiceAnims(int value)
    {
        // int randomNumber = Random.Range(1, 7); // 1 ile 6 arasında rastgele sayı
        
        var randomLetter =
            _animTriggerLetters[Random.Range(0, _animTriggerLetters.Length)]; // A, B veya C harflerinden biri

        string animTriggerName = value + randomLetter;
        // randomAnimTriggers.Add(randomString);
        
        Debug.Log(animTriggerName);

        return animTriggerName;
    }
}