﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaursBBSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject player, raptor, para, stego, tric;

    [SerializeField]
    private int MAX_RAPTORS, MAX_PARAS, MAX_STEGOS, MAX_TRICS = 1;

    public void SpawnDinos(int levelLength, int levelWidth, float distanceBetweenVertices, LevelData levelData, int tileLengthInVertices, int tileWidthInVertices)
    {
       GameObject go_player = Instantiate(player, 
                GetRandomPosition(levelLength, levelWidth, distanceBetweenVertices, levelData, tileLengthInVertices, tileWidthInVertices), 
                Quaternion.identity);

       for(int i = 0; i < MAX_RAPTORS; i++){
           GameObject go_raptor = Instantiate(raptor, 
                    GetRandomPosition(levelLength, levelWidth, distanceBetweenVertices, levelData, tileLengthInVertices, tileWidthInVertices), 
                    Quaternion.identity);
       }

       for(int i = 0; i < MAX_PARAS; i++){
           GameObject go_para = Instantiate(para, 
                    GetRandomPosition(levelLength, levelWidth, distanceBetweenVertices, levelData, tileLengthInVertices, tileWidthInVertices), 
                    Quaternion.identity);
       }
        for (int i = 0; i < MAX_STEGOS; i++)
        {
            GameObject go_stego = Instantiate(stego,
                     GetRandomPosition(levelLength, levelWidth, distanceBetweenVertices, levelData, tileLengthInVertices, tileWidthInVertices),
                     Quaternion.identity);
        }
        for (int i = 0; i < MAX_TRICS; i++)
        {
            GameObject go_tric = Instantiate(tric,
                     GetRandomPosition(levelLength, levelWidth, distanceBetweenVertices, levelData, tileLengthInVertices, tileWidthInVertices),
                     Quaternion.identity);
        }

    }

    public Vector3 GetRandomPosition(int levelLength, int levelWidth, float distanceBetweenVertices, LevelData levelData, int tileLengthInVertices, int tileWidthInVertices)
    {
        
        int zIndex = Random.Range(0, levelLength);
        int xIndex = Random.Range(0, levelWidth);

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

        Vector3 randomPosition = new Vector3(Random.Range(-1f, 1f) * distanceBetweenVertices, 
                                    0f, 
                                    Random.Range(-1f, 1f) * distanceBetweenVertices);

        return realPos + randomPosition;
    }

}