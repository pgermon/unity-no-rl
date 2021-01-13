using UnityEngine;

public abstract class GenerationTile : MonoBehaviour {

	///<summary>Génère des objets sur l'entièreté du terrain</summary>
	///<param name="levelLength">Longueur totale du terrain</param>
	///<param name="levelWidth">Largeur totale du terrain</param>
	///<param name="distanceBetweenVertices">distance entre deux sommets du mesh</param>
	///<param name="levelData">données du terrain</param>
	///<param name="tileLengthInVertices">longueur d'un secteur en nombre de sommets de mesh</param>
	///<param name="tileWidthInVertices">longueur d'un secteur en nombre de sommets de mesh</param>
	public void PopulateLevel(int levelLength, int levelWidth, float distanceBetweenVertices, LevelData levelData, int tileLengthInVertices, int tileWidthInVertices) {
		if (this.ignore()) return;
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

				Vector3 realPos = new Vector3(
					(xIndex - tileWidthInVertices/2) * distanceBetweenVertices,
					meshVertices[vertexIndex].y,
					(zIndex - tileLengthInVertices/2) * distanceBetweenVertices);

				this.generationSubTile(realPos, distanceBetweenVertices, terrainType);
			}
		}
	}

	public virtual bool ignore() {
		return false;
	}

	protected abstract void generationSubTile(Vector3 realPos, float distanceBetweenVertices, TerrainType terrainType);
}