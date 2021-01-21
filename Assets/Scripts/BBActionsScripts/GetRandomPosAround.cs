using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;

[Action("Behaviors/DinoActions/GetRandomPosAround")]
[Help("Returns a random position in a range around the dino")]

public class GetRandomPosAround : GOAction
{
    [OutParam("randomPosition")]
    public Vector3 randomPosition;

    public override TaskStatus OnUpdate(){
        
        Vector3 randomDir = Quaternion.AngleAxis(Random.Range(-45.0f, 45.0f), gameObject.transform.up) * gameObject.transform.forward;
        randomPosition = gameObject.transform.position + randomDir.normalized * 20;

        return TaskStatus.COMPLETED;
    }
    
}
