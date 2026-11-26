using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Satu kartu barang di Toko: nama, ikon, harga, tombol Beli, dan keadaan terkunci / dimiliki / level alat.
public class KartuItemToko : MonoBehaviour
{
    [SerializeField] Image latar, ikon;
    [SerializeField] Sprite kartuNormal, kartuTerkunci;
    [SerializeField] TMP_Text nama, harga, teksLabel, teksSyarat;
    [SerializeField] GameObject barisHarga, label, gembok;
    [SerializeField] Button tombolBeli;
    [SerializeField] Sprite beliAktif, beliMati;
    [SerializeField] Transform barisLevel;
    [SerializeField] Image templatPip;
    [SerializeField] Sprite pipIsi, pipKosong;

    DataItemToko item;
    Image[] pip = new Image[0];

    public void Isi(DataItemToko data)
    {
        item = data;
        nama.text = item.nama;
        ikon.sprite = item.sprite;
        teksSyarat.text = $"Buka di Tahap {item.tahapSyarat}";

        bool alat = item.kategori == KategoriItem.Peralatan;
        teksLabel.text = alat ? "Maks" : "Dimiliki";
        barisLevel.gameObject.SetActive(alat);
        if (alat)
            pip = Enumerable.Range(0, item.levelMaks).Select(_ => Instantiate(templatPip, barisLevel)).ToArray();
        foreach (var p in pip) p.gameObject.SetActive(true);

        tombolBeli.onClick.AddListener(() => item.Beli());   // Beli memicu UangBerubah -> Perbarui
        Perbarui();
    }

    void OnEnable() { DataPemain.UangBerubah += Perbarui; Perbarui(); }
    void OnDisable() => DataPemain.UangBerubah -= Perbarui;

    void Perbarui()
    {
        if (item == null) return;
        bool terkunci = item.Terkunci, habis = item.Habis, bisaBeli = !terkunci && !habis;

        latar.sprite = terkunci ? kartuTerkunci : kartuNormal;
        ikon.color = terkunci ? new Color(1f, 1f, 1f, 0.45f) : Color.white;
        gembok.SetActive(terkunci);
        label.SetActive(habis && !terkunci);
        barisHarga.SetActive(bisaBeli);
        tombolBeli.gameObject.SetActive(bisaBeli);

        int h = item.Harga;
        harga.text = HudUang.FormatAngka(h);
        bool cukup = DataPemain.BisaBayar(h);
        tombolBeli.interactable = cukup;
        tombolBeli.image.sprite = cukup ? beliAktif : beliMati;

        int level = DataPemain.LevelAlat(item.id);
        for (int i = 0; i < pip.Length; i++) pip[i].sprite = i < level ? pipIsi : pipKosong;
    }
}
