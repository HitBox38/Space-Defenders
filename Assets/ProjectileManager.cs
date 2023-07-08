using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float strength;

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

        // Shoot
        if (TimeT > 4)
        {
            Destroy(gameObject);
        }
    }
}
