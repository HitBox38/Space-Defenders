using UnityEngine;

public class UnitStatManager : MonoBehaviour
{
    [SerializeField]
    private float health, speed, kamikaze, fireRate;

    [SerializeField]
    private int cost, strength;

    public void ChangeSpeed(float valSpeed)
    {
        speed += valSpeed;
    }

    public void ChangeStrength(float valMultiplier)
    {
        strength *= (int)valMultiplier;
        kamikaze *= valMultiplier;
    }

    public void ChangeHealth(float valHealth)
    {
        health += valHealth;
    }

    public float GetHealth()
    {
        return health;
    }

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
            Destroy(gameObject);
            Destroy(GetComponent<UnitMovement>().GetCurrentPath());
        }
    }
}
