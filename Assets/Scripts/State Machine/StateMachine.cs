using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State state;

    private void Start()
    {
        if (this.state != null)
        {
            this.state.Enter();
        }
    }

    public void Update()
    {
        if(this.state == null)
        {
            Debug.Log("State Machine " + this.gameObject.name + " has no state!", this.gameObject);
            return;
        }

        State newState = this.state.Execute();

        if (newState != null)
        {
            this.Transition(newState);
        }
    }

    public void FixedUpdate()
    {
        if (this.state == null)
        {
            Debug.Log("State Machine " + this.gameObject.name + " has no state!", this.gameObject);
            return;
        }

        State newState = this.state.FixedExecute();

        if (newState != null)
        {
            this.Transition(newState);
        }
    }

    public void Transition(State newState)
    {
        if (newState == null)
            return;
        if (this.state != null)
        {
            this.state.Exit();
        }
        newState.PreviousState = this.state;
        this.state = newState;
        newState.Enter();
    }
}
