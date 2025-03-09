using UnityEngine;
using System.Collections;

public class TorchFlicker : MonoBehaviour
{
    public Light torchLight; // Asigna la luz de la antorcha en el Inspector
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.15f; // Velocidad de parpadeo más suave
    public float flickerSmoothing = 0.2f; // Suavizado de cambios de intensidad

    private float targetIntensity;

    private void Start()
    {
        if (torchLight == null)
        {
            torchLight = GetComponent<Light>();
        }
        StartCoroutine(FlickerEffect());
    }

    private IEnumerator FlickerEffect()
    {
        while (true)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            float elapsedTime = 0f;
            float startIntensity = torchLight.intensity;

            while (elapsedTime < flickerSmoothing)
            {
                torchLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / flickerSmoothing);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            torchLight.intensity = targetIntensity;
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}