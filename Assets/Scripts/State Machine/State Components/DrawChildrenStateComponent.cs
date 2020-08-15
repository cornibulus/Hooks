using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawChildrenStateComponent : StateComponent
{
    public GameObject spriteParent;

    private SpriteRenderer[] sprites;

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.NO_EXECUTE;
    }

    private void Awake()
    {
        sprites = spriteParent.GetComponentsInChildren<SpriteRenderer>();
    }

    public override void Enter()
    {
        if (sprites != null)
            foreach(SpriteRenderer sprite in sprites)
            {
                sprite.enabled = true;
            }
    }

    public override void Exit()
    {
        if (sprites != null)
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.enabled = false;
            }
    }
}
