using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Variables
    public GameObject[] levelBlocks;
    public static LevelManager instance;

    public int levelCounter = 0;
    public List<GameObject> cells = new List<GameObject>();

    [SerializeField] GameObject enemyS1;
    [SerializeField] GameObject enemyS2;

    public bool isLevel0Complete = false;

    private void Awake()
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

    void Update()
    {
        if (enemyS1 == null && enemyS2 == null)
        {
            isLevel0Complete = true;
        }
    }

    // Level 0
    public void Level00()
    {
        enemyS1.SetActive(true);
        enemyS2.SetActive(true);
    }

    // Increases the level count
    public void IncreaseLevel()
    {
        levelCounter++;
    }

    // Adds cells that are spawned in to the list
    public void AddCells(GameObject cell)
    {
        cells.Add(cell);
    }

    // Removes old cells from the game when the player leaves them
    public void DeleteCells()
    {
        if (levelCounter == 1)
        {
            GameObject startcell = GameObject.Find("Start");
            if (startcell != null)
            {
                Destroy(startcell.gameObject);
            }
        }
        else if(cells.Count > 1)
        {
            GameObject toRemove = cells[0];
            cells.RemoveAt(0);
            Destroy(toRemove.gameObject);
        }
    }
}
