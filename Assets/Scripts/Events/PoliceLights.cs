using UnityEngine;

public class PoliceLights : MonoBehaviour
{
    [SerializeField] Light redLight;
    [SerializeField] Light blueLight;
    [SerializeField] float flashInterval = 0.5f; // Time between flashes

    private bool isRedOn = true;

    void Start()
    {
        // Ensure both lights start in a known state
        redLight.enabled = true;
        blueLight.enabled = false;

        // Start flashing
        InvokeRepeating("ToggleLights", flashInterval, flashInterval);
    }

    void ToggleLights()
    {
        if (isRedOn)
        {
            redLight.enabled = false;
            blueLight.enabled = true;
        }
        else
        {
            redLight.enabled = true;
            blueLight.enabled = false;
        }

        isRedOn = !isRedOn;
    }
}
