using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullStateComponent : StateComponent {
    public bool ShouldPullPlayer { get; set; }

    public State overrideState;

    public LinkedList<GameObject> chains = new LinkedList<GameObject>();

    private bool shouldExit = false;

    public GameObject hook;
    public GameObject player;

    IEnumerator Pull()
    {
        while (this.chains.Count > 0)
        {
            if(ShouldPullPlayer)
            {
                GameObject chain = chains.First.Value;
                chains.RemoveFirst();
                Vector3 hookPos = hook.transform.position;
                Vector3 pos = chain.transform.position;
                GameObject.Destroy(chain);
                player.transform.position = pos;
                hook.transform.position = hookPos;
            }
            else
            {
                GameObject chain = chains.Last.Value;
                chains.RemoveLast();
                Vector3 pos = chain.transform.position;
                GameObject.Destroy(chain);
                hook.transform.position = pos;
            }

            yield return Tick.Instance.Advance();
        }
        this.shouldExit = true;
    }

    public override void Enter()
    {
        StartCoroutine(Pull());
    }

    public override State Execute()
    {
        if (this.shouldExit)
            return overrideState;

        return null;
    }

    public override void Exit()
    {
        this.shouldExit = false;
        chains.Clear();
        chains = new LinkedList<GameObject>();
    }

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.UPDATE;
    }
}
