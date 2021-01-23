using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Conditions;

[Condition("Behaviors/DinoConditions/IsPredatorNear")]
[Help("Returns True if the currentPredator of DinosaurBB is not null")]

public class IsPredatorNear : GOCondition 
{
    public override bool Check(){
        GameObject current_predator = gameObject.GetComponent<DinosaurBB>().getCurrentPredator();
        bool ret = current_predator != null;
        Debug.Log(gameObject.name.Split('(')[0] + ": predator is not null = " + ret);
        return ret;
    }
    
}
