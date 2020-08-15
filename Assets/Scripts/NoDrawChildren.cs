using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDrawChildren : MonoBehaviour
{ 
    private void Awake()
    {
        foreach(SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = false;
        }
    }
}
