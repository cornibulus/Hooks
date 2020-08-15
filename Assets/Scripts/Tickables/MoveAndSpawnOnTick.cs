using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndSpawnOnTick : MoveOnTick
{
    public GameObject prefab;

    public override void Tick()
    {
        if (isDoneMoving)
            return;

        GameObject instance = Instantiate(prefab);
        instance.transform.position = this.transform.position;

        base.Tick();
    }
}
