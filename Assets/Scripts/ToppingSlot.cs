using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
[RequireComponent(typeof(RectTransform), typeof(UnityEngine.UI.Image))]
public sealed class ToppingSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string namaTopping;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Topping dipilih: {namaTopping}", this);
    }
}
