# Prompt Aset Toko untuk GPT (Juragan Martabak)

Daftar aset Toko yang masih kurang, prompt untuk ChatGPT (pembuat gambar), dan file referensi yang diunggah bersama tiap prompt.

## Cara pakai

1. Buka chat baru di ChatGPT. Tempel **Prompt Gaya** di bawah dan unggah referensi gayanya **sekali** di awal chat.
2. Kirim prompt aset **satu per satu** (satu gambar per pesan), dengan referensi tambahan untuk aset itu.
3. Simpan hasil sebagai PNG dengan nama file persis seperti di tabel, lalu taruh di `Assets/temp`. Nanti dipindahkan ke folder tujuan dan diatur import-nya.
4. Kalau hasil masih ada latar putih atau kotak-kotak abu, minta: *"Make the background fully transparent, PNG with alpha."*

Prompt ditulis dalam bahasa Inggris karena istilah gaya gambar lebih konsisten dipahami.

---

## Prompt Gaya (tempel sekali di awal chat)

**Unggah referensi gaya:**
- `Assets/Art/UI/Toko/Barang/ikon_resep_spesial.png`
- `Assets/Art/UI/Toko/Barang/ikon_alat_papan_pesanan.png`
- `Assets/Art/UI/Toko/Barang/ikon_dekorasi_tanaman_pot.png`
- `Assets/Art/UI/Toko/ui_toko_kartu.png`

```
I'm making UI icons for a cozy 2D cooking game about an Indonesian martabak street stall.
Match the style of the attached reference images exactly:
- cute cartoon game icon, clean vector-like shapes
- thick, even dark maroon outline (#8F3B46) around every shape
- soft cel shading with one darker shade, small glossy white highlight at top-left
- warm pastel palette: cream #FFF8F0, soft pink #F6B1AC, warm browns, muted teal/green accents
- light comes from the top-left
- single object, centered, front 3/4 view, fills about 85% of the canvas
- fully transparent background (PNG with alpha), no ground shadow, no text, no border, no frame
Reply "OK" and wait for my next message; each next message describes one icon.
```

---

## A. Wajib (pengganti placeholder)

Tujuan: `Assets/Art/UI/Toko/Barang/` · ukuran **1024×1024** PNG transparan (diperkecil ke 512 saat dipindah).

Daftar dekorasi dan resep di bawah masih **usulan**, karena spek Bagian 6 belum memutuskannya. Ganti nama atau isinya dulu kalau mau, lalu sesuaikan prompt-nya.

### Dekorasi (5 item, tiap dekorasi +3% kesabaran) — ✅ SELESAI, sudah terpasang di aset Dekorasi_1–5

| # | Nama file | Usulan dekorasi | Referensi tambahan |
|---|---|---|---|
| 1 | `ikon_dekorasi_tanaman_pot.png` | Tanaman pot | — (sudah ada, dulu `ikon_dekorasi_placeholder.png`) |
| 2 | `ikon_dekorasi_lampu_gantung.png` | Lampu hias gantung | `ikon_dekorasi_tanaman_pot.png` |
| 3 | `ikon_dekorasi_papan_kapur.png` | Papan menu kapur | `ikon_alat_papan_pesanan.png` |
| 4 | `ikon_dekorasi_radio_jadul.png` | Radio jadul | `ikon_dekorasi_tanaman_pot.png` |
| 5 | `ikon_dekorasi_bendera_hias.png` | Bendera hias merah-putih | `ikon_dekorasi_tanaman_pot.png` |

**2. Lampu hias gantung**
```
Icon: a short string of warm festive light bulbs hanging in a gentle curve between two small hooks,
5 round glowing bulbs in warm yellow and soft orange, dark maroon wire, subtle warm glow around each bulb.
Same style as the reference. Transparent background, no text.
```

**3. Papan menu kapur**
```
Icon: a small A-frame chalkboard menu sign with a wooden frame, dark green-black board,
simple white chalk doodles of a folded martabak and a few short lines (no readable letters),
a tiny chalk heart in the corner. Same style as the reference. Transparent background, no text.
```

**4. Radio jadul**
```
Icon: a cute retro 1970s Indonesian transistor radio, rounded boxy body in muted teal with cream front panel,
round speaker grille with dots, a small tuning dial, a carry handle on top and a short antenna tilted to the side.
Same style as the reference. Transparent background, no text.
```

**5. Bendera hias merah-putih**
```
Icon: a short string of small triangular bunting flags hanging in a gentle curve,
flags alternating red and white (Indonesian colors), cream string, slight wave in the flags.
Same style as the reference. Transparent background, no text.
```

### Resep Spesial (3 item) — ✅ SELESAI, sudah terpasang di aset Resep_Spesial1–3

Referensi tambahan untuk ketiganya: `Assets/Art/UI/Toko/Barang/ikon_resep_spesial.png` (bentuk gulungan resep harus sama, yang berubah hanya martabak di tengahnya).

| # | Nama file | Resep | Isi |
|---|---|---|---|
| 1 | `ikon_resep_martabak_cokelat_keju_kacang.png` | Martabak Cokelat Keju Kacang (manis) | satu rasa campur: meses cokelat + keju parut + kacang cincang |
| 2 | `ikon_resep_martabak_semua_rasa.png` | Martabak Semua Rasa (manis) | bundar seperti pizza, dipotong per irisan, tiap irisan beda rasa: cokelat, keju, kacang, selai stroberi |
| 3 | `ikon_resep_martabak_telur_spesial.png` | Martabak Telur Spesial (asin) | telur dobel (4 butir), daging cincang, daun bawang; isinya lebih tebal dari martabak telur biasa |

**1. Martabak Cokelat Keju Kacang**
```
Icon: the same parchment recipe scroll as the reference (rolled ends, red ribbon at the bottom, gold star sparkle top-right),
but the picture in the middle is a stack of folded sweet martabak (Indonesian thick pancake) slices,
golden honeycomb-textured cake layers, generously filled and topped with dark chocolate sprinkles,
grated yellow cheese and crushed roasted peanuts all mixed together, a little melted butter shine.
Keep the scroll shape, size and position identical to the reference. Transparent background, no readable text.
```

**2. Martabak Semua Rasa**
```
Icon: the same parchment recipe scroll as the reference (rolled ends, red ribbon at the bottom, gold star sparkle top-right),
but the picture in the middle is a whole round sweet martabak seen from above like a pizza (NOT stacked, NOT folded),
golden honeycomb-textured cake, cut into 4 wedge slices with a small gap between them,
each wedge has a different topping: dark chocolate sprinkles, grated yellow cheese, crushed roasted peanuts, glossy red strawberry jam.
Keep the scroll shape, size and position identical to the reference. Transparent background, no readable text.
```

**3. Martabak Telur Spesial**
```
Icon: the same parchment recipe scroll as the reference (rolled ends, red ribbon at the bottom, gold star sparkle top-right),
but the picture in the middle is an extra-thick savory martabak telur (Indonesian stuffed crispy pancake) cut into square pieces,
golden crispy thin crust, the cut side shows a very thick, generous filling of lots of egg, minced beef and chopped green scallion,
noticeably thicker than a normal martabak. Keep the scroll shape, size and position identical to the reference.
Transparent background, no readable text.
```

---

## B. Opsional (pemanis tampilan Toko)

### Papan judul "TOKO"

Tujuan: `Assets/Art/UI/Toko/` · nama `ui_toko_judul.png` · **1200×300** PNG transparan.
Teks "TOKO" sengaja tidak digambar: teksnya ditulis di Unity (font Lilita One) supaya bisa diganti.

**Unggah referensi:** `Assets/Art/UI/Toko/ui_toko_panel.png`, `Assets/Art/MainMenu/Tombol Toko Pastel Juragan Martabak-2.png`

```
UI element: a wide horizontal shop title banner / signboard for a game menu, empty in the middle (NO text),
cream plank face (#FFF8F0) with a thick dark maroon outline (#8F3B46), a small red-and-cream striped awning strip along the top edge
like a market stall, slightly rounded corners, a subtle darker lip at the bottom for depth.
Flat front view, perfectly symmetrical, wide 4:1 shape filling the canvas. Transparent background, no text.
```

### Penanda level alat — ✅ SELESAI (`ui_level_pip_kosong.png` buatan sendiri, `ui_level_pip_isi.png` dari GPT)

Tujuan: `Assets/Art/UI/Toko/` · **128×128** PNG transparan, 2 file:

| Nama file | Isi |
|---|---|
| `ui_level_pip_isi.png` | bulatan penuh (level sudah dibeli) |
| `ui_level_pip_kosong.png` | bulatan kosong (level belum dibeli) |

**Unggah referensi:** `Assets/Art/UI/HPku/ui_centang.png`

```
UI element pair, two separate images please (one per reply).
Image 1 "filled": a small round gem-like dot, warm golden yellow with a glossy white highlight top-left,
thick dark maroon outline (#8F3B46).
Image 2 "empty": the same round dot shape and outline, but the inside is a pale cream (#FFF8F0) hollow with no gloss.
Flat front view, centered, fills 80% of the canvas. Transparent background, no text.
```

### Ikon topping versi kartun (agar senada dengan ikon alat) — ❌ TIDAK PERLU, sprite topping yang ada sudah dianggap cukup kartun

Hanya kalau perbedaan gaya terasa mengganggu di kartu Toko. Tujuan: `Assets/Art/UI/Toko/Barang/` · **1024×1024** PNG transparan.

Untuk tiap topping, unggah sprite aslinya sebagai referensi bentuk (`Assets/Sprites/Toppings/<Sweet|Savory>/<Nama>_Held.png`), lalu kirim:

```
Redraw the attached topping as a shop icon in the same cartoon style as the earlier references:
thick dark maroon outline around the whole pile, soft cel shading, glossy highlight top-left,
served in a small cream ceramic bowl with a maroon rim. Keep the topping recognizable.
Transparent background, no text.
```

| Nama file | Sprite referensi |
|---|---|
| `ikon_topping_cokelat.png` | `Sweet/Chocolate_Held.png` |
| `ikon_topping_keju.png` | `Sweet/Cheese_Held.png` |
| `ikon_topping_kacang.png` | `Sweet/Peanut_Held.png` |
| `ikon_topping_selai.png` | `Sweet/Jam_Held.png` |
| `ikon_topping_telur_ayam.png` | `Savory/ChickenEgg_Held.png` |
| `ikon_topping_daun_bawang.png` | `Savory/GreenOnion_Held.png` |
| `ikon_topping_ayam.png` | `Savory/Chicken_Held.png` |
| `ikon_topping_kornet.png` | `Savory/CornedBeef_Held.png` |
| `ikon_topping_daging.png` | `Savory/MincedMeat_Held.png` |
| `ikon_topping_telur_bebek.png` | `Savory/DuckEgg_Held.png` |

---

## C. Tidak bisa dari GPT: suara

ChatGPT hanya membuat gambar, tidak membuat file audio. Folder `Assets/Art/Bgm` dan `Assets/Art/Fx` masih kosong. Kalau Toko mau ada suara, yang dibutuhkan:

| Nama file | Kegunaan |
|---|---|
| `sfx_beli.wav` | beli berhasil (bunyi koin / "cha-ching") |
| `sfx_gagal.wav` | uang kurang atau barang terkunci |
| `sfx_klik.wav` | klik tab dan tombol |
| `bgm_menu_pagi.ogg` | musik Menu Pagi (loop) |

Sumber yang bisa dipakai: generator efek suara seperti jsfxr (untuk sfx), atau pustaka audio berlisensi bebas (cek lisensinya untuk game komersial).

---

## Ringkasan

| Bagian | Jumlah gambar | Prioritas |
|---|---|---|
| A. Dekorasi | 4 baru + 1 ganti nama | ✅ Selesai |
| A. Resep Spesial | 3 | ✅ Selesai |
| B. Papan judul TOKO | 1 | Opsional, belum dibuat |
| B. Penanda level | 2 | ✅ Selesai |
| B. Ikon topping kartun | 10 | ❌ Tidak perlu |
| C. Suara | 4 file audio | Opsional, bukan dari GPT |
