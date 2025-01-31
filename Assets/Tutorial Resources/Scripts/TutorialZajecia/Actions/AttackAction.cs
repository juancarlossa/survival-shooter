using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action 
{
    public override void Act (StateController controller)
    {
        RotateTurretTowardsTarget(controller);
        Attack(controller);  
    }

    private void Attack(StateController controller)
    {
        RaycastHit hit;

        Debug.DrawRay (controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.red);
        if (Physics.SphereCast (controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.attackRange)
            && hit.collider.CompareTag ("Player")) 
        {
            if (controller.CheckIfCountDownElapsed (controller.enemyStats.attackRate)) 
            {
                controller.tankShooting.Fire(controller.enemyStats.attackForce, controller.enemyStats.attackRate);
            }
        }
    }

    // Método para rotar la torreta hacia el jugador
    private void RotateTurretTowardsTarget(StateController controller)
    {
        // Obtenemos la dirección hacia el jugador
        Vector3 directionToPlayer = controller.chaseTarget.position - controller.turret.position;
        directionToPlayer.y = 0;  // Aseguramos que solo gire en el eje Y

        // Calculamos la rotación necesaria para apuntar al jugador
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Rotamos suavemente la torreta hacia el jugador
        controller.turret.rotation = Quaternion.Slerp(controller.turret.rotation, targetRotation, Time.deltaTime * controller.enemyStats.turretRotationSpeed);
    }
}