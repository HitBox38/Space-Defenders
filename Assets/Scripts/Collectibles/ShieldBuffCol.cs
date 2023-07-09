using System.Collections;
using UnityEngine;

public class ShieldBuffCol : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Unit")
        {
            StartCoroutine(buffHP(col.gameObject));
        }
    }

    IEnumerator buffHP(GameObject colUnit)
    {
        enabled = false;
        ModifyStat(colUnit, 99999);

        yield return new WaitForSeconds(2);

        ModifyStat(colUnit, colUnit.GetComponent<UnitStatManager>().GetHealth());

        Destroy(gameObject);
    }

    private void ModifyStat(GameObject colUnit, float amount)
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

        colUnit.GetComponent<UnitStatManager>().ChangeStrength(amount);
        // foreach (GameObject unit in units)
        // {
        //     unit.GetComponent<UnitMovement>().ChangeSpeed(amount);
        // }
    }
}
