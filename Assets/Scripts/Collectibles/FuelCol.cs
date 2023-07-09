using System;
using UnityEngine;

public class FuelCol : MonoBehaviour
{
    public static event Action<float> OnCollect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Unit")
        {
            OnCollect?.Invoke(.1f);
            Destroy(gameObject);
        }
    }
}
