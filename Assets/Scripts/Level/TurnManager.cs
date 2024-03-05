using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Variables
    public Player player;
    public List<Enemy> enemies = new List<Enemy>();

    private bool playerTurn = true;
    //private int enemyIndex = 0;

    void Start()
    {
        player.APreset();
        StartCoroutine(SearchForEnemies());
        
    }

    void Update()
    {
        if (playerTurn)
        {
            //Debug.Log("Player");
            PlayerTurn();
        }
        else
        {
            //Debug.Log("Enemy");
            EnemyTurn();
        }
    }

    // A coroutine that searches for enemies
    IEnumerator SearchForEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            Enemy[] foundEnemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in foundEnemies)
            {
                if (!enemies.Contains(enemy))
                {
                    enemies.Add(enemy);
                }
            }
            enemies.RemoveAll(enemy => enemy == null);
        }
    }

    // Checks if there are no enemies left
    bool NoEnemiesLeft()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }

    // Handles what to do if it's the player's turn
    void PlayerTurn()
    {
        if (player.GetAP() <= 0)
        {
            playerTurn = false;
            foreach (Enemy enemy in enemies)
            {
                enemy.mPreset();
            }
            DecreaseSpeedPotion();
            DecreaseShield();
        }
    }

    // Decreasese the speed potions's duration each time a player turn ends
    void DecreaseSpeedPotion()
    {
        if (player.SpeedActive == true)
        {
            player.speedDuration--;
            if (player.speedDuration <= 0)
            {
                player.SpeedActive = false;
            }
        }
    }

    // Decrease the holy shield's duration each time a player turn ends
    void DecreaseShield()
    {
        if (player.ShieldActive == true)
        {
            player.shieldDuration--;
            if (player.shieldDuration <= 0)
            {
                player.ShieldActive = false;
            }
        }
    }

    // Handles what to do if it's the enemies' turn
    void EnemyTurn()
    {
        bool allEnemiesFinished = true;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.GetmP() > 0)
            {
                enemy.mPreset();
                allEnemiesFinished = true;
            }
        }

        if (allEnemiesFinished || NoEnemiesLeft())
        {
            playerTurn = true;
            player.APreset();
        }
    }
}
