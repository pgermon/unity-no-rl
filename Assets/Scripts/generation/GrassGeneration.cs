using UnityEngine;

public class GrassGeneration : GenerationTile {

	[SerializeField]
	private GameObject[] grassPrefab;
	
	[Tooltip("Densité en herbe (u/m2)")]
	public float densite = 0.02f;

	public override bool ignore() {
		return this.densite <= 0;
	}

	protected override void generationSubTile(Vector3 realPos, float distanceBetweenVertices, TerrainType terrainType) {

		// grass can only be placed on lowland and mountain
		if (terrainType.name == "lowland") {
			float generation = Mathf.Floor(distanceBetweenVertices * distanceBetweenVertices * this.densite);
			generation += (Random.Range(0f, 1f) < generation % 1) ? 1 : 0;

			for(int i = 0; i < generation; i++){
				int selector = Random.Range(0, this.grassPrefab.Length);
				float noiseSize = Random.Range(0.8f, 2f);
				Vector3 grassPosition = new Vector3(Random.Range(0f, 1f) * distanceBetweenVertices, 0, 
					Random.Range(0f, 1f) * distanceBetweenVertices);
				grassPosition += realPos;

				GameObject grass = GameObject.Instantiate (this.grassPrefab[selector], grassPosition, Quaternion.identity);
				grass.transform.localScale = new Vector3(noiseSize, noiseSize, noiseSize); 
			}
		}
	}
}
