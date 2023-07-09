using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private int strength;

    [SerializeField]
    private GameObject explosion;

    private float TimeT;
    public static event Action<int> OnSettlementShoot;

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
        OnSettlementShoot?.Invoke(5);
        if (col.tag == "Settlement")
        {
            col.gameObject.GetComponent<SettlementManager>().DecrementHealth(strength);
            Debug.Log(strength);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(gameObject);
        }
    }
}
