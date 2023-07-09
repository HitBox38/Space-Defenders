using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthBuffCol : MonoBehaviour
{
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
        ModifyStat(colUnit, 2);

        yield return new WaitForSeconds(2);

        ModifyStat(colUnit, .5f);

        Destroy(gameObject);
    }

    private void ModifyStat(GameObject colUnit, float amount)
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

        colUnit.GetComponent<UnitStatManager>().ChangeStrength(amount);
        foreach (GameObject unit in units)
        {
            unit.GetComponent<UnitShoot>().ChangeStrength(amount);
        }
    }
}
