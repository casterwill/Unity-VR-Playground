using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalDestroyer : MonoBehaviour {

	public float lifeTime = 5.0f;

	[SerializeField] GameObject holeDecals;
    [SerializeField] float fadeOutDuration = 2f;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(lifeTime);

        Material holeMaterial = holeDecals.GetComponent<MeshRenderer>().material;

        float startAlpha = holeMaterial.color.a;
        for (float t = 0f; t < fadeOutDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeOutDuration;
            Color newColor = holeMaterial.color;
            newColor.a = Mathf.Lerp(startAlpha, 0f, normalizedTime);
            holeMaterial.color = newColor;
            yield return null;
        }
        // Ensure it ends at exactly 0
        holeMaterial.color = new Color(holeMaterial.color.r, holeMaterial.color.g, holeMaterial.color.b, 0f);
        Destroy(gameObject);
	}
}
