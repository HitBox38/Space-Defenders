using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject[] debris;
    private int numOfDebris = 0;

    private bool summonDebris = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (numOfDebris < 5)
        {
            if (summonDebris)
            {
                summonDebris = false;
                StartCoroutine(debrisInstantiator());
            }
        }

        DebrisMovement.OnDebrisLeave += DecrementDebris;
    }

    public void DecrementDebris()
    {
        numOfDebris -= 1;
    }

    IEnumerator debrisInstantiator()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10));

        GameObject debrisObj = debris[Random.Range(0, debris.Length)];
        Instantiate(debrisObj, new Vector3(20, Random.Range(-6, 6), 0), debrisObj.transform.rotation);
        numOfDebris += 1;
        summonDebris = true;
    }
}
