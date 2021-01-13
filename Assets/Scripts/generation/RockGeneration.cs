using UnityEngine;

public class RockGeneration : GenerationTile {

	[SerializeField]
	private GameObject[] rockPrefabs;

	[SerializeField]
	private float normalSpawn = 0.9f;

	[SerializeField]
	private float mountainSpawn = 0.6f;

	protected override void generationSubTile(Vector3 realPos, float distanceBetweenVertices, TerrainType terrainType) {

		// rocks cannot only be placed on water
		if (terrainType.name != "water") {

			float rndThreshold = this.normalSpawn;
			if(terrainType.name == "mountain"){
				rndThreshold = this.mountainSpawn;
			}

			if(UnityEngine.Random.Range(0.0f, 1.0f) > rndThreshold) {
                float noiseSize = Random.Range(0.2f, 2f);
                int noiseRotation = Random.Range(0, 360);
				int rockType = UnityEngine.Random.Range(0, this.rockPrefabs.Length);

				Vector3 rockPosition = new Vector3(Random.Range(0f, 1f) * distanceBetweenVertices, 0, 
					Random.Range(0f, 1f) * distanceBetweenVertices);
				rockPosition += realPos;
				GameObject rock = Instantiate (this.rockPrefabs[rockType], rockPosition, Quaternion.Euler(0, noiseRotation, 0)) as GameObject;
				rock.transform.localScale = new Vector3(noiseSize, noiseSize, noiseSize);
			}
		}
	}
}
