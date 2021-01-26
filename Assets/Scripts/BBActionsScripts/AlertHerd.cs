using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;
using UnityEngine.AI;

[Action("Behaviors/DinoActions/AlertHerd")]
[Help("Send a message to the herd-mates of the dino that are in range to alert them of the presence of a predator or a prey")]
public class AlertHerd : GOAction
{
    [InParam("prey")]
    public GameObject prey;

    public override TaskStatus OnUpdate(){

        gameObject.GetComponent<DinosaurBB>().setIsWandering(false);
        
        List<GameObject> herd_in_range = gameObject.GetComponent<DinosaurBB>().getHerdInRange();

        foreach(GameObject mate in herd_in_range){
            if(mate != null){
                mate.GetComponent<DinosaurBB>().OnHerdAlert(gameObject, prey);
            }
        }
        return TaskStatus.COMPLETED;
        
    }
}
