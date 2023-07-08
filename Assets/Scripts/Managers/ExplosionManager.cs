using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    // Start is called before the first frame update]
    private float TimeT;

    // Update is called once per frame
    void Update()
    {
        // increment time
        TimeT += Time.deltaTime;

        // once time is over 1 destroy explosion instance
        if (TimeT > 1)
        {
            Destroy(gameObject);
        }
    }
}
