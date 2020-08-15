using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    protected List<StateComponent> updateComponents = new List<StateComponent>();

    protected List<StateComponent> fixedUpdateComponents = new List<StateComponent>();

    protected List<StateComponent>  noExecuteComponents = new List<StateComponent>();

    public State PreviousState { get; set; }

    public void Awake()
    {
        StateComponent[] components = GetComponents<StateComponent>();

        foreach(StateComponent component in components)
        {
            this.Register(component);
        }
    }

    public void Register(StateComponent component)
    {
        if (component == null) return;
        switch (component.GetStateComponentType())
        {
            case StateComponentType.FIXED_UPDATE:
                this.fixedUpdateComponents.Add(component);
                break;
            case StateComponentType.UPDATE:
                this.updateComponents.Add(component);
                break;
            default:
                this.noExecuteComponents.Add(component);
                break;
        }
    }

    public State Execute()
    {
        foreach (StateComponent component in this.updateComponents)
        {
            State newState = component.Execute();

            if (newState != null)
            {
                return newState;
            }
        }

        return null;
    }

    public State FixedExecute()
    {
        foreach (StateComponent component in this.fixedUpdateComponents)
        {
            State newState = component.Execute();

            if (newState != null)
            {
                return newState;
            }
        }

        return null;
    }

    public virtual void Enter()
    {
        foreach(StateComponent component in this.updateComponents)
        {
            component.Enter();
        }

        foreach (StateComponent component in this.fixedUpdateComponents)
        {
            component.Enter();
        }

        foreach (StateComponent component in this.noExecuteComponents)
        {
            component.Enter();
        }
    }

    public virtual void Exit()
    {
        foreach (StateComponent component in this.updateComponents)
        {
            component.Exit();
        }

        foreach (StateComponent component in this.fixedUpdateComponents)
        {
            component.Exit();
        }

        foreach (StateComponent component in this.noExecuteComponents)
        {
            component.Exit();
        }
    }
}
