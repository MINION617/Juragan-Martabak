using UnityEngine;
using UnityEngine.UI;

public class PatienceBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Sprite green;
    [SerializeField] private Sprite yellow;
    [SerializeField] private Sprite red;

    // value: 0 sampai 1 (sisa kesabaran)
    public void SetPatience(float value)
    {
        if (value >= 0.5f) barImage.sprite = green;
        else if (value >= 0.25f) barImage.sprite = yellow;
        else barImage.sprite = red;
    }
}