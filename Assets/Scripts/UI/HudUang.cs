using System.Globalization;
using TMPro;
using UnityEngine;

// Kotak uang di pojok kanan atas, ikut berubah saat DataPemain.UangBerubah.
public class HudUang : MonoBehaviour
{
    [SerializeField] TMP_Text teksUang;

    void OnEnable() { DataPemain.UangBerubah += Perbarui; Perbarui(); }
    void OnDisable() => DataPemain.UangBerubah -= Perbarui;

    // Tanpa "Rp" karena ikon sudah menunjukkan mata uang, dan angka jutaan tidak muat.
    void Perbarui() => teksUang.text = FormatAngka(DataPemain.Uang);

    // "85.000" — pemisah ribuan titik tanpa bergantung data kultur id-ID di perangkat.
    public static string FormatAngka(int jumlah) =>
        jumlah.ToString("N0", CultureInfo.InvariantCulture).Replace(',', '.');
}
