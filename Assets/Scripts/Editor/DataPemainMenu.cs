using UnityEditor;
using UnityEngine;

// Menu testing untuk DataPemain.
public static class DataPemainMenu
{
    [MenuItem("Juragan Martabak/Tambah Uang 100.000")]
    static void TambahUang() { DataPemain.TambahUang(100000); LogData(); }

    [MenuItem("Juragan Martabak/Reset Data Pemain")]
    static void Reset() { DataPemain.HapusSemua(); LogData(); }

    [MenuItem("Juragan Martabak/Buka Semua Cerita")]
    static void BukaSemuaCerita()
    {
        foreach (var c in DataCerita.Semua) DataPemain.BukaCerita(c.id);
        LogData();
    }

    [MenuItem("Juragan Martabak/Penuhi Progres Semua Tugas")]
    static void PenuhiSemuaTugas()
    {
        foreach (var t in DataTugas.Semua) DataPemain.TambahProgresTugas(t.id, t.target);
        LogData();
    }

    [MenuItem("Juragan Martabak/Log Data Pemain")]
    static void LogData() => Debug.Log("[DataPemain] " + DataPemain.Ringkasan());
}
