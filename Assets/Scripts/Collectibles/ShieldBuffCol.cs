using System.Collections;
using UnityEngine;

public class ShieldBuffCol : MonoBehaviour
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
            StartCoroutine(buffHP(col.gameObject));
        }
    }

    IEnumerator buffHP(GameObject colUnit)
    {
        enabled = false;
        sr.enabled = false;
        col.enabled = false;
        units = GameObject.FindGameObjectsWithTag("Unit");
        ModifyStat(99999);

        yield return new WaitForSeconds(2);
        units = GameObject.FindGameObjectsWithTag("Unit");
        //ModifyStat(100);

        Destroy(gameObject);
    }

    private void ModifyStat(float amount)
    {
        foreach (GameObject unit in units)
        {
             unit.GetComponent<UnitStatManager>().ChangeSpeed(amount);
        }
    }
}
