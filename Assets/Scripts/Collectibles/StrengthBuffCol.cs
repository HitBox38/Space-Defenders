using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthBuffCol : MonoBehaviour
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
            StartCoroutine(buffStrength(col.gameObject));
        }
    }

    IEnumerator buffStrength(GameObject colUnit)
    {
        enabled = false;
        col.enabled = false;
        sr.enabled = false;

        units = GameObject.FindGameObjectsWithTag("Unit");
        ModifyStat(2);

        yield return new WaitForSeconds(2);
        units = GameObject.FindGameObjectsWithTag("Unit");
        //ModifyStat(.5f);

        Destroy(gameObject);
    }

    private void ModifyStat(float amount)
    {
        foreach (GameObject unit in units)
        {
            unit.GetComponent<UnitShoot>().ChangeStrength(amount);
        }
    }
}
