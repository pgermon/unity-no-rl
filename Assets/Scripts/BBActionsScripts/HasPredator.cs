using UnityEngine;
using Pada1.BBCore;           // Code attributes
using BBUnity.Conditions;

/** @brief Charge le prédateur actuel dans l'arbre.
 *
 * Si un prédateur a été enregistrée par \link DinosaurBB::selectCurrentPredator() \endlink, récupère l'objet
 * pour l'arbre de comportement et renvoie true.
 */
[Condition("Behaviors/DinoConditions/HasPredator")]
[Help("Search a predator and return true if found")]
public class HasPredator : GOCondition 
{
    [OutParam("predator")]
    [Help("The predator found")]
    public GameObject current_predator;
    
    public override bool Check(){
        current_predator = gameObject.GetComponent<DinosaurBB>().getCurrentPredator();
        //if (current_predator != null)
        	//Debug.Log(gameObject.name.Split('(')[0] + ": predator is detected " + current_predator.name.Split('(')[0]);
        return current_predator != null;
    }
    
}