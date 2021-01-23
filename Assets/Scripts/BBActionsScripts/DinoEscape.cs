using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;
using UnityEngine.AI;

[Action("Behaviors/DinoActions/FindEscape")]
[Help("Find somewhere to escape the predator")]
public class DinoEscape : GOAction {
	[InParam("predator")]
    public GameObject predator;
	[InParam("security")]
	[Help("Minimum distance to preserve")]
	public float security = 30;

    [OutParam("runToPosition")]
    public Vector3 position;

	public override TaskStatus OnUpdate(){
		if (this.predator != null) {
			Vector3 runDirection = (gameObject.transform.position - predator.gameObject.transform.position).normalized;
			this.position = gameObject.transform.position + 1.5f * security * runDirection;
			// Debug.Log(gameObject.name + ":Fuite vers " + this.position);
			//TODO: vérifier que le point est accessible sur le terrain
			return TaskStatus.COMPLETED;
		} else {
			Debug.LogError("Aucun prédateur à fuir");
			return TaskStatus.FAILED;
		}
	}
}