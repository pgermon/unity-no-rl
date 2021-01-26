using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;
using UnityEngine.AI;

/** @brief Cherche un lieu où fuir.
 *
 * Quand un prédateur est trop proche, le dinosaure va chercher un lieu qui lui permet de s'éloigner.
 * La direction de fuite est à l'opposé du prédateur le plus proche.
 * La distance de fuite à parcourir peut être de seulement quelques mêtres mais renouvelée tant qu'il y a danger.
 */
[Action("Behaviors/DinoActions/FindEscape")]
[Help("Find somewhere to escape the predator")]
public class DinoEscape : GOAction {

	[InParam("predator")]
    public GameObject predator;

	// TODO: à supprimer ou remplacer par longueur de fuite
	[InParam("security")]
	[Help("Minimum distance to preserve")]
	public float security;

    [OutParam("runToPosition")]
    public Vector3 position;

	public override TaskStatus OnUpdate(){
		if (this.predator != null) {

			//Debug.Log(gameObject.name + " running from " + predator.gameObject.name);

			gameObject.GetComponent<DinosaurBB>().setIsWandering(false);

			Vector3 runDirection = gameObject.transform.position - predator.gameObject.transform.position;
			this.position = gameObject.transform.position + runDirection;

			//Vector3 runDirection = (gameObject.transform.position - predator.gameObject.transform.position).normalized;
			//this.position = gameObject.transform.position + 1.5f * security * runDirection;
			// Debug.Log(gameObject.name + ":Fuite vers " + this.position);
			//TODO: vérifier que le point est accessible sur le terrain
			return TaskStatus.COMPLETED;
		} else {
			Debug.LogError("Aucun prédateur à fuir");
			return TaskStatus.FAILED;
		}
	}
}