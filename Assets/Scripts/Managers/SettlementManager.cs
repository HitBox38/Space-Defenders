using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float health = 200;
    private bool stopGame = false;

    public void DecrementHealth(float str)
    {
        health -= str;
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
