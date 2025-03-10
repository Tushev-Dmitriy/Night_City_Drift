using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "UserData")]
public class UserData : ScriptableObject
{
    public int moneyCount;
    public float carVolume;
    public float musicVolume;
    public List<string> userCarsName;
    public void BuyItem(int price)
    {
        moneyCount -= price;
        SaveManager.Instance?.SaveUserData();
    }

    public bool CanBuy(int price)
    {
        return moneyCount >= price;
    }

    public void SetCarVolume(float volume)
    {
        carVolume = volume;
        SaveManager.Instance?.SaveUserData();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        SaveManager.Instance?.SaveUserData();
    }
}
