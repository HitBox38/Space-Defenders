using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("Fuel UI")]
    [SerializeField] private Transform fuelClock;
    [SerializeField] private float fuelReducer = 0.05f;
    [SerializeField] private float fuelRotationSpeed = 1f;
    [SerializeField, Min(1)] private float maxFuel = 1f;
    [Header("Currency UI")]
    [SerializeField] private TMP_Text currency;
    [Header("UI Open Speed")]
    [SerializeField] private float UIOpenSpeed = 5;

    private Coroutine unitsMoveCo;
    private Vector3 UISreenPosition;
    private float fuel = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currency.text = 0.ToString();
        StartCoroutine(ReduceFillAmountOverTime());
    }

    private void Update()
    {
        float fuelIndicatorAngle = (fuel - 0) * (90 - (-90)) / (maxFuel - 0) + (-90);
        fuelClock.rotation = Quaternion.Lerp(fuelClock.rotation, Quaternion.Euler(0, 0, -fuelIndicatorAngle), Time.deltaTime * fuelRotationSpeed);
    }

    // Coroutine to reduce fillAmount over time
    IEnumerator ReduceFillAmountOverTime()
    {
        fuel = maxFuel;
        while (fuel > 0)
        {
            fuel -= fuelReducer * Time.deltaTime;
            if (fuel == .75f)
            {
                Debug.Log("at 75%");
            }
            else if (fuel == .5f)
            {
                Debug.Log("at 50%");
            }
            else if (fuel == .25f)
            {
                Debug.Log("at 25%");
            }
            yield return null;
        }
    }

    public void PopUpUI(GameObject toMove)
    {
        if(unitsMoveCo == null)
            unitsMoveCo = StartCoroutine(MoveUnitsUI(toMove.transform, UISreenPosition));
    }

    public void SetPopUpPos(GameObject obj)
    {
        UISreenPosition = obj.transform.localPosition;
    }

    private IEnumerator MoveUnitsUI(Transform toMove, Vector3 position)
    {
        while(!V3AlmostEqual(toMove.localPosition, position, 0.005f))
        {
            yield return new WaitForEndOfFrame();
            toMove.localPosition = Vector3.Lerp(toMove.localPosition, position, UIOpenSpeed * Time.deltaTime);
        }
        unitsMoveCo = null;
    }

    private bool V3AlmostEqual(Vector3 a, Vector3 b, float comparisonNumber)
    {
        return Vector3.SqrMagnitude(a - b) < comparisonNumber;
    }
}
