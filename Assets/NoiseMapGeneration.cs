using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapGeneration : MonoBehaviour
{
    [SerializeField] private float offsetX = 0;
    [SerializeField] private float offsetY = 0;

    public float[,] GenerateNoiseMap(int mapDepth, int mapWidth, float freq, float scale)
    {
        // create an empty noise map with the mapDepth and mapWidth coordinates
        float[,] noiseMap = new float[mapDepth, mapWidth];

        for (int zIndex = 0; zIndex < mapDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < mapWidth; xIndex++)
            {
                // calculate sample indices based on the coordinates and the scale
                float sampleX = xIndex / freq;
                float sampleZ = zIndex / freq;

                // generate noise value using PerlinNoise
                float noise = Mathf.PerlinNoise(sampleX + offsetX, sampleZ + offsetY);

                noiseMap[zIndex, xIndex] = noise * scale;
            }
        }

        return noiseMap;
    }
}