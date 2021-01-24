using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;

[Action("Behaviors/DinoActions/Run")]
[Help("Runs from the predator")]

public class DinoRun : GOAction 
{

    [InParam("runSpeed")]
    public float runSpeed;

    public override TaskStatus OnUpdate(){
       
        gameObject.GetComponent<NavMeshAgent>().speed = runSpeed;

        return TaskStatus.COMPLETED;
    }
}
