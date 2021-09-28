using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrapCustomer 
{
    void BoughtTrap(PlacedObjectTypeSO placedObjectTypeSO);
    bool TrySpendCoinAmount(int coinAmount);
    bool CanBuyTrap(int spendCoinAmount);


}
