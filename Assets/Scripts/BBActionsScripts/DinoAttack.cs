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

    public override TaskStatus OnUpdate(){
        gameObject.GetComponent<DinosaurBB>().attack();
        return TaskStatus.COMPLETED;
    }
}
