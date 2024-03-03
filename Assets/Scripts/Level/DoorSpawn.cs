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

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.name == "Player")
        {
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

        switch (direction)
        {
            case 1:
                //Debug.Log("North");
                SouthDoor.SetActive(true);
                WestDoor.SetActive(true);
                EastDoor.SetActive(true);

                Destroy(SouthA.gameObject);
                Destroy(WestA.gameObject);
                Destroy(EastA.gameObject);
                break;
            case 2:
                //Debug.Log("South");
                NorthDoor.SetActive(true);
                WestDoor.SetActive(true);
                EastDoor.SetActive(true);

                Destroy(NorthA.gameObject);
                Destroy(WestA.gameObject);
                Destroy(EastA.gameObject);
                break;
            case 3:
                //Debug.Log("West");
                NorthDoor.SetActive(true);
                SouthDoor.SetActive(true);
                EastDoor.SetActive(true);

                Destroy(NorthA.gameObject);
                Destroy(SouthA.gameObject);
                Destroy(EastA.gameObject);
                break;
            case 4:
                //Debug.Log("East");
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
