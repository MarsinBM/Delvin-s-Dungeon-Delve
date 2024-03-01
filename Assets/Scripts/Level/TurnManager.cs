using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Player player;
    public Enemy[] enemies;

    private bool playerTurn = true;

    void Start()
    {
        player.APreset();
    }

    void Update()
    {
        if (playerTurn)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }

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

    void PlayerTurn()
    {
        if (player.GetAP() <= 0)
        {
            playerTurn = false;
            foreach (Enemy enemy in enemies)
            {
                enemy.mPreset();
            }
        }
    }

    void EnemyTurn()
    {
        bool allEnemiesFinished = true;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.GetmP() > 0)
            {
                enemy.mPreset();
                allEnemiesFinished = false;
            }
        }

        if (allEnemiesFinished || NoEnemiesLeft())
        {
            playerTurn = true;
            player.APreset();
        }
    }
}
