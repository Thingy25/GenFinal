using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOver : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject hoverPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverPanel.SetActive(false);
    }
}
