using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathOnTick : MoveOnTick {
    public Transform[] path;

    private int index = 0;

    public override void Tick()
    {
        if(index >= path.Length)
        {
            if (this.shouldDestroyWhenDoneMoving)
                this.isDoneMoving = true;
            
            index = 0;
        }

        Collider2D result = Physics2D.OverlapPoint(
            path[index].position,
            layerMask);

        if (result == null)
        {
            transform.position = path[index++].position;
        }
        else
        {
            isDoneMoving = true;
        }
    }
}
