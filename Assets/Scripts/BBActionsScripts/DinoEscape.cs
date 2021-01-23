using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;

[Action("Behaviors/DinoActions/FindEscape")]
[Help("Find somewhere to escape the predator")]
public class DinoEscape : GOAction {
	[InParam("predator")]
    public GameObject predator;

    [OutParam("runToPosition")]
    public Vector3 position;

	public override TaskStatus OnUpdate(){
		Vector3 runDirection = (gameObject.transform.position - predator.gameObject.transform.position).normalized;
		this.position = gameObject.transform.position + 10 * runDirection;
		//TODO: vérifier pas de sortie du terrain
		return TaskStatus.COMPLETED;
	}
}