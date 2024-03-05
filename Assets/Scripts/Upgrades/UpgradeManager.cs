using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Variables
    private int dmgIncrease = 2;
    private float evIncrease = 0.01f;
    private int hpIncrease = 5;
    private int hpPotionIncrease = 1;
    private int sdPotionIncrease = 1;
    private int hsIncrease = 1;

    private int EVlevel = 0;

    public Player player;

    public static UpgradeManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Increases the player's attack
    public void AttackUp() // #1
    {
        // Increase damage
        player.Attack += dmgIncrease;

    }

    // Increases the player's evasion
    public void EvadeUp() // #2
    {
        // Increase evasion
        if (EVlevel == 0)
        {
            player.Evasion += 0.1f;
            EVlevel += 1;
        }
        else
        {
            player.Evasion += evIncrease;
        }

    }

    // Increases the player's max health
    public void HealthUp() // #3
    {
        // Increase max health (tries to keep the player's health percentage the same)
        int newMaxHealth = player.MaxHealth + hpIncrease;
        float healthPercentage = ((float)player.Health / player.MaxHealth);

        player.MaxHealth = newMaxHealth;
        player.Health = Mathf.RoundToInt(healthPercentage * newMaxHealth);

    }

    // Gives the player a Health potion 
    public void GiveHealthPotion() // #4
    {
        // Gives player +1 Health Potion, potion will restore the player's health to max
        player.HealthPotions += hpPotionIncrease;

    }

    // Gives the player a Speed potion
    public void GiveSpeedPotion() // #5
    {
        // Gives player +1 Speed Potion, potion will allow the player to do more actions per turn (either by giving them more action points or lower the cost of actions)
        player.SpeedPotions += sdPotionIncrease;

    }

    // Gives the player a holy shield
    public void HolyShield() // #6
    {
        // Gives the player +1 Holy Shield, shield will block all damage for an x amount of turns
        player.HolyShields += hsIncrease;
    }
}
