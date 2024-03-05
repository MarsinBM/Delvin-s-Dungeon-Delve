using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Variables
    [SerializeField] GameObject OriginalCell;
    [SerializeField] GameObject UpgradePanel;

    [SerializeField] GameObject NorthDoor;
    [SerializeField] GameObject SouthDoor;
    [SerializeField] GameObject WestDoor;
    [SerializeField] GameObject EastDoor;
    //private GameObject UIcanvas;

    private Player player;

    private List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        StartCoroutine(SearchForEnemies());
    }

    void Update()
    {
        // If there are no enemies let the player walk through the door
        if (NoEnemiesLeft() && LevelManager.instance.isLevel0Complete == true)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Wall");
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
                GameObject northcell = Instantiate(LevelBlocks[Random.Range(0, LevelBlocks.Length)], spawnNorth, Quaternion.identity);
                LevelManager.instance.AddCells(northcell);

                Destroy(SouthDoor.gameObject);
                Destroy(WestDoor.gameObject);
                Destroy(EastDoor.gameObject);
                break;
            case 2:
                // Spawn cell south
                Vector3 spawnSouth = orginalcellPosition + Vector3.back * distance;
                GameObject southcell = Instantiate(LevelBlocks[Random.Range(0, LevelBlocks.Length)], spawnSouth, Quaternion.identity);
                LevelManager.instance.AddCells(southcell);

                Destroy(NorthDoor.gameObject);
                Destroy(WestDoor.gameObject);
                Destroy(EastDoor.gameObject);
                break;
            case 3:
                // Spawn cell west
                Vector3 spawnWest = orginalcellPosition + Vector3.left * distance;
                GameObject westcell = Instantiate(LevelBlocks[Random.Range(0, LevelBlocks.Length)], spawnWest, Quaternion.identity);
                LevelManager.instance.AddCells(westcell);

                Destroy(NorthDoor.gameObject);
                Destroy(SouthDoor.gameObject);
                Destroy(EastDoor.gameObject);
                break;
            case 4:
                // Spawn cell east
                Vector3 spawnEast = orginalcellPosition + Vector3.right * distance;
                GameObject eastcell = Instantiate(LevelBlocks[Random.Range(0, LevelBlocks.Length)], spawnEast, Quaternion.identity);
                LevelManager.instance.AddCells(eastcell);

                Destroy(NorthDoor.gameObject);
                Destroy(SouthDoor.gameObject);
                Destroy(WestDoor.gameObject);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            NextLevel();
            Destroy(gameObject);

            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                GameObject panel = Instantiate(UpgradePanel, canvas.transform);
                panel.transform.localPosition = Vector3.zero;
            }

            GameObject ThePlayer = GameObject.Find("Player");
            player = ThePlayer.GetComponent<Player>();

            player.isUpgradeOpen = true;
        }
    }
}
