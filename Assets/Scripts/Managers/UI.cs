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
    [Header("Settlement UI")]
    [SerializeField] private Image settlementHealth;
    [SerializeField] private float settlementHealthReducer = 0.5f;

    [Header("Managers Inputs")]
    [SerializeField] private WaveManager waveManager;

    [Header("Selected Path Image")]
    [SerializeField] private Image selectedImagePath;
    [SerializeField] private Sprite[] pathsSprites;

    [Header("Unit Numbers")]
    [SerializeField] private TMP_Text[] UnitNumbersHUD;
    [SerializeField] private TMP_Text[] UnitNumbersPurchaseMenu;

    [Header("Buff Effects")]
    [SerializeField] private Sprite[] buffEffects;
    [SerializeField] private Image buffImage;
    [SerializeField, Range(0f, 1f)] private float buffFadePlus = .1f;
    [SerializeField, Min(0)] private float buffFadeDelay = .1f;

    private Coroutine unitsMoveCo;
    private Coroutine buffsFadeCo;
    private Vector3 UISreenPosition;
    private float _fuel = 1f;

    public float fuel { get => _fuel; set => _fuel = value; }

    private void Awake()
    {
        SettlementManager.OnHit += UpdateSettlementUI;
        UnitStatManager.OnUnitDestroy += ReduceUnitNumberUI;
    }

    private void OnDestroy()
    {
        SettlementManager.OnHit -= UpdateSettlementUI;
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

    private void OnEnable()
    {
        FuelCol.OnCollect += AddFuel;
        CollectibleMovement.OnPickUpBuffTyped += SetBuff;
    }

    private void OnDisable()
    {
        FuelCol.OnCollect -= AddFuel;
        CollectibleMovement.OnPickUpBuffTyped -= SetBuff;
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

    public void ChangeSelectedPathImage()
    {
        Debug.Log(waveManager.GetCurrentWaveIndex);
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

    public void SetBuff(int buffIndex)
    {
        if (buffIndex < 0) return;
        buffImage.sprite = buffEffects[buffIndex];
        if(buffsFadeCo == null)
            buffsFadeCo = StartCoroutine(BuffDisplayFadeIn());
    }

    private IEnumerator BuffDisplayFadeIn()
    {
        while(buffImage.color.a < .999f)
        {
            buffImage.color = new Color(255, 255, 255, buffImage.color.a + buffFadePlus);
            yield return new WaitForSeconds(buffFadeDelay);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(BuffDisplayFadeOut());
    }
    private IEnumerator BuffDisplayFadeOut()
    {
        while (buffImage.color.a > 0.001f)
        {
            buffImage.color = new Color(255, 255, 255, buffImage.color.a - buffFadePlus);
            yield return new WaitForSeconds(buffFadeDelay);
        }
        buffsFadeCo = null;
    }

    private bool V3AlmostEqual(Vector3 a, Vector3 b, float comparisonNumber)
    {
        return Vector3.SqrMagnitude(a - b) < comparisonNumber;
    }

    private void UpdateSettlementUI(float health)
    {
        StartCoroutine(ReduceFillAmountSettlement(health));
    }

    private IEnumerator ReduceFillAmountSettlement(float health)
    {
        float targetFillAmount = health / 1500;
        while (settlementHealth.fillAmount > targetFillAmount)
        {
            settlementHealth.fillAmount = Mathf.Lerp(settlementHealth.fillAmount, targetFillAmount, Time.deltaTime * settlementHealthReducer);
            yield return null;
        }
    }
}
