using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using YG;

[Serializable]
[CreateAssetMenu(fileName = "UserData")]
public class UserData : ScriptableObject
{
    public int moneyCount;
    public float carVolume;
    public float musicVolume;
    public int maxScore;
    public List<string> userCarsName;
    public void BuyItem(int price)
    {
        moneyCount -= price;
        SaveManager.Instance?.SaveUserData();
    
        if (price > 0)
        {
            SaveManager.Instance?.SaveCarsDataYG();
        }
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

    public void SetMaxScore(int score)
    {
        if (score > maxScore)
        {
            maxScore = score;
            YG2.SetLeaderboard("driftScore", maxScore);
            SaveManager.Instance?.SaveUserData();
        }
    }
}
