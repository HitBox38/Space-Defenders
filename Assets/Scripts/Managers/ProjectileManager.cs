using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private int strength;

    [SerializeField]
    private GameObject explosion;

    private float TimeT;

    // Set strength from UnitStatManager
    public void SetStats(int str)
    {
        strength = str;
    }

    public int GetStrength()
    {
        return strength;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0.1f, 0);

        // Increment time by deltaTime
        TimeT += Time.deltaTime;

        // Die after 4 seconds
        if (TimeT > 4)
        {
            Destroy(gameObject);
        }
    }


    // On Projectile Hit
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Settlement")
        {
            col.gameObject.GetComponent<SettlementManager>().DecrementHealth(strength);

            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(gameObject);
        }
    }
}
