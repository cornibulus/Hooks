using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneStateComponent : StateComponent
{
    public override void Enter()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.NO_EXECUTE;
    }
}
