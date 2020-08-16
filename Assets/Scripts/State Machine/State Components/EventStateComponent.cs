using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventStateComponent : StateComponent
{
    public UnityEvent enterEvent = new UnityEvent();
    public UnityEvent exitEvent = new UnityEvent();

    public override void Enter()
    {
        enterEvent.Invoke();
    }

    public override void Exit()
    {
        exitEvent.Invoke();
    }

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.NO_EXECUTE;
    }
}
