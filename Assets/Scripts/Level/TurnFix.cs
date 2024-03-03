using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnFix : MonoBehaviour
{
    // This class is used as a fix for an issue that occurs if the game starts with no enemies what so ever

    void Start()
    {
        Destroy(gameObject); 
    }

}
