using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
	[SerializeField] Image black;
	[SerializeField] AudioSource rainSource;
    void Start()
    {
		black.DOFade(0, 2.5f);
		rainSource.DOFade(0.1f, 2.5f);
    }

}
