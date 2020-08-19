using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Spring : MonoBehaviour
{

    public Vector2Int direction = Vector2Int.up;
    public SpringMoveStateComponent overrideState;
    public LayerMask layerMask;

    private bool hasFired = false;

    public void Update()
    {
        if (hasFired)
            return;

        Collider2D overlap = Physics2D.OverlapPoint(
               (Vector2) transform.position, layerMask);
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
