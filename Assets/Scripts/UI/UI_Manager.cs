using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] TMP_Text speedText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public TMP_Text CheckSpeedText()
    {
        return speedText;
    }
}