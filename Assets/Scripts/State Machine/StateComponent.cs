using UnityEngine;

[RequireComponent(typeof(State))]
public abstract class StateComponent : MonoBehaviour {
    public virtual void Enter() { }

    public virtual State Execute() { return null; }

    public virtual void Exit() { }

    public abstract StateComponentType GetStateComponentType();
}