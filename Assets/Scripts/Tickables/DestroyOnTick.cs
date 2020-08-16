using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTick : Tickable
{
    public int delay = 0;

    public override void Tick()
    {
        if(delay-- <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
