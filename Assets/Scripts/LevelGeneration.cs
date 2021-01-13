﻿using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class LevelGeneration : MonoBehaviour {

	[SerializeField]
	[Tooltip("Nombre de secteurs sur l'axe")]
	private int levelWidthInTiles = 2, levelLengthInTiles = 2;

	[SerializeField]
	[Tooltip("Objet/Prefab déterminant les dimensions du terrain")]
	private GameObject tilePrefab;

	[SerializeField]
	private float centerVertexZ = 0, maxDistanceZ = 0;

	/*[SerializeField]
	private RiverGeneration riverGeneration;*/

	void Start() {
		if (this.levelLengthInTiles <= 0 || this.levelLengthInTiles <= 0) {
			Debug.LogError("Vous essayer de créer un terrain sans surface.");
			return;
		}
		GenerateMap ();
	}

	void GenerateMap() {
		// get the tile dimensions from the tile Prefab
		Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer> ().bounds.size;
		
		int tileWidth = (int)tileSize.x;
		int tileLength = (int)tileSize.z;

		// calculate the number of vertices of the tile in each axis using its mesh
		Vector3[] tileMeshVertices = tilePrefab.GetComponent<MeshFilter> ().sharedMesh.vertices;
		int tileDepthInVertices = (int)Mathf.Sqrt (tileMeshVertices.Length);
		int tileWidthInVertices = tileDepthInVertices;

		float distanceBetweenVertices = (float)tileLength / (float)tileDepthInVertices;

		// build an empty LevelData object, to be filled with the tiles to be generated
		LevelData levelData = new LevelData (tileDepthInVertices, tileWidthInVertices, this.levelLengthInTiles, this.levelWidthInTiles);

		// for each Tile, instantiate a Tile in the correct position
		for (int xTileIndex = 0; xTileIndex < levelWidthInTiles; xTileIndex++) {
			for (int zTileIndex = 0; zTileIndex < levelLengthInTiles; zTileIndex++) {
				// calculate the tile position based on the X and Z indices
				Vector3 tilePosition = new Vector3(this.gameObject.transform.position.x + xTileIndex * tileWidth, 
												   this.gameObject.transform.position.y, 
												   this.gameObject.transform.position.z + zTileIndex * tileLength);
				// instantiate a new Tile
				GameObject tile = Instantiate (tilePrefab, tilePosition, Quaternion.identity) as GameObject;
				// generate the Tile texture and save it in the levelData
				TileData tileData = tile.GetComponent<TileGeneration> ().GenerateTile (centerVertexZ, maxDistanceZ);
				levelData.AddTileData (tileData, zTileIndex, xTileIndex);
			}
		}

		// generate trees for the level
		GetComponent<TreeGeneration>().GenerateTrees (this.levelLengthInTiles * tileDepthInVertices, this.levelWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData, tileDepthInVertices, tileWidthInVertices);

		//generates grass for the level
		GetComponent<GrassGeneration>().PopulateLevel(this.levelLengthInTiles * tileDepthInVertices, this.levelWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData, tileDepthInVertices, tileWidthInVertices);
		
		//generates rocks for the level
		GetComponent<RockGeneration>().PopulateLevel(this.levelLengthInTiles * tileDepthInVertices, this.levelWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData, tileDepthInVertices, tileWidthInVertices);
		
		// generate rivers for the level
		//riverGeneration.GenerateRivers(this.levelDepthInTiles * tileDepthInVertices, this.levelWidthInTiles * tileWidthInVertices, levelData);
	}
}

// class to store all the merged tiles data
public class LevelData {
	private int tileDepthInVertices, tileWidthInVertices;

	public TileData[,] tilesData;

	public LevelData(int tileDepthInVertices, int tileWidthInVertices, int levelDepthInTiles, int levelWidthInTiles) {
		// build the tilesData matrix based on the level depth and width
		tilesData = new TileData[tileDepthInVertices * levelDepthInTiles, tileWidthInVertices * levelWidthInTiles];

		this.tileDepthInVertices = tileDepthInVertices;
		this.tileWidthInVertices = tileWidthInVertices;
	}

	public void AddTileData(TileData tileData, int tileZIndex, int tileXIndex) {
		// save the TileData in the corresponding coordinate
		tilesData [tileZIndex, tileXIndex] = tileData;
	}

	public TileCoordinate ConvertToTileCoordinate(int zIndex, int xIndex) {
		// the tile index is calculated by dividing the index by the number of tiles in that axis
		int tileZIndex = (int)Mathf.Floor ((float)zIndex / (float)this.tileDepthInVertices);
		int tileXIndex = (int)Mathf.Floor ((float)xIndex / (float)this.tileWidthInVertices);
		// the coordinate index is calculated by getting the remainder of the division above
		// we also need to translate the origin to the bottom left corner
		int coordinateZIndex = this.tileDepthInVertices - (zIndex % this.tileDepthInVertices) - 1;
		int coordinateXIndex = this.tileWidthInVertices - (xIndex % this.tileDepthInVertices) - 1;

		TileCoordinate tileCoordinate = new TileCoordinate (tileZIndex, tileXIndex, coordinateZIndex, coordinateXIndex);
		return tileCoordinate;
	}
}

// class to represent a coordinate in the Tile Coordinate System
public class TileCoordinate {
	public int tileZIndex;
	public int tileXIndex;
	public int coordinateZIndex;
	public int coordinateXIndex;

	public TileCoordinate(int tileZIndex, int tileXIndex, int coordinateZIndex, int coordinateXIndex) {
		this.tileZIndex = tileZIndex;
		this.tileXIndex = tileXIndex;
		this.coordinateZIndex = coordinateZIndex;
		this.coordinateXIndex = coordinateXIndex;
	}
}