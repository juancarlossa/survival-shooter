using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Arrived")]
public class ArrivedDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        //controller.rot = false;
        bool arrived = IsArrived(controller);
        //Debug.Log("Stan 1!!!: " + arrivedOrHit);
        //if (controller.rot == true) return false;
        return arrived;
    }

    private bool IsArrived(StateController controller)
    {
        return controller.arrived;
    }

}