using System.Collections;
using UnityEngine;

public class SpeedBuffCol : MonoBehaviour
{

    private Collider2D col;
    private SpriteRenderer sr;

    private GameObject[] units;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Unit")
        {
            StartCoroutine(buffSpeed(col.gameObject));
        }
    }

    IEnumerator buffSpeed(GameObject colUnit)
    {
        enabled = false;
        col.enabled = false;
        sr.enabled = false;
        units = GameObject.FindGameObjectsWithTag("Unit");
        ModifyStat(+100f);

        yield return new WaitForSeconds(2);
        units = GameObject.FindGameObjectsWithTag("Unit");
        //ModifyStat(-100f);

        Destroy(gameObject);
    }

    private void ModifyStat(float amount)
    {
        foreach (GameObject unit in units)
        {
            unit.GetComponent<UnitMovement>().ChangeSpeed(amount);
        }
    }
}
