using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NumberDisplay))]
public class AutoLevel : MonoBehaviour {
	void Start () {
        GetComponent<NumberDisplay>().number = SceneManager.GetActiveScene().buildIndex;
    }
}
