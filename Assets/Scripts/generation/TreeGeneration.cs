﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGeneration : MonoBehaviour {

	[SerializeField]
	private NoiseMapGeneration noiseMapGeneration;

	[SerializeField]
	[Tooltip("Octaves du bruit de Perlin")]
	private Wave[] waves;

	[SerializeField]
	private float levelScale;

	[SerializeField]
	private float[] neighborRadius;

	[SerializeField]
	private GameObject[] treePrefab;

	public void GenerateTrees(int levelDepth, int levelWidth, float distanceBetweenVertices, LevelData levelData, int tileDepthInVertices, int tileLengthInVertices) {
		// generate a tree noise map using Perlin Noise
		float[,] treeMap = this.noiseMapGeneration.GeneratePerlinNoiseMap (levelDepth, levelWidth, this.levelScale, 0, 0, this.waves);

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

				Vector3 realPos = new Vector3(
					(xIndex - tileLengthInVertices/2) * distanceBetweenVertices,
					meshVertices[vertexIndex].y + 5,
					(zIndex - tileLengthInVertices/2) * distanceBetweenVertices);

				// check if it is a water terrain. Trees cannot be placed over the water
				if (terrainType.name != "water") {
					float treeValue = treeMap [zIndex, xIndex];
                    float noiseSize = Random.Range(0.8f, 4f);
                    //int terrainTypeIndex = terrainType.index;

                    // compares the current tree noise value to the neighbor ones
                    int neighborZBegin = (int)Mathf.Max (0, zIndex - this.neighborRadius[terrainType.index]);
					int neighborZEnd = (int)Mathf.Min (levelDepth-1, zIndex + this.neighborRadius[terrainType.index]);
					int neighborXBegin = (int)Mathf.Max (0, xIndex - this.neighborRadius[terrainType.index]);
					int neighborXEnd = (int)Mathf.Min (levelWidth-1, xIndex + this.neighborRadius[terrainType.index]);
					float maxValue = 0f;
					for (int neighborZ = neighborZBegin; neighborZ <= neighborZEnd; neighborZ++) {
						for (int neighborX = neighborXBegin; neighborX <= neighborXEnd; neighborX++) {
							float neighborValue = treeMap [neighborZ, neighborX];
							// saves the maximum tree noise value in the radius
							if (neighborValue >= maxValue) {
								maxValue = neighborValue;
							}
						}
					}

					// if the current tree noise value is the maximum one, place a tree in this location
					if (treeValue == maxValue) {
						Vector3 treePosition = new Vector3(Random.Range(0f, 1f) * distanceBetweenVertices, 0, 
							Random.Range(0f, 1f) * distanceBetweenVertices);
						treePosition += realPos;
						
						int treeType = terrainType.index;
						if(terrainType.index == 1 || terrainType.index == 2){
							treeType = UnityEngine.Random.Range(1, this.treePrefab.Length);
						}
						
						GameObject tree = Instantiate (this.treePrefab[treeType], treePosition, Quaternion.identity) as GameObject;
                        tree.transform.localScale = new Vector3(noiseSize, noiseSize, noiseSize);
					}
				}
			}
		}
	}
}
