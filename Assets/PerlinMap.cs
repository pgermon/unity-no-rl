using UnityEngine;

/* Carte en 2D d'un bruit de Perlin.
 * Le bruit généré est compris dans [-amplitude, amplitude].
*/
class Perlin2DMap {

	protected float freqX, freqY, amplitude;

	public Perlin2DMap(float freqX, float freqY, float amplitude) {
		this.freqX = freqX;
		this.freqY = freqY;
		this.amplitude = amplitude;
	}

	public float at(int x, int y) {
		return (Mathf.PerlinNoise((x + 20) * freqX, (y + 13) * freqY) - 0.5f) * 2 * amplitude;
	}
}