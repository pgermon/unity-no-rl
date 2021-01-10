using System;
using UnityEngine;

public class CubeWorld : MonoBehaviour
{
	[Header("Obstacles")]
	/** Terrain où générer des obstacles. */
	[SerializeField] public GameObject surface;
	/** Pilier */
	[SerializeField] public GameObject arbre1;
    [SerializeField] public GameObject arbre2;
    [SerializeField] public GameObject arbre3;
    [SerializeField] public GameObject arbre4;


    // Start is called before the first frame update
    void Start()
	{
		// GameObject surface_go = Instantiate(surface, new Vector3(0f, 0f, 0f), Quaternion.identity);
		Transform surface_t = surface.transform;
		Vector3 surface_dimensions = surface_t.lossyScale;

		Perlin2DMap perlin = new Perlin2DMap(0.1f, 0.4f, 4f);
		for (int x = (int) Mathf.Floor(-surface_dimensions.x * 5); x < (int) Math.Ceiling(surface_dimensions.x * 5); x++)
			for (int z = (int) Mathf.Floor(-surface_dimensions.z * 5); z < (int) Math.Ceiling(surface_dimensions.z * 5); z++) {
                float noise = UnityEngine.Random.Range(0f, 2f);
                float noiseSize = UnityEngine.Random.Range(0f, 0.8f);
                float height = (float) Math.Floor(perlin.at(x, z));
				if (height >= 1) {
                    int selector = UnityEngine.Random.Range(0, 4);

                    switch (selector)
                    {
                        case 0:
                            GameObject obstacle = GameObject.Instantiate(arbre1);
                            obstacle.transform.position = surface_t.position + new Vector3(x, 0, z);
                            obstacle.transform.localScale = new Vector3(1f, 1f- noiseSize, 1f);
                            obstacle.transform.position += new Vector3(0.5f + noise, 0, 0.5f + noise);
                            break;
                        case 1:
                            GameObject obstacle2 = GameObject.Instantiate(arbre2);
                            obstacle2.transform.position = surface_t.position + new Vector3(x, 0, z);
                            obstacle2.transform.localScale = new Vector3(1f, 1f+ noiseSize, 1f);
                            obstacle2.transform.position += new Vector3(0.5f + noise, 0, 0.5f + noise);
                            break;
                        case 2:
                            GameObject obstacle3 = GameObject.Instantiate(arbre3);
                            obstacle3.transform.position = surface_t.position + new Vector3(x, 0, z);
                            obstacle3.transform.localScale = new Vector3(1f, 1f- noiseSize, 1f);
                            obstacle3.transform.position += new Vector3(0.5f + noise, 0, 0.5f + noise);
                            break;
                        case 3:
                            GameObject obstacle4 = GameObject.Instantiate(arbre4);
                            obstacle4.transform.position = surface_t.position + new Vector3(x, 0, z);
                            obstacle4.transform.localScale = new Vector3(1f, 1f+ noiseSize, 1f  );
                            obstacle4.transform.position += new Vector3(0.5f + noise, 0, 0.5f + noise);
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                    
					
				}
			}
	}
}
