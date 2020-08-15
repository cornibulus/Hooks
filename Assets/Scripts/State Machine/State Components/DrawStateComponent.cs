using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawStateComponent : StateComponent
{
    public SpriteRenderer sprite;

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.NO_EXECUTE;
    }

    public override void Enter()

    {
        if (sprite != null)
            sprite.enabled = true;
    }

    public override void Exit()
    {
        if (sprite != null)
            sprite.enabled = false;
    }
}
