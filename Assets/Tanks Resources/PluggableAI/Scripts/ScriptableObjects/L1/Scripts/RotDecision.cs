using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Rot")]
public class RotDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool alreadyRotation = IsRot(controller);
        return alreadyRotation;
    }

    private bool IsRot(StateController controller)
    {
        if (controller.totalRotation >= 360f)
        {
            //Resetamos el bool isHit para que no se siga con el estado 0 y no se bugee
            controller.isRotating = false;
            controller.totalRotation = 0f;
            //Debug.Log("Roation Complete!!");
            controller.rot = true;
        }
        //Debug.Log("Controller.rot value: " + controller.rot);
        return controller.rot;
    }

    
}