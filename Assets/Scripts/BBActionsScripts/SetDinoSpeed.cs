using UnityEngine;
using UnityEngine.AI;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;

/** @brief Modifie la vitesse du dinosaure.
 *
 * Permet de faire accélérer ou ralentir le dinosaure en fonction des situations.
 */
[Action("Behaviors/DinoActions/SetDinoSpeed")]
[Help("Set the speed of the dino's NavMeshAgent component to the specified value")]
public class SetDinoSpeed : GOAction 
{
    [InParam("speed")]
    public float speed;

    public override TaskStatus OnUpdate(){

        gameObject.GetComponent<NavMeshAgent>().speed = speed;

        return TaskStatus.COMPLETED;
    }
}