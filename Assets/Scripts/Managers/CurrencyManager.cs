using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private int money;
    void OnEnable()
    {
        UnitMovement.OnKamikaze += IncrementCurrency;
        //UnitMovement.OnPurchase += DecrementCurrency;
    }

    void OnDisable()
    {
        UnitMovement.OnKamikaze -= IncrementCurrency;
        //UnitMovement.OnPurchase -= DecrementCurrency;
    }

    void IncrementCurrency(int value)
    {
        money += value;
    }

    void DecrementCurrency(int value)
    {
        money -= value;
    }
}
