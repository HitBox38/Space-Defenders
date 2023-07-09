using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private int money = 100;
    [SerializeField]
    private TMP_Text moneyDisplay;

    void Start()
    {
    }
    void OnEnable()
    {
        ProjectileManager.OnSettlementShoot += IncrementCurrency;
        UnitMovement.OnKamikaze += IncrementCurrency;
        WaveManager.OnPurchase += DecrementCurrency;
    }

    void OnDisable()
    {
        ProjectileManager.OnSettlementShoot -= IncrementCurrency;
        UnitMovement.OnKamikaze -= IncrementCurrency;
        WaveManager.OnPurchase -= DecrementCurrency;
    }

    void IncrementCurrency(int value)
    {
        money += value;
        ChangeDisplayedMoney(value);
    }

    void DecrementCurrency(int value)
    {
        money -= value;
        ChangeDisplayedMoney(-value);
    }

    void ChangeDisplayedMoney(int value)
    {
        int asInt = int.Parse(moneyDisplay.text);
        moneyDisplay.text = (asInt + value).ToString();
    }
}
