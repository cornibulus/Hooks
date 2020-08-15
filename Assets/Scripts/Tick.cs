using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : MonoBehaviour
{
    public float seconds = 1f;

    public WaitForSeconds Advance()
    {
        return new WaitForSeconds(seconds);
    }



    //Singleton boilerplate
    private static Tick _instance;

    public static Tick Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
