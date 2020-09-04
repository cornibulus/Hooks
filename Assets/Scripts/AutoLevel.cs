using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NumberDisplay))]
public class AutoLevel : MonoBehaviour {

    public static readonly string LEVEL_PREF = "HighestLevel";

	void Start () {
        GetComponent<NumberDisplay>().number = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(LEVEL_PREF, SceneManager.GetActiveScene().buildIndex);
    }
}
