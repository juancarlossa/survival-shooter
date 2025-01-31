using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/S3toS1")]
public class S3toS1Decision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool isLowHp = controller.hp;
        return isLowHp;
    }
}
