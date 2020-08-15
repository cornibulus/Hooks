using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTick : Tickable
{
    public Vector2Int direction = Vector2Int.down;

    public override void Tick()
    {
        this.transform.position = new Vector3(
                transform.position.x + direction.x,
                transform.position.y + direction.y
            );
    }
}
