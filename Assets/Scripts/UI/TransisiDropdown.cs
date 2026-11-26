using System.Collections;
using System.Linq;
using UnityEngine;

// Tombol-tombol dropdown turun satu per satu dari bawah tombol menu saat dibuka, dan naik kembali saat ditutup.
// Memakai waktu unscaled karena di gameplay dropdown dibuka saat Time.timeScale = 0.
public class TransisiDropdown : MonoBehaviour
{
    const float Durasi = 0.18f, Jeda = 0.05f, JarakAwal = 100f;

    RectTransform[] tombol;
    CanvasGroup[] grup;
    Vector2[] tujuan;

    void Awake()
    {
        tombol = transform.Cast<Transform>().Select(t => (RectTransform)t).ToArray();
        tujuan = tombol.Select(t => t.anchoredPosition).ToArray();
        grup = tombol.Select(t => t.TryGetComponent(out CanvasGroup g) ? g : t.gameObject.AddComponent<CanvasGroup>()).ToArray();
    }

    public bool Terbuka { get; private set; }

    void OnEnable() { Terbuka = true; StartCoroutine(Gerak(true)); }
    void OnDisable() => Terbuka = false;

    // Aman dipanggil saat animasi tutup masih berjalan: langsung berbalik membuka.
    public void Buka()
    {
        if (!gameObject.activeSelf) { gameObject.SetActive(true); return; }
        StopAllCoroutines();
        Terbuka = true;
        StartCoroutine(Gerak(true));
    }

    public void Tutup()
    {
        if (!gameObject.activeSelf) return;
        StopAllCoroutines();
        Terbuka = false;
        StartCoroutine(Gerak(false));
    }

    IEnumerator Gerak(bool buka)
    {
        float total = Durasi + Jeda * (tombol.Length - 1);
        for (float t = 0f; t < total; t += Time.unscaledDeltaTime)
        {
            Atur(buka ? t : total - t);
            yield return null;
        }
        Atur(buka ? total : 0f);
        if (!buka) gameObject.SetActive(false);
    }

    void Atur(float t)
    {
        var awal = tujuan[0] + Vector2.up * JarakAwal;
        for (int i = 0; i < tombol.Length; i++)
        {
            float p = Mathf.Clamp01((t - Jeda * i) / Durasi);
            p = 1f - (1f - p) * (1f - p);   // ease-out
            tombol[i].anchoredPosition = Vector2.LerpUnclamped(awal, tujuan[i], p);
            grup[i].alpha = p;
        }
    }
}
