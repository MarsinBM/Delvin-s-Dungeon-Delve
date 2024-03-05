using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, 40f * Time.deltaTime);
        transform.Rotate(Vector3.right, 30f * Time.deltaTime);
        transform.Rotate(Vector3.forward, 45f * Time.deltaTime);
    }
}
