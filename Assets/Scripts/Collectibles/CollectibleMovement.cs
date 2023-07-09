using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectibleMovement : MonoBehaviour
{
    public static event Action<int> OnPickUpBuffTyped;

    [SerializeField] private int buffIndex = 0;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Unit"))
        {
            OnPickUpBuffTyped?.Invoke(buffIndex);
        }
    }

}
