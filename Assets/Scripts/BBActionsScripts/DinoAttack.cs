using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;

[Action("Behaviors/DinoActions/Attack")]
[Help("Plays the attack animation")]

public class DinoAttack : GOAction
{  
    [InParam("prey")]
    public GameObject prey;

    [InParam("attackDistance")]
    public float attackDistance;

    public override TaskStatus OnUpdate(){

        /*Debug.Log("attack distance = " + attackDistance);
        Debug.LogWarning("distance to prey = " + (gameObject.transform.position - prey.transform.position).sqrMagnitude);
        Debug.LogWarning("attack distance = " + attackDistance * attackDistance);
        Debug.LogWarning("distance to prey < attack distance = " + ((gameObject.transform.position - prey.transform.position).sqrMagnitude < attackDistance * attackDistance));*/

        if (gameObject.GetComponent<DinosaurBB>().attack(prey))
            return TaskStatus.COMPLETED;
        return TaskStatus.FAILED;
    }
}
