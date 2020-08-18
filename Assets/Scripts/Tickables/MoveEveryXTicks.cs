using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEveryXTicks : MoveOnTick {
    public int numTicks = 2;
    private int count = 0;

    public override void Tick()
    {
        if (++count >= numTicks)
        {
            base.Tick();
            count = 0;
        }
    }
}
