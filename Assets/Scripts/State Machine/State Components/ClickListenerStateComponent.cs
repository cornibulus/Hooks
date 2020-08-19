using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListenerStateComponent : StateComponent
{
    public Collider2D area;
    public State overrideState;

    public override State Execute()
    {
        if (Input.GetMouseButtonDown(0) 
            && (area == null 
                || area.bounds.Contains((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition))))
        {
            return overrideState;
        }

        return null;
    }

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.UPDATE;
    }
}
