using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    // Variables
    [SerializeField] private int baseDamage;
    [SerializeField] private float baseAccuracy;
    [SerializeField] private int tier;

    void Start()
    {

        tier = 0;

        float tierChance = Random.value;
        if (tierChance < 0.2f)
        {
            tier++;
        }
        else if (tierChance < 0.05f)
        {
            tier += 2;
        }

        switch (tier)
        {
            case 0:
                baseDamage = Random.Range(5, 10);
                baseAccuracy = 0.3f;
                break;
            case 1:
                baseDamage = Random.Range(6, 12);
                baseAccuracy = 0.35f;
                break;
            case 2:
                baseDamage = Random.Range(8, 16);
                baseAccuracy = 0.4f;
                break;
            case 3:
                baseDamage = Random.Range(12, 20);
                baseAccuracy = 0.45f;
                break;
            case 4:
                baseDamage = Random.Range(12, 25);
                baseAccuracy = 0.5f;
                break;
            case 5:
                baseDamage = Random.Range(20, 25);
                baseAccuracy = 0.55f;
                break;
            case 6:
                baseDamage = 30;
                baseAccuracy = 1f;
                break;
        }
    }

    // Getters
    public int GetBaseDamage()
    {
        return baseDamage;
    }

    public float GetBaseAccuracy()
    {
        return baseAccuracy;
    }

    public int GetTier()
    {
        return tier;
    }
}
