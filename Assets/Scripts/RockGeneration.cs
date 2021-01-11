using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGeneration : MonoBehaviour {

	[SerializeField]
	private GameObject[] rockPrefabs;

	public void GenerateRocks(int levelDepth, int levelWidth, float distanceBetweenVertices, LevelData levelData, int tileDepthInVertices, int tileWidthInVertices) {

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

				// rocks cannot only be placed on water
				if (terrainType.name != "water") {

                    if(UnityEngine.Random.Range(0.0f, 1.0f) > 0.9){
                        
                        int rockType = UnityEngine.Random.Range(0, 2);
                        float rndOffsetX = UnityEngine.Random.Range(-0.5f, 0.5f) * 4;
                        float rndOffsetZ = UnityEngine.Random.Range(-0.5f, 0.5f) * 4;
                        Vector3 rockPosition = new Vector3((xIndex - tileWidthInVertices/2)*distanceBetweenVertices + rndOffsetX, 
                                                                meshVertices[vertexIndex].y - 1, 
                                                                (zIndex - tileDepthInVertices/2)*distanceBetweenVertices + rndOffsetZ);
                        GameObject rock = Instantiate (this.rockPrefabs[rockType], rockPosition, Quaternion.identity) as GameObject;
                        //rock.transform.localScale = new Vector3(2f, 2f, 2f); 
                    }
				}
			}
		}
	}
}
