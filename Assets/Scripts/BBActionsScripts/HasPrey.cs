using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Conditions;

/** @brief Charge la proie dans l'arbre.
 *
 * Si une proie a été choisie par \link DinosaurBB::getCurrentPrey() \endlink,
 * importe l'objet dans l'arbre de comportements et retourne true.
 */
[Condition("Behaviors/DinoConditions/HasPrey")]
[Help("Search a prey and true if found")]
public class HasPrey : GOCondition 
{
    [OutParam("prey")]
    [Help("The prey found")]
    public GameObject prey;

    public override bool Check(){
        prey = gameObject.GetComponent<DinosaurBB>().getCurrentPrey();
        bool ret = prey != null;
        //Debug.Log(gameObject.name.Split('(')[0] + ": prey is not null = " + ret);
        return ret;
    }
    
}
