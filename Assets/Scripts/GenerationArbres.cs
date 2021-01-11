using UnityEngine;

/// Génération des arbres
[RequireComponent(typeof(Transform))]
public class GenerationArbres : MonoBehaviour
{
    [SerializeField] private float frequence = 0.2f;

	[Header("Arbres")]
	/** Pilier */
	[SerializeField] public GameObject arbre1;
    [SerializeField] public GameObject arbre2;
    [SerializeField] public GameObject arbre3;
    [SerializeField] public GameObject arbre4;

    private 


    // Start is called before the first frame update
    void Start()
	{
		Transform surface_t = GetComponent<Transform>().transform;
		Vector3 surface_dimensions = GetComponent<Transform>().lossyScale;

		for (int x = (int) Mathf.Ceil(-surface_dimensions.x * 5); x < (int) Mathf.Floor(surface_dimensions.x * 5); x += 3)
			for (int z = (int) Mathf.Ceil(-surface_dimensions.z * 5); z < (int) Mathf.Floor(surface_dimensions.z * 5); z += 3) {
				if (Random.Range(0f, 1f) < this.frequence) {
                    int selector = Random.Range(0, 4);
                    GameObject modele = null;

                    Vector3 position = new Vector3(x + Random.Range(-0.1f, 0.1f), 40, z + Random.Range(-0.1f, 0.1f));
            
                    switch (selector)
                    {
                        case 0:
                            modele = arbre1;
                            break;
                        case 1:
                            modele = arbre2;
                            break;
                        case 2:
                            modele = arbre3;
                            break;
                        default:
                            modele = arbre4;
                            break;
                    }
                    
                    GameObject arbre = GameObject.Instantiate(modele, position + surface_t.position, Quaternion.identity);
                    arbre.transform.localScale = new Vector3(1f, 1f - UnityEngine.Random.Range(-0.2f, 0.2f), 1f);
				}
			}
	}
}
