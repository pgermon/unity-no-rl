﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGeneration : MonoBehaviour {

	[SerializeField]
	private GameObject[] grassPrefab;

	public void GenerateGrass(int levelDepth, int levelWidth, float distanceBetweenVertices, LevelData levelData, int tileDepthInVertices, int tileWidthInVertices) {

		float levelSizeX = levelWidth * distanceBetweenVertices;
		float levelSizeZ = levelDepth * distanceBetweenVertices;

		for (int zIndex = 0; zIndex < levelDepth; zIndex++) {
			for (int xIndex = 0; xIndex < levelWidth; xIndex++) {
                int selector = Random.Range(0, 3);
                float noiseSize = Random.Range(0.8f, 4f);
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

                    if(UnityEngine.Random.Range(0.0f, 1.0f) > 0.2){
                        for(int i = 0; i < 10; i++){
                            float rndOffsetX = UnityEngine.Random.Range(-1.0f, 1.0f) * 4;
                            float rndOffsetZ = UnityEngine.Random.Range(-1.0f, 1.0f) * 4;
                            Vector3 grassPosition = new Vector3((xIndex - tileWidthInVertices/2)*distanceBetweenVertices + rndOffsetX, 
                                                                 meshVertices[vertexIndex].y-0.8f, 
                                                                 (zIndex - tileDepthInVertices/2)*distanceBetweenVertices + rndOffsetZ);
                            GameObject grass = Instantiate (this.grassPrefab[selector], grassPosition, Quaternion.identity) as GameObject;
                            grass.transform.localScale = new Vector3(noiseSize, noiseSize, noiseSize); 
                        }
                    }
				}
			}
		}
	}
}
