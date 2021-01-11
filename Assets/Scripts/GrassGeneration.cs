using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGeneration : MonoBehaviour {

	[SerializeField]
	private NoiseMapGeneration noiseMapGeneration;

	[SerializeField]
	private Wave[] waves;

	[SerializeField]
	private float levelScale;

	[SerializeField]
	private float neighborRadius;

	[SerializeField]
	private GameObject grassPrefab;

	public void GenerateGrass(int levelDepth, int levelWidth, float distanceBetweenVertices, LevelData levelData, int tileDepthInVertices, int tileWidthInVertices) {
		// generate a grass noise map using Perlin Noise
		//float[,] grassMap = this.noiseMapGeneration.GeneratePerlinNoiseMap (levelDepth, levelWidth, this.levelScale, 0, 0, this.waves);

		float levelSizeX = levelWidth * distanceBetweenVertices;
		float levelSizeZ = levelDepth * distanceBetweenVertices;

		for (int zIndex = 0; zIndex < levelDepth; zIndex++) {
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

				// get the biome of this coordinate
				//Biome biome = tileData.chosenBiomes[tileCoordinate.coordinateZIndex, tileCoordinate.coordinateXIndex];

				// grass can only be placed on lowland and mountain
				if (terrainType.name == "lowland" || terrainType.name == "mountain") {
					/*float grassValue = grassMap [zIndex, xIndex];

					// compares the current grass noise value to the neighbor ones
					int neighborZBegin = (int)Mathf.Max (0, zIndex - this.neighborRadius);
					int neighborZEnd = (int)Mathf.Min (levelDepth-1, zIndex + this.neighborRadius);
					int neighborXBegin = (int)Mathf.Max (0, xIndex - this.neighborRadius);
					int neighborXEnd = (int)Mathf.Min (levelWidth-1, xIndex + this.neighborRadius);
					float maxValue = 0f;
					for (int neighborZ = neighborZBegin; neighborZ <= neighborZEnd; neighborZ++) {
						for (int neighborX = neighborXBegin; neighborX <= neighborXEnd; neighborX++) {
							float neighborValue = grassMap [neighborZ, neighborX];
							// saves the maximum grass noise value in the radius
							if (neighborValue >= maxValue) {
								maxValue = neighborValue;
							}
						}
					}


					// if the current grass noise value is the maximum one, place a grass in this location
					if (grassValue == maxValue) {
						Vector3 grassPosition = new Vector3((xIndex - tileWidthInVertices/2)*distanceBetweenVertices, meshVertices[vertexIndex].y + 1, (zIndex - tileDepthInVertices/2)*distanceBetweenVertices);
						
						GameObject grass = Instantiate (this.grassPrefab, grassPosition, Quaternion.identity) as GameObject;
                        grass.transform.localScale = new Vector3(2f, 2f, 2f);
					}*/

                    if(UnityEngine.Random.Range(0.0f, 1.0f) > 0.3){
                        for(int i = 0; i < 4; i++){
                            float rndOffsetX = UnityEngine.Random.Range(-0.5f, 0.5f) * 4;
                            float rndOffsetZ = UnityEngine.Random.Range(-0.5f, 0.5f) * 4;
                            Vector3 grassPosition = new Vector3((xIndex - tileWidthInVertices/2)*distanceBetweenVertices + rndOffsetX, 
                                                                 meshVertices[vertexIndex].y * 2, 
                                                                 (zIndex - tileDepthInVertices/2)*distanceBetweenVertices + rndOffsetZ);
                            GameObject grass = Instantiate (this.grassPrefab, grassPosition, Quaternion.identity) as GameObject;
                            grass.transform.localScale = new Vector3(2f, 2f, 2f); 
                        }
                    }
				}
			}
		}
	}
}
