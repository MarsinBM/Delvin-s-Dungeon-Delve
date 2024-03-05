using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Introtext : MonoBehaviour
{
    // Variables
    public float shakeAmount = 10f;

    private Vector3 originalTextPos;

    void Start()
    {
        originalTextPos = transform.localPosition;
        StartCoroutine(ShakeText());
    }

    IEnumerator ShakeText()
    {
        while (true)
        {
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);

            transform.localPosition = originalTextPos + new Vector3(offsetX, offsetY, 0);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
