using System.Collections;
using UnityEngine;

public class LogoAwal : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float waktuTampil = 1f;
    [SerializeField] float durasiHilang = 1f;

    IEnumerator Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        yield return new WaitForSecondsRealtime(waktuTampil);

        float waktu = 0f;

        while (waktu < durasiHilang)
        {
            waktu += Time.unscaledDeltaTime;
            canvasGroup.alpha = 1f -
                Mathf.Clamp01(waktu / durasiHilang);

            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}