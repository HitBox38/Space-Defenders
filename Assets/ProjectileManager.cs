using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private float strength;

    [SerializeField]
    private GameObject explosion;

    private float TimeT;

    // Set strength from UnitStatManager
    public void SetStats(float str)
    {
        strength = str;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0.5f, 0);

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
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(gameObject);
        }
    }
}
