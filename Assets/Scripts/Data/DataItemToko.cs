using System.Linq;
using UnityEngine;

public enum KategoriItem { Topping, Resep, Peralatan, Dekorasi }

// Satu barang di Toko. Harga & syarat dihitung dari rumus GDD Bagian 6, bukan diisi per item.
// Semua aset diletakkan di Assets/Resources/ItemToko agar bisa dimuat tanpa disambung manual.
[CreateAssetMenu(menuName = "Juragan Martabak/Data Item Toko")]
public class DataItemToko : ScriptableObject
{
    public string id;
    public string nama;
    public KategoriItem kategori;
    public Sprite sprite;
    public int hargaDasar;
    public int levelMaks = 1;   // hanya dipakai Peralatan
    public int tahapSyarat;
    public bool dimilikiAwal;   // topping gratis sejak awal

    const int KenaikanHargaDekorasi = 50000;

    public static DataItemToko[] Semua => Resources.LoadAll<DataItemToko>("ItemToko");

    public int Harga
    {
        get
        {
            switch (kategori)
            {
                case KategoriItem.Topping:   return hargaDasar * (1 + tahapSyarat);
                case KategoriItem.Resep:     return hargaDasar * DataPemain.Tahap;
                case KategoriItem.Peralatan: return hargaDasar * (1 + DataPemain.LevelAlat(id));
                default: // Dekorasi
                    int dimiliki = Semua.Count(i => i.kategori == KategoriItem.Dekorasi && i.Dimiliki);
                    return hargaDasar + KenaikanHargaDekorasi * dimiliki;
            }
        }
    }

    // Semua barang dikunci tahap (reputasi tidak dipakai).
    public bool Terkunci => DataPemain.Tahap < tahapSyarat;

    public bool Dimiliki => dimilikiAwal || DataPemain.PunyaItem(id);

    // Tidak bisa dibeli lagi: peralatan sudah level maks, barang lain sudah dimiliki.
    public bool Habis => kategori == KategoriItem.Peralatan ? DataPemain.LevelAlat(id) >= levelMaks : Dimiliki;

    public bool Beli()
    {
        int harga = Harga;
        if (Terkunci || Habis || !DataPemain.BisaBayar(harga)) return false;
        if (kategori == KategoriItem.Peralatan) DataPemain.NaikkanLevelAlat(id);
        else DataPemain.BeriItem(id);
        // Bayar setelah barang diberikan: UangBerubah memicu kartu lain menghitung ulang harga (mis. dekorasi berikutnya).
        DataPemain.Belanja(harga);
        return true;
    }
}
