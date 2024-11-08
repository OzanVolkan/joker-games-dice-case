using System;
using System.Collections.Generic;
using Dice;
using Managers;
using PlayerControl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class UIManager : MonoBehaviour, IDragHandler
    {
        public static UIManager Instance;
    
        public static event Action<int, List<int>> OnDiceRoll;
        public static Action<List<int>> OnForwardMovement;
        public static Action OnToggleMute;

        private Action _onMovementEndAction;

        [Header("Buttons")] [SerializeField] private Button _diceRollButton;
        [SerializeField] private Button _muteButton;

        [Header("Dice Settings")] [SerializeField]
        private GameObject _diceControlPanel;

        [SerializeField] private TMP_Dropdown _diceCountDropdown;
        [SerializeField] private GameObject _diceInputFieldPrefab;
        [SerializeField] private Transform _inputFieldContentParent;
        [SerializeField] private ScrollRect _inputFieldsScrollRect;

        [Header("Reward UI")] [SerializeField] private RectTransform _appleUITrans;
        [SerializeField] private RectTransform _pearUITrans;
        [SerializeField] private RectTransform _strawberryUITrans;
        [SerializeField] private TextMeshProUGUI _appleCountText;
        [SerializeField] private TextMeshProUGUI _pearCountText;
        [SerializeField] private TextMeshProUGUI _strawberyyCountText;

        private List<GameObject> _diceInputFields = new();
        private readonly int _maxDiceCount = 20;

        #region Properties

        public RectTransform AppleUITrans => _appleUITrans;
        public RectTransform PearUITrans => _pearUITrans;
        public RectTransform StrawberryUITrans => _strawberryUITrans;

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
            CharacterSelection.OnGameStart += SubscribeOnGameStart;
        }

        private void OnDisable()
        {
            InventoryManager.OnMovementEnd -= _onMovementEndAction;
            InventoryManager.OnUpdateRewardCount -= RewardCountsUIUpdate;
        }

        private void Start()
        {
            _diceCountDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

            _diceRollButton.onClick.AddListener(DiceRollButtonOnClick);
            _muteButton.onClick.AddListener(() => OnToggleMute?.Invoke());

            GenerateDropdownOptions();
            GenerateDiceInputFields(_maxDiceCount);
            ClearDiceInputFields();
        }

        private void RewardCountsUIUpdate(int appleCount, int pearCount, int strawberryCount)
        {
            _appleCountText.text = appleCount.ToString();
            _pearCountText.text = pearCount.ToString();
            _strawberyyCountText.text = strawberryCount.ToString();
        }

        private void SubscribeOnGameStart()
        {
            _onMovementEndAction = () => ButtonInteractableState(true, _diceRollButton);
            InventoryManager.OnMovementEnd += _onMovementEndAction;
        
            InventoryManager.OnUpdateRewardCount += RewardCountsUIUpdate;
        
            RewardCountsUIUpdate(InventoryManager.Instance.AppleCount, InventoryManager.Instance.PearCount, InventoryManager.Instance.StrawberryCount);
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

                EventTrigger eventTrigger = newInputField.GetComponent<EventTrigger>();

                EventTrigger.Entry dragEntry = new EventTrigger.Entry()
                {
                    eventID = EventTriggerType.Drag
                };

                dragEntry.callback.AddListener ((data) => { OnDrag((PointerEventData)data);});
                eventTrigger.triggers.Add(dragEntry);
            }
        }

        public void OnDrag(PointerEventData data)
        {
            _inputFieldsScrollRect.OnDrag(data);
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

        #region UIAdjusments

        private void ButtonInteractableState(bool isActive, Button button)
        {
            button.interactable = isActive;
        }

        #endregion

        #region ButtonOnClickMethods

        private void DiceRollButtonOnClick()
        {
            ButtonInteractableState(false, _diceRollButton);

            var currentDiceCount = GetCurrentDiceCount();

            var currentValues = GetActiveInputFieldValues();

            OnDiceRoll?.Invoke(currentDiceCount, currentValues);
            OnForwardMovement?.Invoke(currentValues);
        }

        #endregion
    }
}