using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] TMP_Text speedText;

    private void OnEnable()
    {
        EventManager.ChangeText(speedText.text);
    }
}
