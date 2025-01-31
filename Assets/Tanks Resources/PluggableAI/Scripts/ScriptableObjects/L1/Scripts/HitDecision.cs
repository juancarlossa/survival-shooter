using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Hit")]
public class HitDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        //controller.rot = false;
        bool hit = IsHit(controller);
        //Debug.Log("Stan 1!!!: " + arrivedOrHit);
        //if (controller.rot == true) return false;
        return hit;
    }

    private bool IsHit(StateController controller)
    {

        return controller.tankHealth.IsHit;
    }
}