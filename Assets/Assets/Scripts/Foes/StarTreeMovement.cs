using UnityEngine;

public class StarTreeMovement : MonoBehaviour
{
    [SerializeField] private float walkPointRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask playerLayer;

    public bool IsFacingRight { get; private set; }

    private Vector3 walkPoint;

    private void Awake()
    {
        
    }

    private void Start()
    {
        IsFacingRight = true;
    }

    void Update()
    {
        StateChange();
    }

    private void StateChange()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();

        else if (playerInSightRange && !playerInAttackRange)
            Chasing();

        else if (playerInAttackRange && playerInSightRange)
            Attacking();
    }

    private void Attacking()
    {
        
    }

    private void Chasing()
    {
        throw new System.NotImplementedException();
    }

    private void Patroling()
    {
        throw new System.NotImplementedException();
    }
}
