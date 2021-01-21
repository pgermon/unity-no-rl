using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;

[Action("Behaviors/DinoActions/GetCurrentPrey")]
[Help("Returns the current prey of the DinosaurBB")]

public class GetCurrentPrey : GOAction
{
    [OutParam("prey")]
    public GameObject prey;

    [OutParam("preyBool")]
    public bool preyBool;

    public override TaskStatus OnUpdate(){
        prey = gameObject.GetComponent<DinosaurBB>().getCurrentPrey();

        if(prey != null){
            preyBool = true;
        }
        else{
            preyBool = false;
        }

        return TaskStatus.COMPLETED;
    }
}
