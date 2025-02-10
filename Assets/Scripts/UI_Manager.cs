using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] TMP_Text speedText;

    public TMP_Text CheckTest()
    {
        return speedText;
    }
}