using System.Collections;
using UnityEngine;

public class SpeedBuffCol : MonoBehaviour
{
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
        ModifyStat(colUnit, +10f);

        yield return new WaitForSeconds(2);

        ModifyStat(colUnit, -10f);

        Destroy(gameObject);
    }

    private void ModifyStat(GameObject colUnit, float amount)
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

        colUnit.GetComponent<UnitStatManager>().ChangeSpeed(amount);
        foreach (GameObject unit in units)
        {
            unit.GetComponent<UnitMovement>().ChangeSpeed(amount);
        }
    }
}
