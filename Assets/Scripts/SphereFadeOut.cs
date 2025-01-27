using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFadeOut : MonoBehaviour
{
    public GameObject sphere;  // Reference to the sphere GameObject that will be fading
    public float startDelay = 2f;  // Delay in seconds before starting the fade out
    public float fadeDuration = 1f;  // Duration of the fade out

    private Material sphereMaterial;
    private Color originalColor;

    void Start()
    {
        if (sphere == null)
        {
            Debug.LogError("Sphere GameObject is not assigned.");
            return;
        }

        sphereMaterial = sphere.GetComponent<Renderer>().material;
        originalColor = sphereMaterial.color;
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(startDelay);

        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            sphereMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        sphere.SetActive(false);
    }

    public void ActivateAndUnfadeSphere()
    {
        if (!sphere.activeInHierarchy)
        {
            sphere.SetActive(true);
            StartCoroutine(UnfadeRoutine());
        }
    }

    IEnumerator UnfadeRoutine()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            sphereMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
    }
}
