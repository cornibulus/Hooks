using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnInterval : Tickable {
    public GameObject prefab;
    public int interval = 5;
    public Transform spawnPosition;

    private int current;

    private void Awake()
    {
        current = interval;
    }

    public override void Tick()
    {
        if(current-- <= 0)
        {
            GameObject obj = Instantiate(this.prefab);
            obj.transform.position = spawnPosition != null ? spawnPosition.position : this.transform.position;
            current = interval;
        }
    }
}
