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
        float min = settlementPos.bounds.min.x;
        float max = settlementPos.bounds.max.x;
        Vector3 newPos;
        bool isPositionFree;

        do
        {
            newPos = new Vector3(UnityEngine.Random.Range(min, max), settlementPos.bounds.center.y, 0);
            Collider[] intersectingObjects = Physics.OverlapSphere(newPos, miniDefender.GetComponent<CircleCollider2D>().radius + .5f);

            isPositionFree = true;
            foreach (Collider collider in intersectingObjects)
            {
                if (collider.gameObject.tag == "Defender")
                {
                    isPositionFree = false;
                    break;
                }
            }

        } while (!isPositionFree);

        Instantiate(miniDefender, newPos, miniDefender.transform.rotation);
    }


}
