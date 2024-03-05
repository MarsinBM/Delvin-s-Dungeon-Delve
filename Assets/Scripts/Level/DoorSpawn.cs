using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawn : MonoBehaviour
{
    // Variables
    [SerializeField] GameObject NorthDoor;
    [SerializeField] GameObject SouthDoor;
    [SerializeField] GameObject WestDoor;
    [SerializeField] GameObject EastDoor;

    [SerializeField] GameObject NorthA;
    [SerializeField] GameObject SouthA;
    [SerializeField] GameObject WestA;
    [SerializeField] GameObject EastA;

    private GameObject Camera;

    void Start()
    {
        Camera = GameObject.Find("Camera");
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.name == "Player")
        {
            LevelManager.instance.IncreaseLevel();
            LevelManager.instance.DeleteCells();
            Destroy(gameObject);
            ActivateDoors();
        }
    }

    // Handles the door's spawing in the right places
    void ActivateDoors()
    {
        string doorActiveName = gameObject.name;

        int direction = 0;
        if (doorActiveName.Contains("NorthActivator"))
        {
            direction = 1;
        }
        else if (doorActiveName.Contains("SouthActivator"))
        {
            direction = 2;
        }
        else if (doorActiveName.Contains("WestActivator"))
        {
            direction = 3;
        }
        else if (doorActiveName.Contains("EastActivator"))
        {
            direction = 4;
        }

        Vector3 originalCamPos = Camera.transform.position;
        float distance = 19f;

        switch (direction)
        {
            case 1:
                //Debug.Log("North");
                Vector3 moveCamSouth = originalCamPos + Vector3.back * distance;
                Camera.transform.position = moveCamSouth;

                SouthDoor.SetActive(true);
                WestDoor.SetActive(true);
                EastDoor.SetActive(true);

                Destroy(SouthA.gameObject);
                Destroy(WestA.gameObject);
                Destroy(EastA.gameObject);
                break;
            case 2:
                //Debug.Log("South");
                Vector3 moveCamNorth = originalCamPos + Vector3.forward * distance;
                Camera.transform.position = moveCamNorth;

                NorthDoor.SetActive(true);
                WestDoor.SetActive(true);
                EastDoor.SetActive(true);

                Destroy(NorthA.gameObject);
                Destroy(WestA.gameObject);
                Destroy(EastA.gameObject);
                break;
            case 3:
                //Debug.Log("West");
                Vector3 moveCamEast = originalCamPos + Vector3.right * distance;
                Camera.transform.position = moveCamEast;

                NorthDoor.SetActive(true);
                SouthDoor.SetActive(true);
                EastDoor.SetActive(true);

                Destroy(NorthA.gameObject);
                Destroy(SouthA.gameObject);
                Destroy(EastA.gameObject);
                break;
            case 4:
                //Debug.Log("East");
                Vector3 moveCamWest = originalCamPos + Vector3.left * distance;
                Camera.transform.position = moveCamWest;

                NorthDoor.SetActive(true);
                WestDoor.SetActive(true);
                SouthDoor.SetActive(true);

                Destroy(NorthA.gameObject);
                Destroy(WestA.gameObject);
                Destroy(SouthA.gameObject);
                break;
        }
    }
}
