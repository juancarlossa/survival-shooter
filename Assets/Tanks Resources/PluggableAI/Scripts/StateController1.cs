using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Complete;

public class StateController1 : MonoBehaviour 
{
	[SerializeField] private Transform _wayPointContainer;

	public EnemyStats enemyStats;
	public Transform eyes;

	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public TankShooting tankShooting;
	[HideInInspector] public Transform[] wayPointList = {};

	private bool aiActive;

	void Awake () 
	{
		tankShooting = GetComponent<TankShooting>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.enabled = true;
		aiActive = true;

		if (_wayPointContainer != null)
		{
			wayPointList = new Transform[_wayPointContainer.childCount];
			for (int i = 0; i < _wayPointContainer.childCount; i++)
				wayPointList[i] = _wayPointContainer.GetChild(i);
		}
	}
}