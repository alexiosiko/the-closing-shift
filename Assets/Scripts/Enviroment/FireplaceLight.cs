using UnityEngine;

public class FireplaceLight : MonoBehaviour
{
    // Reference to the light component
    [SerializeField] Light fireplaceLight;

    // Flicker intensity range
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;

    // Flicker speed
    public float flickerSpeed = 0.1f;

    // Internal timer
    private float timer;
    void Update()
    {
        if (fireplaceLight != null)
        {
            // Smoothly interpolate light intensity over time
            timer += Time.deltaTime * flickerSpeed;
            float noise = Mathf.PerlinNoise(timer, 0.0f);
            fireplaceLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        }
    }
}
