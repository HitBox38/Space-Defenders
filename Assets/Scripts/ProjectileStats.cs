using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    [SerializeField]
    private float strength;

    public float GetStrength()
    {
        return strength;
    }
}
