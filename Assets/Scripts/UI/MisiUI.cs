using UnityEngine;
using UnityEngine.UI;

// Aplikasi Misi: tab Harian | Per Tahap di atas satu daftar.
public class MisiUI : MonoBehaviour
{
    [SerializeField] DaftarTugasUI daftar;
    [SerializeField] Image tabHarian, tabPerTahap;
    [SerializeField] Sprite tabNormal, tabAktif;

    void OnEnable() => PilihHarian();

    public void PilihHarian() => Pilih(JenisTugas.MisiHarian);
    public void PilihPerTahap() => Pilih(JenisTugas.MisiPerTahap);

    void Pilih(JenisTugas jenis)
    {
        tabHarian.sprite = jenis == JenisTugas.MisiHarian ? tabAktif : tabNormal;
        tabPerTahap.sprite = jenis == JenisTugas.MisiPerTahap ? tabAktif : tabNormal;
        daftar.Tampilkan(jenis);
    }
}
