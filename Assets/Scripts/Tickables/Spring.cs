using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Spring : Tickable
{

    public Vector2Int direction = Vector2Int.up;
    public SpringMoveStateComponent overrideState;

    private bool hasFired = false;

    public override void Tick()
    {
        if (hasFired)
            return;

        Collider2D overlap = Physics2D.OverlapPoint(
               new Vector2(transform.position.x, transform.position.y));
        if (overlap == null)
            return;

        StateMachine sm = overlap.GetComponent<StateMachine>();
        if(sm != null && Player.Instance.gameObject == sm.gameObject)
        {
            overrideState.ObjectToMove = overlap.gameObject;
            overrideState.PreviousState = sm.state;
            overrideState.Direction = direction;
            sm.Transition(this.overrideState.GetComponent<State>());

            this.hasFired = true;
            GetComponent<Animator>().SetTrigger("fire");
            this.gameObject.layer = LayerMask.NameToLayer("Wall");
            return;
        }

        MoveOnTick mot = overlap.GetComponent<MoveOnTick>();
        if(mot != null)
        {
            mot.direction = direction;

            this.hasFired = true;
            GetComponent<Animator>().SetTrigger("fire");
            this.gameObject.layer = LayerMask.NameToLayer("Wall");
        }
    }
}
