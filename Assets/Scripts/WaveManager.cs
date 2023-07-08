using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private TMP_Text smallCount, mediumCount, largeCount, moneyDisplay;

    [SerializeField]
    private GameObject[] units;

    [SerializeField] private BoxCollider2D spawnerPos;
    private Wave currentWave;

    public static event Action<int> OnPurchase;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PurchaseSmall()
    {
        int asInt = int.Parse(moneyDisplay.text);
        if (asInt >= 15)
        {
            OnPurchase?.Invoke(15);
            asInt = int.Parse(smallCount.text);
            smallCount.text = (asInt + 1).ToString();

            Instantiate(units[0], getRandomPosition(), Quaternion.identity);
        }
    }

    public void PurchaseMedium()
    {
        int asInt = int.Parse(moneyDisplay.text);
        if (asInt >= 45)
        {
            OnPurchase?.Invoke(45);
            asInt = int.Parse(mediumCount.text);
            mediumCount.text = (asInt + 1).ToString();

            Instantiate(units[1], getRandomPosition(), Quaternion.identity);
        }
    }

    public void PurchaseLarge()
    {
        int asInt = int.Parse(moneyDisplay.text);
        if (asInt >= 70)
        {
            OnPurchase?.Invoke(70);
            asInt = int.Parse(largeCount.text);
            largeCount.text = (asInt + 1).ToString();

            Instantiate(units[2], getRandomPosition(), Quaternion.identity);
        }
    }

    private Vector3 getRandomPosition()
    {
        float min, max;
        min = spawnerPos.bounds.min.x;
        max = spawnerPos.bounds.max.x;

        return new Vector3(UnityEngine.Random.Range(min, max), spawnerPos.bounds.center.y, 0);
    }
}
