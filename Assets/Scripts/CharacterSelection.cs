using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public static event Action OnGameStart;
    public static event Action OnButtonClick;
    
    [Header("Panels")] 
    [SerializeField] private GameObject _diceControlPanel;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _charSelectionPanel;
    
    [Header("Buttons")]
    [SerializeField] private Button _leftArrow;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private Button _startButton;

    [Header("Show Models")]
    [SerializeField] private GameObject _peonChar;
    [SerializeField] private GameObject _carChar;

    [Header("Actual Models")] 
    [SerializeField] private GameObject _peonActual;
    [SerializeField] private GameObject _carActual;

    void Start()
    {
        _leftArrow.onClick.AddListener(OnLeftButtonClick);
        _rightArrow.onClick.AddListener(OnRightButtonClick);
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnLeftButtonClick()
    {
        OnButtonClick?.Invoke();
        
        _peonChar.SetActive(true);
        _peonActual.SetActive(true);
        _carChar.SetActive(false);
        _carActual.SetActive(false);
        _rightArrow.interactable = true;
        _leftArrow.interactable = false;
    }

    private void OnRightButtonClick()
    {
        OnButtonClick?.Invoke();

        _carChar.SetActive(true);
        _carActual.SetActive(true);
        _peonChar.SetActive(false);
        _peonActual.SetActive(false);
        _rightArrow.interactable = false;
        _leftArrow.interactable = true;
    }

    private void OnStartButtonClick()
    {
        //TODO: START SESİ OYNATILACAK!!! ***
        _diceControlPanel.SetActive(true);
        _inventoryPanel.SetActive(true);
        _charSelectionPanel.SetActive(false);
        
        OnGameStart?.Invoke();

        gameObject.SetActive(false);
    }
}
