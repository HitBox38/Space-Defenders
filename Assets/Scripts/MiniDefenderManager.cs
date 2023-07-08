using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MiniDefenderManager : MonoBehaviour
{
    [SerializeField] private GameObject miniDefender;
    [SerializeField] private BoxCollider2D settlementPos;
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
        float min, max;
        min = settlementPos.bounds.min.x;
        max = settlementPos.bounds.max.x;

        Instantiate(miniDefender, new Vector3(UnityEngine.Random.Range(min, max), settlementPos.bounds.center.y, 0), miniDefender.transform.rotation);
    }

}
