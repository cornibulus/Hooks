using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDisplay : MonoBehaviour {
    [Range(0, 99)]
    public int number = 0;
    private int cachedNumber = -1;

    [Header("index 0 = '0', index 1 = '1', etc.")]
    public GameObject[] digitPrefabs;

    public Transform tensPosition;
    public Transform onesPosition;

    private void Start()
    {
        UpdateNumber();
    }

    private void Update()
    {
        UpdateNumber();
    }

    void UpdateNumber()
    {
        if (this.cachedNumber == number)
            return;

        this.cachedNumber = number;

        //clean up children
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        int firstDigit = Mathf.FloorToInt(number / 10f);
        GameObject fdObj = Instantiate(digitPrefabs[firstDigit], this.transform, true);
        fdObj.transform.position = tensPosition.position;

        int secondDigit = number % 10;
        GameObject sdObj = Instantiate(digitPrefabs[secondDigit], this.transform, true);
        sdObj.transform.position = onesPosition.position;
    }
}
