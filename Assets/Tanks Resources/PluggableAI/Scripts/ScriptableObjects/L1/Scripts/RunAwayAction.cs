using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/RunAway")]
public class RunAwayAction : Action
{
    public override void Act(StateController controller)
    {
        RunAway(controller);
    }

    private void RunAway(StateController controller)
    {
        
        controller.navMeshAgent.isStopped = false;
        controller.navMeshAgent.updateRotation = true;

        Vector3 directionToTarget = (controller.transform.position - controller.playerPosition.position).normalized; // Calcula la dirección opuesta
        if (directionToTarget.magnitude > 1f)
        {
            //Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            //controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRotation, Time.deltaTime * controller.enemyStats.tankRotationSpeed);
        }
        Vector3 fleeDestination = controller.transform.position + directionToTarget * 10; // Ajusta la distancia que deseas huir
        controller.navMeshAgent.destination = fleeDestination;
        //Debug.Log(controller.chaseTarget.position);


        
        // Calcular la rotación objetivo
        //targetRotation = Quaternion.LookRotation(directionToTarget);

        // Rotar suavemente el tanque hacia el objetivo
        //controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRotation, Time.deltaTime * controller.enemyStats.tankRotationSpeed);

        // Mover el tanque hacia adelante en la dirección actual
        //controller.transform.position += controller.transform.forward * controller.enemyStats.moveSpeed * Time.deltaTime;
    }
}