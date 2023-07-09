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
    [Header("UI Open Speed")]
    [SerializeField] private float UIOpenSpeed = 5;

    private Coroutine unitsMoveCo;
    private Vector3 UISreenPosition;
    private float _fuel = 1f;

    public float fuel { get => _fuel; set => _fuel = value; }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReduceFillAmountOverTime());
    }

    private void OnEnable()
    {
        FuelCol.OnCollect += AddFuel;
    }

    private void OnDisable()
    {
        FuelCol.OnCollect -= AddFuel;
    }

    private void Update()
    {
        float fuelIndicatorAngle = (_fuel - 0) * (90 - (-90)) / (maxFuel - 0) + (-90);
        fuelClock.rotation = Quaternion.Lerp(fuelClock.rotation, Quaternion.Euler(0, 0, -fuelIndicatorAngle), Time.deltaTime * fuelRotationSpeed);

        if (_fuel == 0)
        {

            // go to game over screen
        }
    }

    // Coroutine to reduce fillAmount over time
    IEnumerator ReduceFillAmountOverTime()
    {
        _fuel = maxFuel;
        while (_fuel > 0)
        {
            float toReduce = fuelReducer * Time.deltaTime;
            _fuel -= toReduce;
            if (_fuel >= (.75f - toReduce) && _fuel <= (.75f + toReduce))
            {
                Debug.Log("at 75%");
            }
            else if (_fuel >= (.5f - toReduce) && _fuel <= (.5f + toReduce))
            {
                Debug.Log("at 50%");
            }
            else if (_fuel >= (.25f - toReduce) && _fuel <= (.25f + toReduce))
            {
                Debug.Log("at 25%");
            }
            yield return null;
        }
    }

    private void AddFuel(float amount)
    {
        _fuel = Mathf.Clamp(amount + _fuel, 0, 1);
    }

    public void PopUpUI(GameObject toMove)
    {
        if (unitsMoveCo == null)
            unitsMoveCo = StartCoroutine(MoveUnitsUI(toMove.transform, UISreenPosition));
    }

    public void SetPopUpPos(GameObject obj)
    {
        UISreenPosition = obj.transform.localPosition;
    }

    private IEnumerator MoveUnitsUI(Transform toMove, Vector3 position)
    {
        while (!V3AlmostEqual(toMove.localPosition, position, 0.05f))
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
