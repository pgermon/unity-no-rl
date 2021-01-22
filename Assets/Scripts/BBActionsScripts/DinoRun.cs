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
    [InParam("predator")]
    public GameObject predator;

    [InParam("runSpeed")]
    public float runSpeed;

    [OutParam("runToPosition")]
    public Vector3 position;

    public override TaskStatus OnUpdate(){

        Debug.Log(gameObject.name + " running from " + predator.gameObject.name);
        Vector3 runDirection = gameObject.transform.position - predator.gameObject.transform.position;
        position = gameObject.transform.position + runDirection;

        gameObject.GetComponent<NavMeshAgent>().speed = runSpeed;

        return TaskStatus.COMPLETED;
    }
}
