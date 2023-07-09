using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] collectibles;

    void OnEnable()
    {
        DebrisMovement.OnDebrisCrash += summonCollectible;
    }

    void OnDisable()
    {
        DebrisMovement.OnDebrisCrash -= summonCollectible;
    }

    void summonCollectible(Vector3 position)
    {
        GameObject toSummon = collectibles[UnityEngine.Random.Range(0, collectibles.Length - 1)];
        Instantiate(toSummon, position, toSummon.transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
