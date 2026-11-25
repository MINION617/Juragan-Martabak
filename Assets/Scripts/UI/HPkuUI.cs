using System;
using TMPro;
using UnityEngine;

// Panel HP-ku: beranda berisi ikon aplikasi; tombol Home kembali ke beranda, atau menutup HP jika sudah di beranda.
public class HPkuUI : MonoBehaviour
{
    [SerializeField] GameObject beranda;
    [SerializeField] TMP_Text jam;

    GameObject aplikasiAktif;

    public void Buka()
    {
        gameObject.SetActive(true);
        KeBeranda();
    }

    public void Tutup() => gameObject.SetActive(false);

    public void BukaAplikasi(GameObject aplikasi)
    {
        beranda.SetActive(false);
        aplikasi.SetActive(true);
        aplikasiAktif = aplikasi;
    }

    public void TekanHome()
    {
        if (aplikasiAktif != null) KeBeranda();
        else Tutup();
    }

    void KeBeranda()
    {
        if (aplikasiAktif != null) aplikasiAktif.SetActive(false);
        aplikasiAktif = null;
        beranda.SetActive(true);
    }

    // shortcut: jam memakai waktu perangkat, ganti ke jam gim kalau GameClock sudah ada di Menu Pagi.
    void OnEnable() => InvokeRepeating(nameof(PerbaruiJam), 0f, 1f);
    void OnDisable() => CancelInvoke(nameof(PerbaruiJam));
    void PerbaruiJam() => jam.text = DateTime.Now.ToString("HH:mm");
}
