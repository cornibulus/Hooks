using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullStateComponent : StateComponent {
    public bool ShouldPullPlayer { get; set; }

    public State overrideState;

    public LinkedList<GameObject> chains = new LinkedList<GameObject>();

    private bool shouldExit = false;

    public GameObject hook;
    public GameObject ItemToPull { get; set; }

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
                Player.Instance.transform.position = pos;
                hook.transform.position = hookPos;
            }
            else
            {
                GameObject chain = chains.Last.Value;
                chains.RemoveLast();
                Vector3 pos = chain.transform.position;
                GameObject.Destroy(chain);
                ItemToPull.transform.position -= (hook.transform.position - pos);
                hook.transform.position = pos;
            }

            yield return Tick.Instance.Advance();
        }
        this.shouldExit = true;
    }

    public override void Enter()
    {
        if(this.chains.Count == 0)
        {
            this.shouldExit = true;
            return;
        }
        StartCoroutine(Pull());
    }

    public override State Execute()
    {
        return shouldExit ? overrideState : null;
    }

    public override void Exit()
    {
        StopAllCoroutines();
        this.shouldExit = false;
        ItemToPull = null;
    }

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.UPDATE;
    }
}
