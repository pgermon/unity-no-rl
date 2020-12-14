using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWorld : MonoBehaviour
{
	/** Terrain où générer des obstacles. */
	public GameObject surface;
	/** Pilier */
	public GameObject cube;

	// Start is called before the first frame update
	void Start()
	{
		Transform surface_t = surface.transform;
		Vector3 surface_dimensions = surface_t.lossyScale;

		Perlin2DMap perlin = new Perlin2DMap(0.2f, 0.2f, 6f);

		for (int x = (int) Mathf.Floor(-surface.transform.localScale.x * 5); x < (int) Math.Ceiling(surface.transform.localScale.x * 5); x++)
			for (int z = (int) Mathf.Floor(-surface.transform.localScale.z * 5); z < (int) Math.Ceiling(surface.transform.localScale.z * 5); z++) {
				float height = (float) Math.Floor(perlin.at(x, z) - 1 + Mathf.Sqrt((x * x + z * z)) / 8);
				if (height >= 1) {
					GameObject obstacle = GameObject.Instantiate(cube);
					obstacle.transform.position = surface.transform.position + new Vector3(x, 0, z);
					obstacle.transform.localScale = new Vector3(1f, height, 1f);
					obstacle.transform.position += new Vector3(0.5f, height / 2, 0.5f);
				}
			}
	}
}
