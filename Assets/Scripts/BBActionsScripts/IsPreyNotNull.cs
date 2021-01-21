using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Conditions;

[Condition("Behaviors/DinoConditions/IsPreyNotNull")]
[Help("Returns True if the currentPrey of DinosaurBB is not null")]

public class IsPreyNotNull : GOCondition 
{

    public override bool Check(){
        GameObject current_prey = gameObject.GetComponent<DinosaurBB>().getCurrentPrey();
        bool ret = current_prey != null;
        Debug.Log(gameObject.name.Split('(')[0] + ": prey is not null = " + ret);
        return ret;
    }
    
}
