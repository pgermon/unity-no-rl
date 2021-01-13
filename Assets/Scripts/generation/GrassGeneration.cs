using UnityEngine;

public class GrassGeneration : MonoBehaviour {

	[SerializeField]
	private GameObject[] grassPrefab;
	
	[Tooltip("Densité en herbe (u/m2)")]
	public float densite = 0.02f;

	///<summary>Génère de l'herbe sur l'entièreté du terrain</summary>
	///<param name="levelLength">Longueur totale du terrain</param>
	///<param name="levelWidth">Largeur totale du terrain</param>
	///<param name="distanceBetweenVertices">distance entre deux sommets du mesh</param>
	///<param name="levelData">données du terrain</param>
	///<param name="tileLengthInVertices">longueur d'un secteur en nombre de sommets de mesh</param>
	///<param name="tileWidthInVertices">longueur d'un secteur en nombre de sommets de mesh</param>
	public void GenerateGrass(int levelLength, int levelWidth, float distanceBetweenVertices, LevelData levelData, int tileLengthInVertices, int tileWidthInVertices) {
		if (densite <= 0) return;
		float aire = distanceBetweenVertices * distanceBetweenVertices;

		float levelSizeX = levelWidth * distanceBetweenVertices;
		float levelSizeZ = levelLength * distanceBetweenVertices;

		for (int zIndex = 0; zIndex < levelLength; zIndex++) {
			for (int xIndex = 0; xIndex < levelWidth; xIndex++) {
                // convert from Level Coordinate System to Tile Coordinate System and retrieve the corresponding TileData
                TileCoordinate tileCoordinate = levelData.ConvertToTileCoordinate (zIndex, xIndex);
				TileData tileData = levelData.tilesData [tileCoordinate.tileZIndex, tileCoordinate.tileXIndex];
				int tileWidth = tileData.heightMap.GetLength (1);

				// calculate the mesh vertex index
				Vector3[] meshVertices = tileData.mesh.vertices;
				int vertexIndex = tileCoordinate.coordinateZIndex * tileWidth + tileCoordinate.coordinateXIndex;

				// get the terrain type of this coordinate
				TerrainType terrainType = tileData.chosenHeightTerrainTypes [tileCoordinate.coordinateZIndex, tileCoordinate.coordinateXIndex];

				// grass can only be placed on lowland and mountain
				if (terrainType.name == "lowland") {
					
					Vector3 realPos = new Vector3(
						(xIndex - tileWidthInVertices/2) * distanceBetweenVertices,
						meshVertices[vertexIndex].y + 5,
						(zIndex - tileLengthInVertices/2) * distanceBetweenVertices);
					float generation = Mathf.Floor(aire * this.densite);
					generation += (Random.Range(0f, 1f) < generation % 1) ? 1 : 0;

					for(int i = 0; i < generation; i++){
                		int selector = Random.Range(0, this.grassPrefab.Length);
                		float noiseSize = Random.Range(0.8f, 2f);
						Vector3 grassPosition = new Vector3(Random.Range(-0.5f, 0.5f) * distanceBetweenVertices, 0, 
							Random.Range(-0.5f, 0.5f) * distanceBetweenVertices);
						grassPosition += realPos;

						GameObject grass = GameObject.Instantiate (this.grassPrefab[selector], grassPosition, Quaternion.identity);
						grass.transform.localScale = new Vector3(noiseSize, noiseSize, noiseSize); 
					}
				}
			}
		}
	}
}
