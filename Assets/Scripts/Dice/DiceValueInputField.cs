using TMPro;
using UnityEngine;

namespace Dice
{
    public class DiceValueInputField : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _placeHolder;
        [SerializeField] private TMP_InputField _inputField;

        private readonly string _placeHolderText = ". Dice Value";
        private int _childOrder;

        #region Property

        public TMP_InputField InputField
        {
            get => _inputField;
            set => _inputField = value;
        }

        #endregion

        private void OnEnable()
        {
            _childOrder = transform.GetSiblingIndex() + 1;
            _placeHolder.text = _childOrder + _placeHolderText;
        }
    }
}
