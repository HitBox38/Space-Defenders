using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyDisplay;
    private int money = 100;

    void Start()
    {

    }

    private void Update()
    {
        if (money == 0)
        {
            // start counting 3 seconds and then go to game over screen
        }
    }
    void OnEnable()
    {
        UnitMovement.OnKamikaze += IncrementCurrency;
        WaveManager.OnPurchase += DecrementCurrency;
    }

    void OnDisable()
    {
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
