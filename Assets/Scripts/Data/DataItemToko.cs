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

    public int SyaratReputasi => kategori == KategoriItem.Topping ? 20 + 15 * tahapSyarat : 0;

    // GDD: topping dikunci reputasi; barang lain dikunci tahap.
    public bool Terkunci => kategori == KategoriItem.Topping
        ? DataPemain.Reputasi < SyaratReputasi
        : DataPemain.Tahap < tahapSyarat;

    public bool Dimiliki => dimilikiAwal || DataPemain.PunyaItem(id);

    // Tidak bisa dibeli lagi: peralatan sudah level maks, barang lain sudah dimiliki.
    public bool Habis => kategori == KategoriItem.Peralatan ? DataPemain.LevelAlat(id) >= levelMaks : Dimiliki;
}
