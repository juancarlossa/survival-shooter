using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Hp")]
public class HpDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        Hp(controller);
        bool isLowHp = controller.hp;

        return isLowHp;
    }

    private void Hp(StateController controller)
    {
        RaycastHit hit;

        float maxHp = controller.tankHealth.MaxHealth;
        float hp = controller.tankHealth.CurrentHealth;

        if (hp < (maxHp * 0.25))
        {
            controller.hp = true;
        }
        else
        {
            controller.hp = false;
        }
    }
}
