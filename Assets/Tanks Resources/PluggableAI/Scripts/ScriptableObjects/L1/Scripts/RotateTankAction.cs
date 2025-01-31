using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/RotateTank")]
public class RotateTankAction : Action
{
    //public float rotationSpeed = 60f; // Velocidad de rotación en grados por segundo


    public override void Act(StateController controller)
    {

        Rotate(controller);
    }

    private void Rotate(StateController controller)
    {
        //controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.isStopped = true;
        controller.navMeshAgent.updateRotation = false;

        float rotationAmount = controller.tankRotationSpeed * Time.deltaTime;
        controller.transform.Rotate(Vector3.up, rotationAmount);
        controller.totalRotation += rotationAmount; 

    }
    
}