using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Variables
    [SerializeField] int dmgIncrease;
    [SerializeField] float evIncrease;
    [SerializeField] int hpIncrease;
    [SerializeField] int hpPotionIncrease;
    [SerializeField] int sdPotionIncrease;
    [SerializeField] int hsIncrease;

    private int EVlevel = 0;

    public Player player;
    
    // Increases the player's attack
    public void AttackUp()
    {
        // Increase damage
        player.Attack += dmgIncrease;

    }

    // Increases the player's evasion
    public void EvadeUp()
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

    // Increases the player's max health & heals slightly
    public void HealthUp()
    {
        // Increase max health + heal
        player.MaxHealth += hpIncrease;
        if (player.Health != player.MaxHealth)
        {
            player.Heal(2);
        }

    }

    // Gives the player a Health potion 
    public void GiveHealthPotion()
    {
        // Gives player +1 Health Potion, potion will restore the player's health to max
        player.HealthPotions += hpPotionIncrease;

    }

    // Gives the player a Speed potion
    public void GiveSpeedPotion()
    {
        // Gives player +1 Speed Potion, potion will allow the player to do more actions per turn (either by giving them more action points or lower the cost of actions)
        player.SpeedPotions += sdPotionIncrease;

    }

    // Gives the player a holy shield
    public void HolyShield()
    {
        // Gives the player +1 Holy Shield, shield will block all damage for an x amount of turns
        player.HolyShields += hsIncrease;

    }
}
