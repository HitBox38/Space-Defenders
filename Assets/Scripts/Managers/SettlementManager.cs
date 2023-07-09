using System;
using System.Collections.Generic;
using UnityEngine;

public class SettlementManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float health = 1500;
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
                //print("death");

                PauseMenu.Instance.ShowWinMenu();
                stopGame = true;
            }
        }
    }
}
