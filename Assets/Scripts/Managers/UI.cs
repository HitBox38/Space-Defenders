using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("Fuel UI")]
    [SerializeField] private Image fuelClock;
    [SerializeField] private float fuelReducer = 0.05f;
    [Header("Currency UI")]
    [SerializeField] private TMP_Text currency;
    [Header("UI Open Speed")]
    [SerializeField] private float UIOpenSpeed = 5;

    private Coroutine unitsMoveCo;
    private Vector3 UISreenPosition;
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
