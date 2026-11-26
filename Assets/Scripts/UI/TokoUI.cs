using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Panel Toko: tab kategori di kiri, deretan kartu barang yang di-scroll horizontal.
public class TokoUI : MonoBehaviour
{
    [SerializeField] Transform isi;                 // Content dari ScrollRect
    [SerializeField] ScrollRect scroll;
    [SerializeField] KartuItemToko prefabKartu;
    [SerializeField] Image[] tab;                   // urutan sama dengan KategoriItem
    [SerializeField] Sprite tabNormal, tabAktif;

    public void Buka()
    {
        gameObject.SetActive(true);
        PilihTab(0);
    }

    public void Tutup() => gameObject.SetActive(false);

    // int (bukan KategoriItem) agar bisa dipasang langsung di Button.onClick.
    public void PilihTab(int kategori)
    {
        for (int i = 0; i < tab.Length; i++) tab[i].sprite = i == kategori ? tabAktif : tabNormal;

        foreach (Transform anak in isi) Destroy(anak.gameObject);
        var barang = DataItemToko.Semua.Where(d => (int)d.kategori == kategori)
                                       .OrderBy(d => d.tahapSyarat).ThenBy(d => d.name);
        foreach (var item in barang) Instantiate(prefabKartu, isi).Isi(item);

        scroll.horizontalNormalizedPosition = 0f;
    }
}
