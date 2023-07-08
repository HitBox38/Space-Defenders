using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private Image fuelClock;
    [SerializeField] private float fuelReducer = 0.05f;
    [SerializeField] private TMP_Text currency;

    // Start is called before the first frame update
    void Start()
    {
        currency.text = 0.ToString();
        StartCoroutine(ReduceFillAmountOverTime());
    }

    // Coroutine to reduce fillAmount over time
    IEnumerator ReduceFillAmountOverTime()
    {
        while (fuelClock.fillAmount > 0)
        {
            fuelClock.fillAmount -= fuelReducer * Time.deltaTime;
            if (fuelClock.fillAmount == .75f)
            {
                Debug.Log("at 75%");
            }
            else if (fuelClock.fillAmount == .5f)
            {
                Debug.Log("at 50%");
            }
            else if (fuelClock.fillAmount == .25f)
            {
                Debug.Log("at 25%");
            }
            yield return null;
        }
    }
}
