using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MiniDefenderManager : MonoBehaviour
{
    [SerializeField]
    private GameObject miniDefender;
    private float TimeT;

    // Update is called once per frame
    void Update()
    {
        TimeT += Time.deltaTime;

        if (TimeT > 20)
        {
            SummonMiniDefender();
            TimeT = 0;
        }
    }

    void SummonMiniDefender()
    {
        Instantiate(miniDefender, new Vector3(UnityEngine.Random.Range(-15, 15), 8, 0), miniDefender.transform.rotation);
    }

}
