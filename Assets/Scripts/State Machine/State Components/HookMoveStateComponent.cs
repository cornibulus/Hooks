﻿using System.Collections;
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
                (Vector2) hook.transform.position + this.moveDirection,
                layerMask);

            if (result == null || (result != null && "Lock" == result.tag && Player.Instance.Keys > 0))
            {
                if (result != null && "Lock" == result.tag) //hit lock
                {
                    if (SoundManager.Instance != null)
                        SoundManager.Instance.PlayUnlockAudio();
                    Player.Instance.Keys--;
                    GameObject.Destroy(result.gameObject);
                }

                //Play sound one tick early because it sounds delayed otherwise
                if (Physics2D.OverlapPoint((Vector2)hook.transform.position + this.moveDirection * 2, layerMask) != null)
                {
                    if (SoundManager.Instance != null)
                        SoundManager.Instance.PlayHookAudio();
                }

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
            else //hit
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
