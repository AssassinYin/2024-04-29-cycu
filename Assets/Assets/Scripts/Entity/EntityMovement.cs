using UnityEngine;

public abstract class EntityMovement : MonoBehaviour
{

    private Vector3 walkPoint;

    protected abstract void StateChange();

    protected abstract void Attacking();

    protected abstract void Chasing();

    protected abstract void Patroling();
}
