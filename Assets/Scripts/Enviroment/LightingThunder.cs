using System.Collections;
using UnityEngine;

public class LightningThunder : MonoBehaviour
{
    [SerializeField] Light lightningLight;      // The light source for lightning flash
    [SerializeField] AudioSource source;  // The audio source for thunder
	[SerializeField] AudioClip[] thunderSounds;
    [SerializeField] float minFlashDuration = 0.01f;
    [SerializeField] float maxFlashDuration = 0.1f;
    [SerializeField] float minFlashIntensity = 0.5f;
    [SerializeField] float maxFlashIntensity = 10f;
    [SerializeField] float maxVolume = 0.2f;
    [SerializeField] float minVolume = 0.5f;
    public void Start() => StartCoroutine(LightningFlash());

    IEnumerator LightningFlash()
    {
		yield return new WaitForSeconds(Random.Range(30f, 45f));
        lightningLight.enabled = true;
        lightningLight.intensity = Random.Range(minFlashIntensity, maxFlashIntensity); 
        yield return new WaitForSeconds(Random.Range(minFlashDuration, maxFlashDuration));
        lightningLight.enabled = false;

        float thunderDelay = Random.Range(10, 20) / 10f; 
        yield return new WaitForSeconds(thunderDelay);
		if (source.isPlaying) {
			while (source.volume > 0) {
				source.volume -= 0.01f;
				yield return new WaitForSeconds(0.01f);
			}
		}
		source.clip = thunderSounds[Random.Range(0, thunderSounds.Length - 1)];
		source.volume = Random.Range(minVolume, maxVolume);
		source.Play();
		StartCoroutine(LightningFlash());

    }
}
