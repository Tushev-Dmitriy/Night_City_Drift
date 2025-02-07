using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<string> OnTextChanged;

    public static void ChangeText(string newText)
    {
        OnTextChanged?.Invoke(newText);
    }
}