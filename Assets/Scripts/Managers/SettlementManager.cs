using System;
using System.Collections.Generic;
using UnityEngine;

public class SettlementManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float health = 200;
    private bool stopGame = false;

    public static event Action<float> OnHit;

    public void DecrementHealth(float str)
    {
        health -= str;
        OnHit?.Invoke(health);
    }

    void Update()
    {
        if (!stopGame)
        {
            if (health <= 0)
            {
                print("death");
                stopGame = true;
            }
        }
    }
}
