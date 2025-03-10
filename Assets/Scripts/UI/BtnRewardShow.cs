using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class BtnRewardShow : MonoBehaviour
{
    [SerializeField] EventManager _eventManager;

    public void ShowAdvReward()
    {
        string id = "coin";
        YG2.RewardedAdvShow(id, Reward);
    }

    private void Reward()
    {
        _eventManager.MoneyForYG(-10000);
    }
}
