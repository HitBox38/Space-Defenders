using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DebrisMovement : MonoBehaviour
{
    // Update is called once per frame
    public GameObject explosion;
    public static event Action OnDebrisLeave;
    public static event Action<Vector3> OnDebrisCrash;

    private float verticalOrientation;

    void Start()
    {
        verticalOrientation = UnityEngine.Random.Range(-0.1f, 0.1f);
    }
    void Update()
    {
        transform.position += new Vector3(-3f, verticalOrientation, 0) * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 20) * Time.deltaTime);

        if (transform.position.x < -20)
        {
            OnDebrisLeave?.Invoke();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Unit")
        {
            OnDebrisCrash?.Invoke(transform.position);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(coll.gameObject);
            Destroy(coll.GetComponent<UnitMovement>().GetCurrentPath());
            Destroy(gameObject);
        }
    }

}
