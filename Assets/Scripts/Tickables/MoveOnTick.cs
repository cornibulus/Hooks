using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTick : Tickable
{
    public Vector2Int direction = Vector2Int.down;

    public LayerMask layerMask;

    protected bool isDoneMoving = false;
    public bool shouldDestroyWhenDoneMoving = true;

    public override void Tick()
    {
        if (isDoneMoving)
            return;

        Vector3 testPoint = new Vector3(
                transform.position.x + direction.x,
                transform.position.y + direction.y
            );

        Collider2D result = Physics2D.OverlapPoint(
                testPoint,
                layerMask);

        if(result == null)
        {
            transform.position = testPoint;
        }
        else
        {
            isDoneMoving = true;
        }
    }

    private void Update()
    {
        if(this.shouldDestroyWhenDoneMoving && this.isDoneMoving)
        {
            Destroy(this.gameObject);
        }
    }
}
