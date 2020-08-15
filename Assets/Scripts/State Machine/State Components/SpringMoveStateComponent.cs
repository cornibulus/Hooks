using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringMoveStateComponent : StateComponent
{
    private bool shouldExit = false;

    public GameObject ObjectToMove { get; set; }
    public State PreviousState { get; set; }

    public Vector2Int direction = Vector2Int.up;

    public LayerMask layerMask;

    IEnumerator MoveObject()
    {
        bool isMovingPlayer = ObjectToMove == Player.Instance.gameObject;
        Collider2D result = Physics2D.OverlapPoint(
                new Vector2(ObjectToMove.transform.position.x, ObjectToMove.transform.position.y) + this.direction,
                layerMask);
        GameObject[] chains = null;
        PullStateComponent psc = PreviousState.GetComponent<PullStateComponent>();
        if(psc != null)
        {
            chains = new GameObject[psc.chains.Count];
            psc.chains.CopyTo(chains, 0);
        }

        while (result == null)
        {
            //move
            Vector3 oldPosition = ObjectToMove.transform.position;
            ObjectToMove.transform.position = new Vector3(
                ObjectToMove.transform.position.x + this.direction.x,
                ObjectToMove.transform.position.y + this.direction.y);

            if(chains != null)
            {
                foreach(GameObject chain in chains)
                {
                    chain.transform.position = new Vector3(
                        chain.transform.position.x + this.direction.x,
                        chain.transform.position.y + this.direction.y);
                }
            }

            result = Physics2D.OverlapPoint(
                new Vector2(ObjectToMove.transform.position.x, ObjectToMove.transform.position.y) + this.direction,
                layerMask);

            yield return isMovingPlayer ? Tick.Instance.Advance() : Tick.Instance.DontAdvance();
        }
        this.shouldExit = true;
    }

    public override void Enter()
    {
        StartCoroutine(MoveObject());
    }

    public override State Execute()
    {
        return this.shouldExit ? PreviousState : null;
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
