using GLTF.Schema;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarPlateChecker : MonoBehaviour
{
    [Header("Input field")]
    [SerializeField] private TMP_InputField carPlateInputField;

    [Header("UI")]
    [SerializeField] private Button purchaseBtn;

    [Header("Events")]
    [SerializeField] private GameEvent purchaseItemEvent;
    [SerializeField] private GameEvent carPlateTextEvent;

    private char[] _correctLetters = new char[] { 'А', 'В', 'Е', 'К', 'М', 'Н', 'О', 'Р', 'С', 'Т', 'У', 'Х' };
    private char[] _correctNumbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    private Color errorColor = Color.red;
    private Color normalColor = Color.black;

    private bool _isCorrectNumber = false; 
    public bool IsCorrectNumber() => _isCorrectNumber;

    private void Awake()
    {
        purchaseBtn.onClick.AddListener(BuyNewCarPlate);
    }

    public void OnValueChanged()
    {
        string carPlateData = carPlateInputField.text.Trim().ToUpper();
        List<char> charList = carPlateData.ToList();

        bool isValid = CheckEachCharacter(charList);

        if (!isValid)
        {
            carPlateInputField.textComponent.color = errorColor;
            _isCorrectNumber = false;
        }
        else
        {
            carPlateInputField.textComponent.color = normalColor;
            _isCorrectNumber = true;
        }
    }

    private bool CheckEachCharacter(List<char> charList)
    {
        for (int i = 0; i < charList.Count; i++)
        {
            char c = charList[i];

            if (i == 0 || i == 4 || i == 5)
            {
                if (!_correctLetters.Contains(c))
                {
                    return false;
                }
            }
            else if (i >= 1 && i <= 3)
            {
                if (!_correctNumbers.Contains(c))
                {
                    return false;
                }

                if (charList.Count >= 4 && charList[1] == '0' && charList[2] == '0' && charList[3] == '0')
                {
                    return false;
                }
            }
            else if (i >= 6 && i <= 8)
            {
                if (!_correctNumbers.Contains(c))
                {
                    return false;
                }
                if (i == 8 && c == '0')
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void BuyNewCarPlate()
    {
        if (_isCorrectNumber && carPlateInputField.text.Length == 9)
        {
            purchaseItemEvent.Raise(10000);
            carPlateTextEvent.Raise(carPlateInputField.text);
            carPlateInputField.text = null;
        }
        else
        {
            Debug.LogError("Номер невозможно купить");
        }
    }
}