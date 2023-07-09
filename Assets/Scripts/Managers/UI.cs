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

    [Header("Managers Inputs")]
    [SerializeField] private WaveManager waveManager;

    [Header("Selected Path Image")]
    [SerializeField] private Image selectedImagePath;
    [SerializeField] private Sprite[] pathsSprites;

    [Header("Unit Numbers")]
    [SerializeField] private TMP_Text[] UnitNumbersHUD;
    [SerializeField] private TMP_Text[] UnitNumbersPurchaseMenu;

    private Coroutine unitsMoveCo;
    private Vector3 UISreenPosition;
    private float _fuel = 1f;

    public float fuel => _fuel;

    private void Awake()
    {
        UnitStatManager.OnUnitDestroy += ReduceUnitNumberUI;
    }

    private void OnDestroy()
    {
        UnitStatManager.OnUnitDestroy -= ReduceUnitNumberUI;
    }

    private void ReduceUnitNumberUI(int index)
    {
        UnitNumbersHUD[index].text = (int.Parse(UnitNumbersHUD[index].text) - 1).ToString();
        UnitNumbersPurchaseMenu[index].text = (int.Parse(UnitNumbersPurchaseMenu[index].text) - 1).ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReduceFillAmountOverTime());
    }

    private void Update()
    {
        float fuelIndicatorAngle = (_fuel - 0) * (90 - (-90)) / (maxFuel - 0) + (-90);
        fuelClock.rotation = Quaternion.Lerp(fuelClock.rotation, Quaternion.Euler(0, 0, -fuelIndicatorAngle), Time.deltaTime * fuelRotationSpeed);

        if (_fuel <= 0)
        {
            PauseMenu.Instance.ShowLoseMenu();
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

    public void PopUpUI(GameObject toMove)
    {
        if (unitsMoveCo == null)
            unitsMoveCo = StartCoroutine(MoveUnitsUI(toMove.transform, UISreenPosition));
    }

    public void SetPopUpPos(GameObject obj)
    {
        UISreenPosition = obj.transform.localPosition;
    }

    public void ChangeSelectedPathImage()
    {
        selectedImagePath.sprite = pathsSprites[waveManager.GetCurrentWaveIndex];
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
