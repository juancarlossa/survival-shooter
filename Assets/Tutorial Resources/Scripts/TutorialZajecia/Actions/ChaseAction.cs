using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action 
{
    public override void Act (StateController controller)
    {
        Chase (controller);    
    }

    private void Chase(StateController controller)
    {
        controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.isStopped = false;

        Vector3 directionToTarget = controller.chaseTarget.position - controller.transform.position;
        if (controller.navMeshAgent.remainingDistance > 5f)
        {
            controller.navMeshAgent.isStopped = false;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRotation, Time.deltaTime * controller.enemyStats.tankRotationSpeed);

            // Mover el tanque hacia adelante en la dirección actual
            //controller.transform.position += controller.transform.forward * controller.enemyStats.moveSpeed * Time.deltaTime;
        } else
        {
            controller.navMeshAgent.isStopped = true;
        }
    }
}