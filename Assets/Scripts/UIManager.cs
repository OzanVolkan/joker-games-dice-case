using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public static event Action<int, List<int>> OnDiceRoll;
    
    [Header("Buttons")] [SerializeField] private Button _diceRollButton;
    
    [Header("Dice Settings")] [SerializeField]
    private TMP_Dropdown _diceCountDropdown;
    [SerializeField] private GameObject _diceInputFieldPrefab;
    [SerializeField] private Transform _inputFieldContentParent;


    private List<GameObject> _diceInputFields = new();


    private readonly int _maxDiceCount = 20;

    private void Start()
    {
        _diceCountDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        
        _diceRollButton.onClick.AddListener(DiceRollButtonOnClick);
        
        GenerateDropdownOptions();
        GenerateDiceInputFields(_maxDiceCount);
        ClearDiceInputFields();
    }


    #region DiceSettings

    private void GenerateDropdownOptions()
    {
        for (int i = 0; i < _maxDiceCount; i++)
        {
            var option = new TMP_Dropdown.OptionData
            {
                text = (i + 1).ToString()
            };
            _diceCountDropdown.options.Add(option);
        }
    }

    private void GenerateDiceInputFields(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newInputField = Instantiate(_diceInputFieldPrefab, _inputFieldContentParent);

            _diceInputFields.Add(newInputField);
        }
    }

    private void ClearDiceInputFields()
    {
        if (_diceInputFields == null) return;

        for (int i = 1; i < _diceInputFields.Count; i++)
        {
            _diceInputFields[i].SetActive(false);
        }
    }

    private void OnDropdownValueChanged(int value)
    {
        ClearDiceInputFields();

        var numberOfFields = _diceCountDropdown.value + 1;

        for (int i = 0; i < numberOfFields; i++)
        {
            _diceInputFields[i].SetActive(true);
        }
    }
    
    private int GetCurrentDiceCount()
    {
        return _diceCountDropdown.value + 1;
    }

    private List<int> GetActiveInputFieldValues()
    {
        var activeValues = new List<int>();

        for (int i = 0; i < _inputFieldContentParent.childCount; i++)
        {
            var child = _inputFieldContentParent.GetChild(i).gameObject;

            if (!child.activeInHierarchy) continue;
            
            var inputFieldText = child.GetComponent<DiceValueInputField>().InputField.text;

            if (string.IsNullOrEmpty(inputFieldText))
            {
                var randomValue = Random.Range(1, 7);
                activeValues.Add(randomValue);
            }
            else if (int.TryParse(inputFieldText, out int parsedValue))
            {
                activeValues.Add(parsedValue);
            }
            else
            {
                Debug.LogWarning("Invalid number input: " + inputFieldText);
            }
        }

        return activeValues;
    }
    
    #endregion

    #region ButtonOnClickMethods

    private void DiceRollButtonOnClick()
    {
        var currentDiceCount = GetCurrentDiceCount();

        var currentValues = GetActiveInputFieldValues();
        
        OnDiceRoll?.Invoke(currentDiceCount, currentValues);
    }

    #endregion
}