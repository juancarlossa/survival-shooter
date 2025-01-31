using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Complete;

public class StateController : MonoBehaviour 
{
    public State currentState;
	public EnemyStats enemyStats;
	public Transform eyes;
    public Transform turret;
    public float tankRotationSpeed;

    public bool arrived;
    public bool hp;
    public bool rot;
    public bool hit;

    public bool isRotating = false;
    public float totalRotation = 0f;

    public List<Transform> wayPointList;
    //[SerializeField] private Transform _patrolPointContainer;

    [HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public TankShooting tankShooting;
    [HideInInspector] public IHealth tankHealth;
    [HideInInspector] public IHealth playerHealth;
    [HideInInspector] public Transform playerPosition;

    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    private bool aiActive;


    void Awake () 
	{
        tankRotationSpeed = GetComponent<TankMovement>().m_TurnSpeed;
		tankShooting = GetComponent<TankShooting> ();
        tankHealth = GetComponent<TankHealth>();
        playerHealth = GameObject.FindGameObjectWithTag("Player")?.GetComponent<IHealth>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        chaseTarget = GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent> ();

        SetupAI(true, wayPointList);
	}

	public void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
    {
        wayPointList = wayPointsFromTankManager;
        aiActive = aiActivationFromTankManager;

        if (aiActive) 
		{
			navMeshAgent.enabled = true;
		} 
        else 
		{
			navMeshAgent.enabled = false;
		}
	}

    void Update()
    {
        if (!aiActive)
            return;

        stateTimeElapsed += Time.deltaTime;
        currentState.UpdateState(this);
    }

    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
        }
    }

    public void TransitionToState(State nextState)
    {
    //    if (nextState != remainState)
    //    {
    //        currentState = nextState;
    //        OnExitState();
    //    }
        currentState = nextState;
        OnExitState();
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        //stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
        
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
        ResetStateFlags();
        //resetowac totalrotation
        Debug.Log("Transitioning to state: " + currentState.name);
    }

    public void ResetStateFlags()
    {
        arrived = false;
        rot = false;
        isRotating = false;
        totalRotation = 0f;
    }
}