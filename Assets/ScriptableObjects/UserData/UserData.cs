using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData")]
public class UserData : ScriptableObject
{
    public int moneyCount;

    public void BuyItem(int price)
    {
        moneyCount = moneyCount - price;
    }
}
