using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dice Settings")] [SerializeField]
    private TMP_Dropdown _diceCountDropdown;
    [SerializeField] private GameObject _diceInputFieldPrefab;
    [SerializeField] private Transform _inputFieldContentParent;


    private List<GameObject> _diceInputFields = new();


    private readonly int _maxDiceCount = 20;

    void Start()
    {
        _diceCountDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        
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

        // Seçilen sayı kadar input field oluştur
        for (int i = 0; i < numberOfFields; i++)
        {
            _diceInputFields[i].SetActive(true);
        }
    }

    #endregion
}