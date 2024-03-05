using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    // Variables
    [SerializeField] Transform[] spawnTiles;
    [SerializeField] GameObject[] Enemies;
    private int spawnPoints;

    void Start()
    {
        int SpawnIncrease = 0;
        if (LevelManager.instance.levelCounter >= 10 && LevelManager.instance.levelCounter % 10 == 0)
        {
            SpawnIncrease++;
        }

        RandomTiles();
        spawnPoints = SpawnIncrease + Random.Range(1, 3);

        for (int i = 0; i < Mathf.Min(spawnPoints, spawnTiles.Length); i++)
        {
            Transform tile = spawnTiles[i];
            Instantiate(Enemies[0], tile.position + Vector3.up, Quaternion.identity);
        }
    }

    void RandomTiles()
    {
        for (int i = 0; i < spawnTiles.Length; i++)
        {
            Transform temp = spawnTiles[i];
            int randomIndex = Random.Range(i, spawnTiles.Length);
            spawnTiles[i] = spawnTiles[randomIndex];
            spawnTiles[randomIndex] = temp;
        }
    }
}
