using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuffCol : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Unit")
        {
            print("1");
            StartCoroutine(buffSpeed(col.gameObject));
        }
    }

    IEnumerator buffSpeed(GameObject colunit)
    {
        print(2);
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

        colunit.GetComponent<UnitStatManager>().changeSpeed(+10f);
        foreach (GameObject unit in units)
        {
            unit.GetComponent<UnitMovement>().ChangeSpeed(+10f);
        }

        yield return new WaitForSeconds(2);


        print(3);
        colunit.GetComponent<UnitStatManager>().changeSpeed(-10f);
        foreach (GameObject unit in units)
        {
            unit.GetComponent<UnitMovement>().ChangeSpeed(-10f);
        }
    }
}
