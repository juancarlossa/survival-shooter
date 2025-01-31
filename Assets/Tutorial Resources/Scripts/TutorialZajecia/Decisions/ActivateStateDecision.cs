using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision 
{
    public override bool Decide (StateController controller)
    {
        if (!IsPlayerDead (controller))
        {
            bool chaseTargetIsActive = controller.chaseTarget.gameObject.activeSelf;
            controller.rot = false;
            return chaseTargetIsActive;
        } else
        {
            return false;
        }

    }

    public bool IsPlayerDead(StateController controller)
    {
        float playerHealth = controller.playerHealth.CurrentHealth;

        if (playerHealth < 1)
        {
            return true;
        } else
        {
            return false;
        }
    }

}