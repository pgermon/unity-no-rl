using UnityEngine;
using Pada1.BBCore;           // Code attributes
using BBUnity.Conditions;

[Condition("Behaviors/DinoConditions/HasPredator")]
[Help("Search a predator and return true if found")]
public class HasPredator : GOCondition 
{
    [OutParam("predator")]
    [Help("The predator found")]
    public GameObject current_predator;
    
    public override bool Check(){
        current_predator = gameObject.GetComponent<DinosaurBB>().getCurrentPredator();
        bool ret = current_predator != null;
        Debug.Log(gameObject.name.Split('(')[0] + ": predator is not null = " + ret);
        return ret;
    }
    
}