using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMoveStateComponent : StateComponent
{
    public GameObject hook;
    public State overrideState;
    private bool shouldExit = false;

    public GameObject chainPrefab;

    [Header("For collision detection")]
    public LayerMask layerMask;

    public Vector2Int moveDirection = Vector2Int.right;

    private Collider2D[] results = new Collider2D[5];

    IEnumerator CheckAndMove()
    {
        int resultsCount = 0;
        do
        {
            //check one time further
            resultsCount = Physics2D.OverlapPointNonAlloc(
                new Vector2(hook.transform.position.x, hook.transform.position.y) + this.moveDirection,
                results,
                layerMask);

            Tick.Instance.Advance();

            if(resultsCount == 0)
            {
                //move
                Vector3 oldPosition = hook.transform.position;
                hook.transform.position = new Vector3(
                        hook.transform.position.x + this.moveDirection.x,
                        hook.transform.position.y + this.moveDirection.y
                    );

                //spawn chain TODO on a specific cleanup object
                GameObject instance = GameObject.Instantiate(chainPrefab);
                instance.transform.position = oldPosition;
            }

            yield return new WaitForSeconds(Tick.Instance.seconds);
        }
        while (resultsCount == 0);
        shouldExit = true;
    }

    public override void Enter()
    {
        if(hook == null)
        {
            throw new MissingReferenceException("No hook assigned to " + this.name);
        }
        StartCoroutine(CheckAndMove());
    }

    public override State Execute()
    {
        if(this.shouldExit)
        {
            return overrideState;
        }

        return null;
    }

    public override void Exit()
    {
        this.shouldExit = false;
    }

    public override StateComponentType GetStateComponentType()
    {
        return StateComponentType.UPDATE;
    }
}
