using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Variables
    [SerializeField] GameObject OriginalCell;

    // Handles the levels spawinging in the right place
    void NextLevel()
    {
        string doorName = gameObject.name;

        int direction = 0;
        if (doorName.Contains("NorthDoor"))
        {
            direction = 1;
        }
        else if (doorName.Contains("SouthDoor"))
        {
            direction = 2;
        }
        else if (doorName.Contains("WestDoor"))
        {
            direction = 3;
        }
        else if (doorName.Contains("EastDoor"))
        {
            direction = 4;
        }

        //int randomCell = Random.Range(0, LevelBlocks.Length);
        Vector3 orginalcellPosition = OriginalCell.transform.position;
        GameObject[] LevelBlocks = LevelManager.instance.levelBlocks;
        float distance = 19f;

        switch (direction)
        {
            case 1:
                // Spawn cell north
                Vector3 spawnNorth = orginalcellPosition + Vector3.forward * distance;
                Instantiate(LevelBlocks[Random.Range(0, LevelBlocks.Length)], spawnNorth, Quaternion.identity);
                break;
            case 2:
                // Spawn cell south
                Vector3 spawnSouth = orginalcellPosition + Vector3.back * distance;
                Instantiate(LevelBlocks[Random.Range(0, LevelBlocks.Length)], spawnSouth, Quaternion.identity);
                break;
            case 3:
                // Spawn cell west
                Vector3 spawnWest = orginalcellPosition + Vector3.left * distance;
                Instantiate(LevelBlocks[Random.Range(0, LevelBlocks.Length)], spawnWest, Quaternion.identity);
                break;
            case 4:
                // Spawn cell east
                Vector3 spawnEast = orginalcellPosition + Vector3.right * distance;
                Instantiate(LevelBlocks[Random.Range(0, LevelBlocks.Length)], spawnEast, Quaternion.identity);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            NextLevel();
            Destroy(gameObject);
        }
    }
}
