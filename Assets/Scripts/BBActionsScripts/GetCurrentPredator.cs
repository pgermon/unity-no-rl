using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;

[Action("Behaviors/DinoActions/GetCurrentPredator")]
[Help("Gets the current predator of the DinosaurBB and sets predatorBool accordingly")]

public class GetCurrentPredator : GOAction
{
    [OutParam("predator")]
    public GameObject predator;

    [OutParam("predatorBool")]
    public bool predatorBool;

    public override TaskStatus OnUpdate(){
        predator = gameObject.GetComponent<DinosaurBB>().getCurrentPredator();

        if(predator != null){
            predatorBool = true;
        }
        else{
            predatorBool = false;
        }
        
        return TaskStatus.COMPLETED;
    }
}
