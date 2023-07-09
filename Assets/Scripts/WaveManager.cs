using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TMP_Text smallCount, mediumCount, largeCount, moneyDisplay;
    [SerializeField] private TMP_Text smallCountHUD, mediumCountHUD, largeCountHUD;

    [SerializeField] private GameObject[] units;

    [SerializeField]
    private GameObject[] paths;

    [SerializeField] private BoxCollider2D spawnerPos;
    private Wave currentWave = new Wave();

    public int GetCurrentWaveIndex { get; private set; } = 0;

    public static event Action<int> OnPurchase;
    void Start()
    {
        currentWave.SelectedPath(paths[0]);
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
            smallCountHUD.text = smallCount.text;

            GameObject currentUnit = Instantiate(units[0], getRandomPosition(), Quaternion.identity);
            currentWave.AppendUnit(currentUnit);
            currentUnit.GetComponent<UnitMovement>().SetPath(currentWave.getPath());
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
            mediumCountHUD.text = mediumCount.text;

            GameObject currentUnit = Instantiate(units[1], getRandomPosition(), Quaternion.identity);
            currentWave.AppendUnit(currentUnit);
            currentUnit.GetComponent<UnitMovement>().SetPath(currentWave.getPath());
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
            largeCountHUD.text = largeCount.text;

            GameObject currentUnit = Instantiate(units[2], getRandomPosition(), Quaternion.identity);
            currentWave.AppendUnit(currentUnit);
            currentUnit.GetComponent<UnitMovement>().SetPath(currentWave.getPath());
        }
    }

    private Vector3 getRandomPosition()
    {
        float min, max;
        min = spawnerPos.bounds.min.x;
        max = spawnerPos.bounds.max.x;

        return new Vector3(UnityEngine.Random.Range(min, max), spawnerPos.bounds.center.y - 1, 0);
    }

    public void SelectLoopy()
    {
        currentWave.SelectedPath(paths[1]);
        GetCurrentWaveIndex = 1;
    }

    public void SelectCurvy()
    {
        currentWave.SelectedPath(paths[2]);
        GetCurrentWaveIndex = 2;
    }

    public void SelectZigzag()
    {
        currentWave.SelectedPath(paths[0]);
        GetCurrentWaveIndex = 0;
    }
}
