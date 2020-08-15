using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneStateComponent : StateComponent
{
    [Header("Will reload current scene if empty")]
    public string sceneName;

    public override void Enter()
    {
        if(this.sceneName == null || this.sceneName == "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.NO_EXECUTE;
    }
}
