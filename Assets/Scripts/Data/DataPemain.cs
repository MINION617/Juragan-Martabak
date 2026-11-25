using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Data pemain yang bertahan antar scene, disimpan sebagai satu JSON di PlayerPrefs.
public static class DataPemain
{
    const string KunciSimpan = "JuraganMartabak_Save";
    const int LevelPerTahap = 10, JumlahTahap = 3;

    [Serializable] struct LevelAlatEntri { public string id; public int level; }
    [Serializable] struct TugasEntri { public string id; public int progres; public bool diklaim; }

    [Serializable]
    class Isi
    {
        public int uang;
        public int reputasi = 20;
        public int tahap = 1;
        public int[] bintang = new int[JumlahTahap * LevelPerTahap];
        public List<string> itemDimiliki = new List<string>();
        public List<LevelAlatEntri> levelAlat = new List<LevelAlatEntri>();
        public List<string> ceritaTerbuka = new List<string>();
        public List<TugasEntri> tugas = new List<TugasEntri>();
    }

    static Isi isi;
    static Isi D { get { if (isi == null) Muat(); return isi; } }

    public static event Action UangBerubah;

    // Domain reload dimatikan di project ini, jadi static harus dikosongkan tiap masuk Play.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatic() { isi = null; UangBerubah = null; }

    public static int Uang => D.uang;
    public static int Reputasi { get => D.reputasi; set { D.reputasi = Mathf.Clamp(value, 0, 100); Simpan(); } }
    public static int Tahap { get => D.tahap; set { D.tahap = Mathf.Clamp(value, 1, JumlahTahap); Simpan(); } }

    public static void TambahUang(int jumlah)
    {
        D.uang = Mathf.Max(0, D.uang + jumlah);
        Simpan();
        UangBerubah?.Invoke();
    }

    public static bool BisaBayar(int biaya) => biaya >= 0 && D.uang >= biaya;

    public static bool Belanja(int biaya)
    {
        if (!BisaBayar(biaya)) return false;
        TambahUang(-biaya);
        return true;
    }

    public static bool PunyaItem(string id) => D.itemDimiliki.Contains(id);

    public static void BeriItem(string id)
    {
        if (PunyaItem(id)) return;
        D.itemDimiliki.Add(id);
        Simpan();
    }

    public static bool CeritaTerbuka(string id) => D.ceritaTerbuka.Contains(id);

    public static void BukaCerita(string id)
    {
        if (CeritaTerbuka(id)) return;
        D.ceritaTerbuka.Add(id);
        Simpan();
    }

    // Progres & klaim Misi/Pencapaian. Status (Terkunci/Aktif/BisaDiklaim/Diklaim) diturunkan di DataTugas.
    public static int ProgresTugas(string id) => CariTugas(id).progres;
    public static bool TugasDiklaim(string id) => CariTugas(id).diklaim;

    public static void TambahProgresTugas(string id, int jumlah)
    {
        var t = CariTugas(id);
        t.progres = Mathf.Max(0, t.progres + jumlah);
        SimpanTugas(t);
    }

    public static void TandaiTugasDiklaim(string id)
    {
        var t = CariTugas(id);
        t.diklaim = true;
        SimpanTugas(t);
    }

    static TugasEntri CariTugas(string id)
    {
        int i = D.tugas.FindIndex(e => e.id == id);
        return i < 0 ? new TugasEntri { id = id } : D.tugas[i];
    }

    static void SimpanTugas(TugasEntri t)
    {
        int i = D.tugas.FindIndex(e => e.id == t.id);
        if (i < 0) D.tugas.Add(t); else D.tugas[i] = t;
        Simpan();
    }

    public static int LevelAlat(string id)
    {
        int i = D.levelAlat.FindIndex(e => e.id == id);
        return i < 0 ? 1 : D.levelAlat[i].level;
    }

    public static void NaikkanLevelAlat(string id)
    {
        int i = D.levelAlat.FindIndex(e => e.id == id);
        var entri = new LevelAlatEntri { id = id, level = LevelAlat(id) + 1 };
        if (i < 0) D.levelAlat.Add(entri); else D.levelAlat[i] = entri;
        Simpan();
    }

    // tahap 1–3, level 1–10, bintang 0–3. Hanya hasil terbaik yang disimpan.
    public static void SetBintang(int tahap, int level, int bintang)
    {
        int i = IndeksLevel(tahap, level);
        bintang = Mathf.Clamp(bintang, 0, 3);
        if (bintang <= D.bintang[i]) return;
        D.bintang[i] = bintang;
        Simpan();
    }

    public static int GetBintang(int tahap, int level) => D.bintang[IndeksLevel(tahap, level)];

    public static int TotalBintang() => D.bintang.Sum();

    static int IndeksLevel(int tahap, int level)
    {
        if (tahap < 1 || tahap > JumlahTahap || level < 1 || level > LevelPerTahap)
            throw new ArgumentOutOfRangeException($"tahap {tahap}, level {level}");
        return (tahap - 1) * LevelPerTahap + (level - 1);
    }

    public static void Muat()
    {
        isi = new Isi();
        string json = PlayerPrefs.GetString(KunciSimpan, "");
        if (json != "")
        {
            try { JsonUtility.FromJsonOverwrite(json, isi); }
            catch (ArgumentException e) { Debug.LogWarning($"[DataPemain] Data simpan rusak, mulai dari awal: {e.Message}"); isi = new Isi(); }
        }
        if (isi.bintang == null || isi.bintang.Length != JumlahTahap * LevelPerTahap)
            Array.Resize(ref isi.bintang, JumlahTahap * LevelPerTahap);
        UangBerubah?.Invoke();
    }

    public static void Simpan()
    {
        PlayerPrefs.SetString(KunciSimpan, JsonUtility.ToJson(D));
        PlayerPrefs.Save();
    }

    public static void HapusSemua()
    {
        PlayerPrefs.DeleteKey(KunciSimpan);
        PlayerPrefs.Save();
        isi = new Isi();
        UangBerubah?.Invoke();
    }

    public static string Ringkasan() =>
        $"Uang Rp {D.uang:N0}, Reputasi {D.reputasi}, Tahap {D.tahap}, Bintang {TotalBintang()}/{D.bintang.Length * 3}, " +
        $"Item [{string.Join(", ", D.itemDimiliki)}], Alat [{string.Join(", ", D.levelAlat.Select(e => $"{e.id}:{e.level}"))}], " +
        $"Cerita [{string.Join(", ", D.ceritaTerbuka)}], " +
        $"Tugas [{string.Join(", ", D.tugas.Select(e => $"{e.id}:{e.progres}{(e.diklaim ? "✓" : "")}"))}]";
}
