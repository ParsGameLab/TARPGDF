using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITrapCustomer
{
    public static Player Instance { get; private set; }
    public event EventHandler OnCoinAmountChange;

    public int coinAmount;
    private void Awake()
    {
        Instance = this;
    }
    public int GetCoinAmount()
    {
        return coinAmount;
    }
    public void AddCoinAmount(int addCoinAmount)
    {
        coinAmount += addCoinAmount;
        OnCoinAmountChange?.Invoke(this, EventArgs.Empty);
    }
    public void BoughtTrap(PlacedObjectTypeSO placedObjectTypeSO)
    {
        Debug.Log("Buy Trap" + placedObjectTypeSO.name);
    }

    public bool TrySpendCoinAmount(int spendCoinAmount)
    {
        if (GetCoinAmount() >= spendCoinAmount)
        {
            coinAmount -= spendCoinAmount;
            OnCoinAmountChange?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanBuyTrap(int spendCoinAmount)
    {
        if (GetCoinAmount() >= spendCoinAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void PortCharacter(Vector3 target)
    {
        this.transform.position = target;
    }
}
