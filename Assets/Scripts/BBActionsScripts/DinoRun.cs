using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;

[Action("Behaviors/DinoActions/Run")]
[Help("Runs from the predator")]

public class DinoRun : GOAction 
{
    [InParam("animator")]
    public Animator anim;  

    [InParam("predator")]
    public GameObject predator;

    [InParam("area")]
    public GameObject area;

    [OutParam("runToPosition")]
    public Vector3 position;

    public override TaskStatus OnUpdate(){

        Debug.Log(gameObject.name + " running from " + predator.gameObject.name);
        Vector3 runDirection = gameObject.transform.position - predator.gameObject.transform.position;
        //float distance = runDirection.magnitude;

        position = gameObject.transform.position + runDirection;

        //anim.Play("Base Layer.Run");
        return TaskStatus.COMPLETED;
    }
}
