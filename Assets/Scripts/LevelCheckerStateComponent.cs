using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCheckerStateComponent : StateComponent
{
    public int level;
    public State overrideState;

    public override State Execute()
    {
        if (!PlayerPrefs.HasKey(AutoLevel.LEVEL_PREF))
            return null;

        return PlayerPrefs.GetInt(AutoLevel.LEVEL_PREF) >= level ? overrideState : null;
    }

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.UPDATE;
    }
}
