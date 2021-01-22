using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;

[Action("Behaviors/DinoActions/GetRandomPosAround")]
[Help("Returns a random position in a range around the dino")]

public class GetRandomPosAround : GOAction
{
    [InParam("wanderSpeed")]
    public float wanderSpeed;

    [OutParam("randomPosition")]
    public Vector3 randomPosition;

    public override TaskStatus OnUpdate(){
        
        Vector3 randomDir = Quaternion.AngleAxis(Random.Range(-180f, 180f), gameObject.transform.up) * gameObject.transform.forward;
        randomPosition = gameObject.transform.position + randomDir.normalized * Random.Range(5, 15);

        gameObject.GetComponent<NavMeshAgent>().speed = wanderSpeed;

        return TaskStatus.COMPLETED;
    }
    
}
