using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMoveStateComponent : StateComponent
{
    public GameObject hook;
    public State pullState;
    private bool shouldExit = false;

    public GameObject chainPrefab;

    [Header("For collision detection")]
    public LayerMask layerMask;

    public Vector2Int moveDirection = Vector2Int.right;
    private PullStateComponent pullStateComponent;

    private void Awake()
    {
        this.pullStateComponent = this.pullState.GetComponent<PullStateComponent>();
    }

    IEnumerator CheckAndMove()
    {
        Collider2D result;
        do
        {
            //check one time further
            result = Physics2D.OverlapPoint(
                new Vector2(hook.transform.position.x, hook.transform.position.y) + this.moveDirection,
                layerMask);

            if (result == null)
            {
                //move
                Vector3 oldPosition = hook.transform.position;
                hook.transform.position = new Vector3(
                        hook.transform.position.x + this.moveDirection.x,
                        hook.transform.position.y + this.moveDirection.y
                    );

                GameObject instance = GameObject.Instantiate(chainPrefab);
                pullStateComponent.chains.AddLast(instance);
                instance.transform.position = oldPosition;

                yield return Tick.Instance.Advance();
            }
            else
            {
                pullStateComponent.ShouldPullPlayer = LayerMask.NameToLayer("Wall") == result.gameObject.layer;
                pullStateComponent.ItemToPull = result.gameObject;
            }
        }
        while (result == null);
        shouldExit = true;
    }

    public override void Enter()
    {
        if(hook == null)
        {
            throw new MissingReferenceException("No hook assigned to " + this.name);
        }

        //don't even start if we find a collision
        if (Physics2D.OverlapPoint(
                hook.transform.position,
                layerMask) != null)
        {
            this.shouldExit = true;
            return;
        }

        StartCoroutine(CheckAndMove());
    }

    public override State Execute()
    {
        return shouldExit ? pullState : null;
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
