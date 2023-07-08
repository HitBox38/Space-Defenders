using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShoot : MonoBehaviour
{
    // Stats
    private float strength, projSpeed, fireRate, projLifespan;

    [SerializeField]
    private GameObject projectile;
    private float TimeT;

    // Start is called before the first frame update
    void Start()
    {
        strength = GetComponent<UnitStatManager>().GetStrength();
        projSpeed = GetComponent<UnitStatManager>().GetProjSpeed();
        fireRate = GetComponent<UnitStatManager>().GetFireRate();
        projLifespan = GetComponent<UnitStatManager>().GetLifeSpan();
    }

    // Update is called once per frame
    void Update()
    {
        // Increment time by deltaTime
        TimeT += Time.deltaTime;

        // Shoot
        if (TimeT > fireRate)
        {
            GameObject unitProjectile = Instantiate(projectile, transform.position, projectile.transform.rotation);
            unitProjectile.GetComponent<ProjectileManager>().SetStats(strength, projSpeed, projLifespan);
            TimeT = 0;
        }
    }
}
