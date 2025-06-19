using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class ChangeTextColorOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText; // Reference to the button's text
    public Color hoverColor = Color.red; // Color to change on hover
    public Color defaultColor = Color.white; // Default color

    private void Start()
    {
        if (buttonText == null)
            buttonText = GetComponentInChildren<TMP_Text>();
        buttonText.color = defaultColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		transform.DOScale(1f, 0.1f);
    }
}
