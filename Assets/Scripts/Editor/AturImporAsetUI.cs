using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

// Menerapkan pengaturan import + border 9-slice dari SPEK_TOKO_HPKU.md Bagian 7.
public static class AturImporAsetUI
{
    static readonly string[] FolderSprite = { "Assets/Art/UI/Toko", "Assets/Art/UI/HPku" }; // Toko mencakup Toko/Barang
    static readonly string[] FileTambahan =
    {
        "Assets/Art/MainMenu/ui_tombol_hpku.png", "Assets/Art/MainMenu/ui_ikon_hpku.png",
        "Assets/Art/UI/ui_hud_rupiah.png", "Assets/Art/UI/ui_ikon_rupiah.png",
    };

    // Nama file -> border (kiri, bawah, kanan, atas). Yang tidak terdaftar = nol.
    static readonly Dictionary<string, Vector4> Border = new Dictionary<string, Vector4>
    {
        { "ui_toko_panel",          new Vector4(44, 44, 44, 44) },
        { "ui_toko_panel_tab",      new Vector4(44, 44, 44, 44) },
        { "ui_toko_tab_normal",     new Vector4(36, 36, 36, 36) },
        { "ui_toko_tab_aktif",      new Vector4(36, 36, 36, 36) },
        { "ui_toko_kartu",          new Vector4(48, 48, 48, 48) },
        { "ui_toko_kartu_terkunci", new Vector4(48, 48, 48, 48) },
        { "ui_tombol_beli",         new Vector4(34, 34, 34, 34) },
        { "ui_tombol_beli_mati",    new Vector4(34, 34, 34, 34) },
        { "ui_label_dimiliki",      new Vector4(36, 0, 36, 0) },
        { "ui_hpku_layar",          new Vector4(32, 32, 32, 32) },
        { "ui_hpku_header",         new Vector4(32, 8, 32, 32) },
        { "ui_hpku_baris",          new Vector4(30, 30, 30, 30) },
        { "ui_tombol_klaim",        new Vector4(30, 30, 30, 30) },
        { "ui_tombol_klaim_mati",   new Vector4(30, 30, 30, 30) },
        { "ui_bar_latar",           new Vector4(20, 20, 20, 20) },
        { "ui_bar_isi",             new Vector4(18, 18, 18, 18) },
    };

    [MenuItem("Juragan Martabak/Atur Impor Aset UI")]
    static void Jalankan()
    {
        var jalur = AssetDatabase.FindAssets("t:Texture2D", FolderSprite)
            .Select(AssetDatabase.GUIDToAssetPath)
            .Concat(FileTambahan.Where(File.Exists))
            .Distinct()
            .ToList();

        int jumlah = 0;
        var ditemukan = new HashSet<string>();
        foreach (var p in jalur)
        {
            if (!(AssetImporter.GetAtPath(p) is TextureImporter imp)) continue;
            string nama = Path.GetFileNameWithoutExtension(p);

            imp.textureType = TextureImporterType.Sprite;
            imp.spriteImportMode = SpriteImportMode.Single;
            imp.mipmapEnabled = false;
            imp.alphaIsTransparency = true;
            imp.maxTextureSize = 2048;
            imp.textureCompression = TextureImporterCompression.Compressed;

            var s = new TextureImporterSettings();
            imp.ReadTextureSettings(s);
            s.spriteMeshType = SpriteMeshType.FullRect;
            s.spriteBorder = Border.TryGetValue(nama, out var b) ? b : Vector4.zero;
            imp.SetTextureSettings(s);

            imp.SaveAndReimport();
            if (Border.ContainsKey(nama)) ditemukan.Add(nama);
            jumlah++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        foreach (var hilang in Border.Keys.Except(ditemukan))
            Debug.LogWarning($"[AturImporAsetUI] Sprite ber-border tidak ditemukan: {hilang}");
        foreach (var f in FileTambahan.Where(f => !File.Exists(f)))
            Debug.LogWarning($"[AturImporAsetUI] File tidak ditemukan: {f}");
        Debug.Log($"[AturImporAsetUI] {jumlah} sprite diatur.");
    }
}
