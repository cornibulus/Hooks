using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListenerStateComponent : StateComponent {

    public KeyCode key;
    public State overrideState;

    public override State Execute()
    {
        if(Input.GetKey(key))
        {
            return overrideState;
        }

        return null;
    }

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.FIXED_UPDATE;
    }
}
