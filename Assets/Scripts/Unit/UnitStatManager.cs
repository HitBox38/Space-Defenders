using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitStatManager : MonoBehaviour
{
    [SerializeField]
    private float health, speed, kamikaze, fireRate;

    [SerializeField]
    private int cost, strength;

    [Header("For UI")]
    [SerializeField] private int unitIndex = 0;

    public static event Action<int> OnUnitDestroy;

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
    public int GetStrength()
    {
        return strength;
    }
    // Return kamikaze to unit movement
    public float GetKamikaze()
    {
        return kamikaze;
    }

    // Return cost to currently unnamed unit shop manager
    public int GetCost()
    {
        return cost;
    }

    public void DecrementHealth(float projStr)
    {
        health -= projStr;
    }

    void Update()
    {
        if (health <= 0)
        {
            OnUnitDestroy?.Invoke(unitIndex);
            Destroy(gameObject);
            Destroy(GetComponent<UnitMovement>().GetCurrentPath());
        }
    }
}
