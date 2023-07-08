using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float strength, projSpeed, projLifespan;

    private float TimeT;

    // Set strength from UnitStatManager
    public void SetStats(float str, float ps, float ls)
    {
        strength = str;
        projSpeed = ps;
        projLifespan = ls;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, projSpeed, 0);

        // Increment time by deltaTime
        TimeT += Time.deltaTime;

        // Shoot
        if (TimeT > projLifespan)
        {
            Destroy(gameObject);
        }
    }
}
