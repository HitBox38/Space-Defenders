using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float angle = 0;

    private Vector3 currentPosition;
    void Start()
    {
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currentPosition + new Vector3(0, Mathf.Sin(angle) / 8, 0);
        angle += 3 * Time.deltaTime;
    }

}
