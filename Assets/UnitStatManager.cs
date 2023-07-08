using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatManager : MonoBehaviour
{
    [SerializeField]
    private float health, speed, strength, kamikaze, cost, fireRate;


    // Return speed to UnitMovement.cs
    public float GetSpeed()
    {
        return speed;
    }

    // Return fire rate to UnitShoot
    public float GetFireRate()
    {
        return fireRate;
    }

    // Return strength to UnitShoot
    public float GetStrength()
    {
        return strength;
    }

    // Return cost to currently unnamed unit shop manager
    public float GetCost()
    {
        return cost;
    }

    public void DecrementHealth(float projStr)
    {
        health -= projStr;
    }
}
