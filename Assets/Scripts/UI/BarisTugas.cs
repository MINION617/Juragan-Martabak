using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Satu baris Misi/Pencapaian: judul, deskripsi, bar progres, hadiah, dan tombol Klaim.
public class BarisTugas : MonoBehaviour
{
    [SerializeField] TMP_Text judul, deskripsi, teksProgres, teksHadiah;
    [SerializeField] Image barIsi;
    [SerializeField] Button tombolKlaim;
    [SerializeField] Sprite klaimAktif, klaimMati;
    [SerializeField] GameObject centang;
    [SerializeField] CanvasGroup grup;
    [SerializeField] Image ikon;                          // opsional
    [SerializeField] Sprite ikonTerbuka, ikonTerkunci;    // opsional, untuk lencana Pencapaian

    DataTugas tugas;

    public void Isi(DataTugas t)
    {
        tugas = t;
        tombolKlaim.onClick.AddListener(Klaim);
        Perbarui();
    }

    void Klaim()
    {
        if (tugas.Klaim()) Perbarui();
    }

    void Perbarui()
    {
        var status = tugas.Status;
        judul.text = tugas.judul;
        deskripsi.text = tugas.deskripsi;
        teksProgres.text = $"{tugas.Progres}/{tugas.target}";
        barIsi.fillAmount = (float)tugas.Progres / tugas.target;
        teksHadiah.text = "+Rp " + HudUang.FormatAngka(tugas.hadiahUang);

        bool bisa = status == StatusTugas.BisaDiklaim;
        tombolKlaim.gameObject.SetActive(status != StatusTugas.Diklaim);
        tombolKlaim.interactable = bisa;
        tombolKlaim.image.sprite = bisa ? klaimAktif : klaimMati;
        centang.SetActive(status == StatusTugas.Diklaim);
        grup.alpha = status == StatusTugas.Terkunci ? 0.5f : 1f;

        if (ikonTerbuka != null)
            ikon.sprite = status >= StatusTugas.BisaDiklaim ? ikonTerbuka : ikonTerkunci;
    }
}
