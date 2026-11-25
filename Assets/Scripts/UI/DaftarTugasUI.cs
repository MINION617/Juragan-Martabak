using System.Linq;
using UnityEngine;

// Daftar Misi/Pencapaian satu jenis; dibangun ulang tiap kali tampil. Dipakai langsung oleh aplikasi Pencapaian.
public class DaftarTugasUI : MonoBehaviour
{
    [SerializeField] JenisTugas jenis;
    [SerializeField] Transform isi;          // Content dari ScrollRect
    [SerializeField] BarisTugas templat;     // baris nonaktif di dalam isi

    void OnEnable() => Tampilkan(jenis);

    public void Tampilkan(JenisTugas j)
    {
        jenis = j;
        foreach (Transform anak in isi)
            if (anak != templat.transform) Destroy(anak.gameObject);

        foreach (var t in DataTugas.Semua.Where(t => t.jenis == j).OrderBy(t => t.name))
        {
            var baris = Instantiate(templat, isi);
            baris.gameObject.SetActive(true);
            baris.Isi(t);
        }
    }
}
