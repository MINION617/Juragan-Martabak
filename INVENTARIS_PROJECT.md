# Inventaris Project — Juragan Martabak

> Data diambil langsung dari file scene, prefab, `.meta`, dan skrip di working copy (termasuk perubahan yang **belum di-commit**) pada 25 November 2026.

## Daftar Isi
1. [Ringkasan](#1-ringkasan)
2. [Scene](#2-scene)
3. [Prefab](#3-prefab)
4. [Kode (Scripts)](#4-kode-scripts)
5. [Aset](#5-aset)
6. [Package & Pengaturan](#6-package--pengaturan)
7. [Temuan & Catatan](#7-temuan--catatan)

---

## 1. Ringkasan

| Item | Jumlah / Nilai |
|---|---|
| Versi Unity | 6000.6.3f1 (Unity 6) |
| Render pipeline | URP 2D (`Renderer2D.asset`) |
| Scene | 3 (`MainMenu`, `SampleScene`, `GamePlay`) |
| Prefab | 2 (`MainMenuCar`, `MartabakMenu`) |
| Skrip game | 7 (+1 skrip Editor, +1 skrip template Welcome) |
| Gambar (PNG) game | 30 (4 ikon, 7 latar, 5 UI main menu, 14 topping) |
| Screenshot | 11 file (~25 MB) |
| Folder kosong | `Animations`, `Audio`, `Materials`, `Resources` |

**Urutan Build Settings:**

| Index | Scene | Aktif |
|---|---|---|
| 0 | `Assets/Scenes/MainMenu.unity` | ✅ |
| 1 | `Assets/Scenes/SampleScene.unity` | ✅ |
| 2 | `Assets/Scenes/GamePlay.unity` | ✅ |

---

## 2. Scene

### 2.1 MainMenu (`Assets/Scenes/MainMenu.unity`)
Menu utama: latar toko, logo yang memudar, tombol menu/pengaturan/toko, dan mobil yang lewat.

- **GameObject:** 60 (6 nonaktif secara default)
- **Skrip dipakai:** `LogoAwal`, `MartabakMenuUI`, `MainMenuCarMotion` (lewat prefab)

**Hierarki:**
```
Main Camera            Camera, AudioListener, UniversalAdditionalCameraData
Global Light 2D        Light2D
Canvas                 Canvas, CanvasScaler (1920x1080, match 0.5), GraphicRaycaster
├── Background
│   └── LatarT1Dalam   Image → bgMainMenu.png
├── UI
│   └── Canvas         Canvas kosong (bersarang, tanpa isi)
├── Logo               Image, CanvasGroup, LogoAwal   ⚠ sprite hilang (missing)
├── MartabakMenuRoot   MartabakMenuUI (isMainMenu = true) — salinan lokal, BUKAN instance prefab
│   ├── MenuButton            Button → UI_Menu
│   ├── Dropdown (off)
│   │   ├── SettingsButton    Button → UI_Settings
│   │   ├── HomeButton        Button → UI_Home
│   │   └── ShopButton        Button → UI_Shop
│   ├── PauseButton (off)     Button → UI_Pause
│   ├── MainShopButton        Button → Tombol Toko Pastel Juragan Martabak-2.png
│   │   └── MainShopIllustration  Button → Ikon toko martabak mungil-1.png
│   ├── SettingsPanel (off)   "Pengaturan": slider Musik, slider Efek Suara, tombol "Tutup"
│   ├── ShopPanel (off)       "Toko": WarungIllustration, "Barang belum tersedia", tombol "Tutup"
│   ├── PausePanel (off)      "Jeda": Lanjutkan / Settings / Home
│   └── HomeConfirmPanel (off) "Kembali ke menu utama?": Ya / Batal
└── MainMenuCar        Instance prefab MainMenuCar.prefab
EventSystem            EventSystem, InputSystemUIInputModule
```

**Aset dipakai:** `bgMainMenu.png`, `Lembar sprite tombol martabak enam ikon.png` (UI_Menu, UI_Settings, UI_Home, UI_Pause, UI_Shop), `Panel tombol kosong hijau zaitun-3.png`, `Tombol Toko Pastel Juragan Martabak-2.png`, `Ikon toko martabak mungil-1.png`, `Sprite sheet mobil teal empat frame.png` (lewat prefab), font `LiberationSans SDF`.

---

### 2.2 GamePlay (`Assets/Scenes/GamePlay.unity`)
Layar warung: latar luar berganti siang/sore/malam sesuai jam gim, panel jam + level, prototype ambil topping, dan menu jeda.

- **GameObject:** 16 (+ isi 2 instance prefab)
- **Skrip dipakai:** `GameClock`, `BackgroundLuarSwitcher`, `ToppingHeldOnly`, `MartabakMenuUI` (prefab), `MainMenuCarMotion` (prefab)

**Hierarki:**
```
Main Camera            Camera, AudioListener, UniversalAdditionalCameraData
Global Light 2D        Light2D
Canvas                 Canvas, CanvasScaler, GraphicRaycaster
├── Background
│   ├── LatarT1LuarSiang   Image → Maskot koki Pak Kumis yang galak.png
│   ├── LatarT1LuarSore    Image → Golden hour at Martabak Pak Kumis-1.png
│   ├── LatarT1LuarMalam   Image → Martabak Pak Kumis under night lights-2.png
│   ├── LatarT1Dalam       Image → Interior toko Indonesia terbuka tanpa kaca.png
│   ├── LatarT1MejaDepan   Image → Tablet putih di meja dapur.png
│   └── MobilJalan         Instance prefab MainMenuCar (override: ukuran 770x395, passDelay 30, roadBottomY -280)
├── UI
│   └── PanelJam           Image → ui_jam_panel.png
│       ├── IkonJam        Image → ui_jam_ikon_siang.png
│       ├── TxtLevel       TextMeshPro "Level 1"
│       └── TxtJam         TextMeshPro "10.00"
└── MartabakMenuRoot       Instance prefab MartabakMenu.prefab (isMainMenu = false)
EventSystem            EventSystem, InputSystemUIInputModule
GameManajer            GameClock, BackgroundLuarSwitcher, ToppingHeldOnly
```

**Nilai Inspector di `GameManajer`:**

| Komponen | Field | Nilai |
|---|---|---|
| GameClock | level | 1 |
| | realSecondsPerGameHour | **3** (nilai testing; GDD = 45) |
| | startHour / endHour | 10 / 21 |
| | minutesPerStep | 15 |
| BackgroundLuarSwitcher | siang / sore / malam | LatarT1LuarSiang / LatarT1LuarSore / LatarT1LuarMalam |
| | ikonSiang / ikonSore / ikonMalam | ui_jam_ikon_siang_0 / ui_jam_ikon_sore_0 / ui_jam_ikon_malam_0 |
| | soreMulai / malamMulai | 15 / 18 |
| ToppingHeldOnly | ukuranHeld | 96 |
| | offsetKursor | (35, 35) |

**Slot topping (`ToppingHeldOnly.toppingSlots`):**

| # | Nama | Sprite Held | areaKlik |
|---|---|---|---|
| 1 | Cokelat | `Sweet/Chocolate_Held.png` | ⚠ kosong |
| 2 | Keju | `Sweet/Cheese_Held.png` | ⚠ kosong |
| 3 | Kacang | `Sweet/Peanut_Held.png` | ⚠ kosong |
| 4 | Selai | `Sweet/Jam_Held.png` | ⚠ kosong |
| 5 | Ayam | `Savory/Chicken_Held.png` | ⚠ kosong |
| 6 | Kornet | `Savory/CornedBeef_Held.png` | ⚠ kosong |
| 7 | Daging | `Savory/MincedMeat_Held.png` | ⚠ kosong |
| 8 | TelurAyam | `Savory/ChickenEgg_Held.png` | ⚠ kosong |
| 9 | TelurBebek | `Savory/DuckEgg_Held.png` | ⚠ kosong |
| 10 | DaunBawang | `Savory/GreenOnion_Held.png` | ⚠ kosong |

> Lihat [Temuan #1](#7-temuan--catatan) — di commit terakhir slot-slot ini masih terhubung.

---

### 2.3 SampleScene (`Assets/Scenes/SampleScene.unity`)
Scene bawaan template 2D, kosong.

```
Main Camera        Camera, AudioListener, UniversalAdditionalCameraData
Global Light 2D    Light2D
```

---

## 3. Prefab

### 3.1 MainMenuCar (`Assets/Prefabs/UI/MainMenuCar.prefab`)
Mobil teal yang lewat bolak-balik di jalan.

| GameObject | Komponen | Aset |
|---|---|---|
| MainMenuCar | Image, `MainMenuCarMotion` | `Sprite sheet mobil teal empat frame.png` → Car_TopLeft, Car_TopRight, Car_BottomLeft, Car_BottomRight |

**Dipakai di:** MainMenu (`MainMenuCar`), GamePlay (`MobilJalan`).

### 3.2 MartabakMenu (`Assets/Prefabs/UI/MartabakMenu.prefab`)
UI menu lengkap (hamburger, dropdown, panel Pengaturan/Toko/Jeda/Konfirmasi Home). Dibuat otomatis oleh `MartabakMenuBuilder`.

- **GameObject:** 51 (6 nonaktif)
- **Struktur:** sama seperti `MartabakMenuRoot` di MainMenu (lihat 2.1).
- **Teks UI:** "Pengaturan", "Musik", "Efek Suara", "Tutup", "Toko", "Barang belum tersedia", "Jeda", "Lanjutkan", "Settings", "Home", "Kembali ke menu utama?", "Ya", "Batal".

**Dipakai di:** GamePlay (instance prefab). MainMenu memakai salinan lokal yang tidak terhubung ke prefab.

---

## 4. Kode (Scripts)

| Skrip | Baris | Jenis | Dipakai di |
|---|---|---|---|
| `Scripts/BackgroundLuarSwitcher.cs` | 30 | MonoBehaviour | GamePlay › GameManajer |
| `Scripts/GameClock.cs` | 51 | MonoBehaviour | GamePlay › GameManajer |
| `Scripts/LogoAwal.cs` | 34 | MonoBehaviour | MainMenu › Logo |
| `Scripts/MainMenuCarMotion.cs` | 119 | MonoBehaviour | Prefab MainMenuCar |
| `Scripts/MartabakMenuUI.cs` | 240 | MonoBehaviour | Prefab MartabakMenu, MainMenu |
| `Scripts/ToppingHeldOnly.cs` | 144 | MonoBehaviour | GamePlay › GameManajer |
| `Scripts/ToppingSlot.cs` | 14 | MonoBehaviour | ❌ tidak dipakai |
| `Scripts/Editor/MartabakMenuBuilder.cs` | 228 | Editor (static) | Alat editor |
| `Welcome/Welcome2DScript.cs` | — | Template Unity | Halaman Welcome 2D |

### `GameClock`
Jam gim 10.00–21.00; 1 jam gim = `realSecondsPerGameHour` detik nyata. Menampilkan waktu format "5:45 PM" (dibulatkan per `minutesPerStep` menit) dan "Level N".
- **Field:** `clockText`, `levelText`, `level`, `realSecondsPerGameHour`, `startHour`, `endHour`, `minutesPerStep`
- **Publik:** `IsOpen` (false setelah jam tutup), `GameHour`
- Saat jam tutup: `Debug.Log("Warung tutup")`.

### `BackgroundLuarSwitcher`
Membaca `GameClock.GameHour` lalu mengaktifkan latar siang (< `soreMulai`), sore (< `malamMulai`), atau malam, dan mengganti ikon jam. Hanya berganti saat fase berubah.
- **Field:** `clock`, `siang`, `sore`, `malam`, `ikonJam`, `ikonSiang`, `ikonSore`, `ikonMalam`, `soreMulai`, `malamMulai`

### `ToppingHeldOnly`
Prototype interaksi topping: klik kiri di area slot → sprite "Held" mengikuti kursor; klik kanan / Esc → batal. Membuat objek `HeldTopping_Runtime` di Canvas saat Awake. Mendukung Input System baru dan lama.
- **Field:** `canvas`, `toppingSlots` (nama, areaKlik, spriteHeld), `ukuranHeld`, `offsetKursor`
- **Publik:** `BatalkanHeld()`
- Belum ada: tabur, taruh, masak, kocok.

### `ToppingSlot`
Komponen klik (IPointerClickHandler) yang hanya mencetak `Topping dipilih: <nama>` ke Console. Tidak terpasang di scene/prefab mana pun.

### `LogoAwal`
Menampilkan logo selama `waktuTampil` (1 dtk), lalu memudarkan `CanvasGroup` selama `durasiHilang` (1 dtk) dan menonaktifkan objek. Memakai waktu unscaled.

### `MainMenuCarMotion`
Animasi 4 frame mobil (`framesPerSecond` = 8) + gerakan horizontal dari luar layar kiri ke kanan, tunggu `passDelay`, lalu balik arah (dicerminkan dengan scale X negatif).
- **Field:** `frames[4]`, `framesPerSecond`, `speed` (350), `passDelay` (5), `roadBottomY` (35), `carSize` (808x490), `leftOutsideMargin`, `rightOutsideMargin`, `startsMovingRight`, `flipX`

### `MartabakMenuUI`
Pengendali seluruh UI menu.
- **Main menu (`isMainMenu = true`):** tombol menu membuka/menutup dropdown; Home tidak melakukan apa-apa.
- **Gameplay:** tombol menu = Jeda (`Time.timeScale = 0`, overlay abu-abu + ikon pause), tekan lagi = Lanjut. Home → panel konfirmasi → `SceneManager.LoadScene("MainMenu")`.
- **Pengaturan:** slider Musik & Efek Suara disimpan ke `PlayerPrefs` (`Martabak_MusicVolume`, `Martabak_SfxVolume`) dan diterapkan ke semua `AudioSource` (yang `loop` atau bernama "music" dianggap musik).
- **Fungsi publik:** `ToggleMenu`, `OpenSettingsFromMenu`, `OpenSettingsFromPause`, `CloseSettings`, `OpenShop`, `CloseShop`, `Pause`, `Resume`, `HomeFromMenu`, `HomeFromPause`, `CancelHome`, `ConfirmHome`, `SetMusicVolume`, `SetSfxVolume`
- **Static:** `MusicVolume`, `SfxVolume`

### `MartabakMenuBuilder` (Editor)
Membangun hierarki `MartabakMenuRoot` dari kode, menyimpannya sebagai `Assets/Prefabs/UI/MartabakMenu.prefab`, memasangnya di MainMenu & GamePlay, mengatur CanvasScaler 1920x1080, dan menambahkan GamePlay ke Build Settings. Dipanggil lewat `MartabakMenuBuilder.Build()` (tidak ada `[MenuItem]`, jadi tidak muncul di menu Unity).

---

## 5. Aset

### 5.1 `Assets/Art/Ikon` — ikon jam

| File | Ukuran | Dipakai di |
|---|---|---|
| `ui_jam_panel.png` | 340×110 | GamePlay › PanelJam |
| `ui_jam_ikon_siang.png` | 128×128 | GamePlay › IkonJam, BackgroundLuarSwitcher |
| `ui_jam_ikon_sore.png` | 128×128 | BackgroundLuarSwitcher |
| `ui_jam_ikon_malam.png` | 128×128 | BackgroundLuarSwitcher |

### 5.2 `Assets/Art/Latar` — latar belakang

| File | Ukuran | Berat | Dipakai di |
|---|---|---|---|
| `bgMainMenu.png` (sprite: "Roomy Checkerboard Shop…") | 1672×941 | 1.7 MB | MainMenu › LatarT1Dalam |
| `Maskot koki Pak Kumis yang galak.png` | 1672×941 | 1.8 MB | GamePlay › LatarT1LuarSiang |
| `Golden hour at Martabak Pak Kumis-1.png` | 1672×941 | 1.8 MB | GamePlay › LatarT1LuarSore |
| `Martabak Pak Kumis under night lights-2.png` | 1672×941 | 1.8 MB | GamePlay › LatarT1LuarMalam |
| `Interior toko Indonesia terbuka tanpa kaca.png` | 1672×941 | 1.0 MB | GamePlay › LatarT1Dalam |
| `Tablet putih di meja dapur.png` | 1672×941 | 0.7 MB | GamePlay › LatarT1MejaDepan |
| `Gemini_Generated_Image_85gmse85gmse85gm-Photoroom.png` | 2400×1340 | 2.5 MB | ❌ tidak dipakai |

### 5.3 `Assets/Art/MainMenu` — UI menu

| File | Ukuran | Sprite di dalamnya | Dipakai di |
|---|---|---|---|
| `Lembar sprite tombol martabak enam ikon.png` | 1536×1024 | UI_Menu, UI_Settings, UI_Home, UI_Pause, UI_Shop, UI_Blank | Tombol menu/dropdown (UI_Blank tidak dipakai) |
| `Panel tombol kosong hijau zaitun-3.png` | 2022×778 | — | Semua tombol berlabel (Tutup, Lanjutkan, Ya, Batal, dll.) |
| `Tombol Toko Pastel Juragan Martabak-2.png` | 2172×724 | — | MainShopButton |
| `Ikon toko martabak mungil-1.png` | 1254×1254 | — | MainShopIllustration, WarungIllustration |
| `Sprite sheet mobil teal empat frame.png` | 1254×1254 | Car_TopLeft, Car_TopRight, Car_BottomLeft, Car_BottomRight | Prefab MainMenuCar |

### 5.4 `Assets/Sprites/Toppings` — topping martabak (semua 1254×1254)

| Kategori | File | Nama di game | Dipakai |
|---|---|---|---|
| Manis | `Sweet/Chocolate_Held.png` | Cokelat | ✅ slot topping |
| Manis | `Sweet/Cheese_Held.png` | Keju | ✅ slot topping |
| Manis | `Sweet/Peanut_Held.png` | Kacang | ✅ slot topping |
| Manis | `Sweet/Jam_Held.png` | Selai | ✅ slot topping |
| Manis | `Sweet/Chocolate_Sprinkle.png` | — | ❌ (untuk fitur tabur nanti) |
| Manis | `Sweet/Cheese_Sprinkle.png` | — | ❌ |
| Manis | `Sweet/Peanut_Sprinkle.png` | — | ❌ |
| Manis | `Sweet/Jam_Spread.png` | — | ❌ |
| Asin | `Savory/Chicken_Held.png` | Ayam | ✅ slot topping |
| Asin | `Savory/CornedBeef_Held.png` | Kornet | ✅ slot topping |
| Asin | `Savory/MincedMeat_Held.png` | Daging | ✅ slot topping |
| Asin | `Savory/ChickenEgg_Held.png` | TelurAyam | ✅ slot topping |
| Asin | `Savory/DuckEgg_Held.png` | TelurBebek | ✅ slot topping |
| Asin | `Savory/GreenOnion_Held.png` | DaunBawang | ✅ slot topping |

### 5.5 `Assets/Screenshots` — tangkapan layar (tidak dipakai game)
`gameplay-background-car-check.png`, `gameplay-background-day.png`, `gameplay-background-live.png`, `gameplay-background-night.png`, `gameplay-background-pause.png`, dan 6 file `screenshot-20261029-*.png`. Total ±25 MB.

### 5.6 `Assets/Settings` — pengaturan render & input

| File | Fungsi |
|---|---|
| `UniversalRP.asset` | Pipeline URP |
| `Renderer2D.asset` | Renderer 2D |
| `UniversalRenderPipelineGlobalSettings.asset` | Pengaturan global URP |
| `DefaultVolumeProfile.asset` | Volume post-processing default |
| `InputSystem_Actions.inputactions` | Aksi Input System (terdaftar di Build Settings) |
| `Lit2DSceneTemplate.scenetemplate`, `Scenes/URP2DSceneTemplate.unity` | Template scene 2D |

### 5.7 Lainnya
- `Assets/TextMesh Pro/` — paket TMP Essentials; font yang dipakai: `LiberationSans SDF`.
- `Assets/Welcome/` — halaman Welcome dari template 2D Unity (`2d-template.png`, `Welcome2DScript.cs`, dll.).

---

## 6. Package & Pengaturan

| Package | Versi | Kegunaan |
|---|---|---|
| `com.unity.render-pipelines.universal` | 17.6.0 | URP 2D |
| `com.unity.ugui` | 2.6.0 | UI (Canvas, Button, Slider, TextMeshPro) |
| `com.unity.inputsystem` | 1.20.0 | Input baru (dipakai `ToppingHeldOnly`, EventSystem) |
| `com.unity.2d.animation` | 16.0.1 | Animasi 2D |
| `com.unity.2d.sprite` | 1.0.0 | Sprite Editor |
| `com.unity.2d.aseprite` / `psdimporter` | 6.0.0 / 15.0.1 | Import file Aseprite / PSD |
| `com.unity.2d.spriteshape`, `tilemap`, `tilemap.extras` | — | Belum dipakai |
| `com.unity.timeline` | 6.6.0 | Belum dipakai |
| `com.unity.visualscripting` | 1.9.12 | Belum dipakai |
| `com.unity.test-framework` | 1.8.0 | Belum ada test |
| `com.coplaydev.unity-mcp` | git `main` | Jembatan MCP Unity (alat AI) |
| `com.unity.collab-proxy` | 2.13.6 | Unity Version Control (Plastic) |

---

## 7. Temuan & Catatan

| # | Tingkat | Temuan |
|---|---|---|
| 1 | 🔴 Penting | **Topping tidak bisa diangkat di working copy.** Di commit terakhir (`4f89a24 toppings bisa diangkat`) GamePlay punya objek `SlotTopping` dan 10 `Slot<Nama>` yang terhubung ke `areaKlik`. Di versi sekarang (belum di-commit) objek-objek itu hilang, semua `areaKlik` kosong, sehingga `ToppingHeldOnly` melewati semua slot. Pulihkan dari commit tersebut atau buat ulang tray-nya. |
| 2 | 🔴 Penting | **Sprite logo hilang.** Image di `MainMenu › Logo` merujuk aset (guid `61da0592…`) yang tidak ada di project. |
| 3 | 🟠 Sedang | **Tidak ada jalan dari MainMenu ke GamePlay.** Satu-satunya `LoadScene` ada di `ConfirmHome()` (ke MainMenu). Belum ada tombol Main/Mulai. |
| 4 | 🟠 Sedang | `GameClock.realSecondsPerGameHour = 3` di scene (nilai testing). GDD menetapkan 45. |
| 5 | 🟡 Kecil | `SampleScene` (kosong) masih ada di Build Settings index 1. |
| 6 | 🟡 Kecil | `MartabakMenuRoot` di MainMenu adalah salinan lokal, bukan instance prefab — perubahan di `MartabakMenu.prefab` tidak ikut ke MainMenu. |
| 7 | 🟡 Kecil | Ada `Canvas` kosong bersarang di `Canvas › UI` (MainMenu). |
| 8 | 🟡 Kecil | Tombol di panel Jeda (`resumeButton`, `pauseSettingsButton`, `pauseHomeButton`) diisi tapi tidak diberi listener; `Card` panel Jeda juga disembunyikan oleh `ConfigurePauseOverlay()`. |
| 9 | 🟡 Kecil | Belum ada audio sama sekali (folder `Audio` kosong) — slider volume belum berefek. |
| 10 | 🟡 Kecil | `ToppingSlot.cs` tidak dipakai; namanya juga sama dengan kelas bersarang `ToppingHeldOnly.ToppingSlot` (tidak error, tapi membingungkan). |
| 11 | ⚪ Info | Aset tidak terpakai: `Gemini_Generated_Image_…-Photoroom.png`, 4 sprite Sprinkle/Spread, sprite `UI_Blank`, dan folder `Screenshots` (±25 MB). |
| 12 | ⚪ Info | Nama file `Maskot koki Pak Kumis yang galak.png` dipakai sebagai latar luar siang — nama file tidak menggambarkan isinya. |
| 13 | ⚪ Info | Latar lama `latar_t1_dalam 1.png`, `latar_t1_luar_siang/sore/malam.png` sudah dihapus di working copy (belum di-commit). |
| 14 | ⚪ Info | Folder kosong: `Animations`, `Audio`, `Materials`, `Resources`. |
