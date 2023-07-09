using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShoot : MonoBehaviour
{
    // Stats
    private float fireRate;
    private int strength;

    [SerializeField]
    private GameObject projectile;
    private float TimeT;

    // Start is called before the first frame update
    void Start()
    {
        strength = GetComponent<UnitStatManager>().GetStrength();
        fireRate = GetComponent<UnitStatManager>().GetFireRate();
    }

    // Update is called once per frame
    void Update()
    {
        // Increment time by deltaTime
        TimeT += Time.deltaTime;

        // Shoot
        if (fireRate > 0)
        {
            if (TimeT > fireRate / 16)
            {
                // Summon Projectile
                GameObject unitProjectile = Instantiate(projectile, transform.position + new Vector3(0, 0.5f, 0), projectile.transform.rotation);
                unitProjectile.GetComponent<ProjectileManager>().SetStats(strength);
                TimeT = 0;
            }
        }
    }
}
