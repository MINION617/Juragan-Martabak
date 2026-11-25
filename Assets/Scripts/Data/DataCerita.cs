using UnityEngine;

// Satu cut scene yang bisa diputar ulang dari aplikasi Cerita di HP-ku.
// Aset diletakkan di Assets/Resources/Cerita; urutan tampil mengikuti nama aset.
[CreateAssetMenu(menuName = "Juragan Martabak/Data Cerita")]
public class DataCerita : ScriptableObject
{
    public string id;
    public string judul;
    public Sprite thumbnail;

    public static DataCerita[] Semua => Resources.LoadAll<DataCerita>("Cerita");
}
