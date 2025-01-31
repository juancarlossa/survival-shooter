using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/RumAway")]
public class RunAwayDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool isLowHP = IsLowHP(controller);
        return isLowHP;
    }

    private bool IsLowHP(StateController controller)
    {
        float tankHp = controller.tankHealth.CurrentHealth;
        if (tankHp < controller.tankHealth.MaxHealth)
        {
            return true;
        } else
        {
            return false;
        }
    }
}