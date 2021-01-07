using UnityEngine;

public enum JeuEtat {
	DEPART,
	PARTIE,
	FIN
}

public class Jeu : MonoBehaviour {
	[Header("Joueur")]
	[SerializeField] private GameObject joueur_prefab;

	private JeuEtat etat;
	private GameObject joueur;
	/** @brief Duree de la partie. */
	private float duree;

	public void Start() {
		this.etat = JeuEtat.DEPART;
	}

	public void Update() {
		if (this.etat == JeuEtat.PARTIE) {
			this.duree += Time.deltaTime;
		}
	}

	public Transform joueurPos() {
		return joueur.transform;
	}
}