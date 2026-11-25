using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Aplikasi Cerita: daftar cut scene yang sudah terbuka, dibangun ulang tiap kali aplikasi dibuka.
public class CeritaUI : MonoBehaviour
{
    [SerializeField] Transform isi;        // Content dari ScrollRect
    [SerializeField] GameObject templat;   // kartu nonaktif di dalam isi
    [SerializeField] GameObject teksKosong;

    void OnEnable()
    {
        foreach (Transform anak in isi)
            if (anak.gameObject != templat) Destroy(anak.gameObject);

        var terbuka = DataCerita.Semua.Where(c => DataPemain.CeritaTerbuka(c.id)).OrderBy(c => c.name).ToList();
        foreach (var cerita in terbuka)
        {
            var kartu = Instantiate(templat, isi);
            kartu.SetActive(true);
            kartu.GetComponent<Image>().sprite = cerita.thumbnail;
            kartu.GetComponentInChildren<TMP_Text>().text = cerita.judul;
            kartu.GetComponent<Button>().onClick.AddListener(() => Putar(cerita));
        }
        teksKosong.SetActive(terbuka.Count == 0);
    }

    // shortcut: belum ada sistem cut scene, jadi hanya log; sambungkan ke pemutar cut scene saat sudah dibuat.
    void Putar(DataCerita cerita) => Debug.Log($"[Cerita] Putar: {cerita.judul}");
}
