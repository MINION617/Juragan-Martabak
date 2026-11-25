using UnityEngine;

public enum JenisTugas { MisiHarian, MisiPerTahap, Pencapaian }
public enum StatusTugas { Terkunci, Aktif, BisaDiklaim, Diklaim }

// Satu Misi atau Pencapaian. Progres & klaim disimpan di DataPemain; status diturunkan dari situ.
// Aset diletakkan di Assets/Resources/Tugas; urutan tampil mengikuti nama aset.
[CreateAssetMenu(menuName = "Juragan Martabak/Data Tugas")]
public class DataTugas : ScriptableObject
{
    public string id;
    public JenisTugas jenis;
    public string judul;
    [TextArea] public string deskripsi;
    public int target = 1;
    public int hadiahUang;
    public int tahapSyarat = 1;   // Misi Per Tahap terkunci sampai pemain mencapai tahap ini

    public static DataTugas[] Semua => Resources.LoadAll<DataTugas>("Tugas");

    public int Progres => Mathf.Min(DataPemain.ProgresTugas(id), target);

    public StatusTugas Status
    {
        get
        {
            if (DataPemain.TugasDiklaim(id)) return StatusTugas.Diklaim;
            if (DataPemain.Tahap < tahapSyarat) return StatusTugas.Terkunci;
            return Progres >= target ? StatusTugas.BisaDiklaim : StatusTugas.Aktif;
        }
    }

    public bool Klaim()
    {
        if (Status != StatusTugas.BisaDiklaim) return false;
        DataPemain.TandaiTugasDiklaim(id);
        DataPemain.TambahUang(hadiahUang);
        return true;
    }
}
