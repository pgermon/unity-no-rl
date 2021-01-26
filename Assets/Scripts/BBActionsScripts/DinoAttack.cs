using UnityEngine;
 
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;

/** @brief Le dinasaure attaque.
 *
 * Déclenche l'action d'attaque {@link DinosaurBB::attack(GameObject)}.
 * Cette action peut échouer, par exemple si la cible est trop loin.
 */
[Action("Behaviors/DinoActions/Attack")]
[Help("Plays the attack animation")]
public class DinoAttack : GOAction
{  
    [InParam("prey")]
    public GameObject prey;

    public override TaskStatus OnUpdate(){

        if (gameObject.GetComponent<DinosaurBB>().attack(prey))
            return TaskStatus.COMPLETED;
        return TaskStatus.FAILED;
    }
}
