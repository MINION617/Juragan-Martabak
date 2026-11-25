#if UNITY_EDITOR
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public static class MartabakMenuBuilder
{
    const string Art = "Assets/Art/MainMenu/";
    const string PrefabPath = "Assets/Prefabs/UI/MartabakMenu.prefab";
    static readonly Color Cream = new Color(1f, .93f, .76f, 1f);
    static readonly Color Brown = new Color(.31f, .13f, .065f, 1f);
    static readonly Color Olive = new Color(.42f, .47f, .22f, 1f);

    static Sprite Sprite(string name)
    {
        return AssetDatabase.LoadAllAssetsAtPath(Art + "Lembar sprite tombol martabak enam ikon.png").OfType<Sprite>().First(s => s.name == name);
    }

    static RectTransform Rect(string name, Transform parent, Vector2 anchor, Vector2 pivot, Vector2 position, Vector2 size)
    {
        var go = new GameObject(name, typeof(RectTransform));
        go.layer = 5;
        go.transform.SetParent(parent, false);
        var rt = (RectTransform)go.transform;
        rt.anchorMin = rt.anchorMax = anchor;
        rt.pivot = pivot;
        rt.anchoredPosition = position;
        rt.sizeDelta = size;
        return rt;
    }

    static RectTransform Stretch(string name, Transform parent)
    {
        var rt = Rect(name, parent, Vector2.zero, new Vector2(.5f, .5f), Vector2.zero, Vector2.zero);
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
        return rt;
    }

    static Image Image(RectTransform rt, Sprite sprite, Color color, bool raycast)
    {
        rt.gameObject.AddComponent<CanvasRenderer>();
        var img = rt.gameObject.AddComponent<Image>();
        img.sprite = sprite;
        img.color = color;
        img.preserveAspect = sprite != null;
        img.raycastTarget = raycast;
        return img;
    }

    static Button Icon(string name, Transform parent, Sprite sprite, Vector2 anchor, Vector2 pivot, Vector2 position, Vector2 size)
    {
        var rt = Rect(name, parent, anchor, pivot, position, size);
        var img = Image(rt, sprite, Color.white, true);
        var button = rt.gameObject.AddComponent<Button>();
        button.targetGraphic = img;
        button.transition = Selectable.Transition.ColorTint;
        return button;
    }

    static TextMeshProUGUI Text(string name, Transform parent, string value, int size, Color color, Vector2 position, Vector2 dimensions)
    {
        var rt = Rect(name, parent, new Vector2(.5f,.5f), new Vector2(.5f,.5f), position, dimensions);
        rt.gameObject.AddComponent<CanvasRenderer>();
        var label = rt.gameObject.AddComponent<TextMeshProUGUI>();
        label.text = value;
        label.fontSize = size;
        label.color = color;
        label.alignment = TextAlignmentOptions.Center;
        label.raycastTarget = false;
        return label;
    }

    static Button LabelButton(string name, Transform parent, string label, Vector2 position, Vector2 size)
    {
        var button = Icon(name, parent, AssetDatabase.LoadAssetAtPath<Sprite>(Art + "Panel tombol kosong hijau zaitun-3.png"),
            new Vector2(.5f,.5f), new Vector2(.5f,.5f), position, size);
        Text("Label", button.transform, label, 29, Cream, Vector2.zero, size * .8f);
        return button;
    }

    static RectTransform Panel(string name, Transform root, string title, out RectTransform card)
    {
        var panel = Stretch(name, root);
        Image(panel, null, new Color(.08f,.035f,.015f,.72f), true);
        card = Rect("Card", panel, new Vector2(.5f,.5f), new Vector2(.5f,.5f), Vector2.zero, new Vector2(640,480));
        Image(card, null, Cream, true);
        Text("Title", card, title, 46, Brown, new Vector2(0,180), new Vector2(560,65));
        panel.gameObject.SetActive(false);
        return panel;
    }

    static Slider VolumeSlider(string name, Transform parent, string label, float y)
    {
        Text(name + "Label", parent, label, 30, Brown, new Vector2(-175,y+38), new Vector2(270,50));
        var rt = Rect(name, parent, new Vector2(.5f,.5f), new Vector2(.5f,.5f), new Vector2(0,y), new Vector2(440,32));
        var bg = Stretch("Background", rt);
        Image(bg, null, new Color(.57f,.46f,.31f,1), false);
        var fillArea = Stretch("Fill Area", rt);
        fillArea.offsetMin = new Vector2(12,0);
        fillArea.offsetMax = new Vector2(-12,0);
        var fill = Stretch("Fill", fillArea);
        Image(fill, null, Olive, false);
        var handleArea = Stretch("Handle Slide Area", rt);
        handleArea.offsetMin = new Vector2(12,0);
        handleArea.offsetMax = new Vector2(-12,0);
        var handle = Rect("Handle", handleArea, new Vector2(.5f,.5f), new Vector2(.5f,.5f), Vector2.zero, new Vector2(36,46));
        Image(handle, null, Brown, true);
        var slider = rt.gameObject.AddComponent<Slider>();
        slider.fillRect = fill;
        slider.handleRect = handle;
        slider.targetGraphic = handle.GetComponent<Image>();
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = 1;
        return slider;
    }

    static GameObject BuildRoot()
    {
        var root = new GameObject("MartabakMenuRoot", typeof(RectTransform), typeof(MartabakMenuUI));
        root.layer = 5;
        var rect = (RectTransform)root.transform;
        rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one;
        rect.offsetMin = rect.offsetMax = Vector2.zero;
        var ui = root.GetComponent<MartabakMenuUI>();
        ui.menuButton = Icon("MenuButton", root.transform, Sprite("UI_Menu"), new Vector2(1,1), new Vector2(1,1), new Vector2(-36,-36), new Vector2(88,88));
        var dropdown = Rect("Dropdown", root.transform, new Vector2(1,1), new Vector2(1,1), new Vector2(-36,-136), new Vector2(88,288));
        ui.dropdown = dropdown.gameObject;
        ui.settingsButton = Icon("SettingsButton", dropdown, Sprite("UI_Settings"), new Vector2(0,1), new Vector2(0,1), Vector2.zero, new Vector2(88,88));
        ui.homeButton = Icon("HomeButton", dropdown, Sprite("UI_Home"), new Vector2(0,1), new Vector2(0,1), new Vector2(0,-100), new Vector2(88,88));
        ui.shopButton = Icon("ShopButton", dropdown, Sprite("UI_Shop"), new Vector2(0,1), new Vector2(0,1), new Vector2(0,-200), new Vector2(88,88));
        dropdown.gameObject.SetActive(false);
        ui.pauseButton = Icon("PauseButton", root.transform, Sprite("UI_Pause"), new Vector2(1,1), new Vector2(1,1), new Vector2(-36,-36), new Vector2(88,88));
        ui.mainShopButton = Icon("MainShopButton", root.transform,
            AssetDatabase.LoadAssetAtPath<Sprite>(Art+"Tombol Toko Pastel Juragan Martabak-2.png"),
            new Vector2(1,0), new Vector2(1,0), new Vector2(-54,50), new Vector2(330,110));
        var shopIcon = Rect("MainShopIllustration", ui.mainShopButton.transform,
            new Vector2(.5f,1f), new Vector2(.5f,0f), new Vector2(0,12), new Vector2(330,350));
        shopIcon.anchorMin = new Vector2(0,1);
        shopIcon.anchorMax = new Vector2(1,1);
        shopIcon.sizeDelta = new Vector2(0,350);
        var shopIconImage = Image(shopIcon, AssetDatabase.LoadAllAssetsAtPath(Art+"Ikon toko martabak mungil-1.png")
            .OfType<Sprite>().First(s => s.name == "Ikon toko martabak mungil-1_0"), Color.white, true);
        shopIcon.gameObject.AddComponent<Button>().targetGraphic = shopIconImage;

        RectTransform card;
        var settings = Panel("SettingsPanel", root.transform, "Pengaturan", out card);
        ui.settingsPanel = settings.gameObject;
        ui.musicSlider = VolumeSlider("MusicSlider", card, "Musik", 75);
        ui.sfxSlider = VolumeSlider("SfxSlider", card, "Efek Suara", -45);
        ui.settingsCloseButton = LabelButton("SettingsCloseButton", card, "Tutup", new Vector2(0,-175), new Vector2(220,75));

        var shop = Panel("ShopPanel", root.transform, "Toko", out card);
        ui.shopPanel = shop.gameObject;
        var illustration = Rect("WarungIllustration", card, new Vector2(.5f,.5f), new Vector2(.5f,.5f), new Vector2(0,35), new Vector2(190,190));
        Image(illustration, AssetDatabase.LoadAssetAtPath<Sprite>(Art+"Ikon toko martabak mungil-1.png"), Color.white, false);
        Text("EmptyMessage", card, "Barang belum tersedia", 28, Brown, new Vector2(0,-105), new Vector2(530,55));
        ui.shopCloseButton = LabelButton("ShopCloseButton", card, "Tutup", new Vector2(0,-178), new Vector2(220,75));

        var pause = Panel("PausePanel", root.transform, "Jeda", out card);
        ui.pausePanel = pause.gameObject;
        ui.resumeButton = LabelButton("ResumeButton", card, "Lanjutkan", new Vector2(0,70), new Vector2(300,75));
        ui.pauseSettingsButton = LabelButton("PauseSettingsButton", card, "Settings", new Vector2(0,-35), new Vector2(300,75));
        ui.pauseHomeButton = LabelButton("PauseHomeButton", card, "Home", new Vector2(0,-140), new Vector2(300,75));

        var confirm = Panel("HomeConfirmPanel", root.transform, "Kembali ke menu utama?", out card);
        ui.homeConfirmPanel = confirm.gameObject;
        ui.homeYesButton = LabelButton("HomeYesButton", card, "Ya", new Vector2(-130,-55), new Vector2(220,75));
        ui.homeNoButton = LabelButton("HomeNoButton", card, "Batal", new Vector2(130,-55), new Vector2(220,75));
        return root;
    }

    public static string Build()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Prefabs")) AssetDatabase.CreateFolder("Assets","Prefabs");
        if (!AssetDatabase.IsValidFolder("Assets/Prefabs/UI")) AssetDatabase.CreateFolder("Assets/Prefabs","UI");
        var menuScene = EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
        var canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var scaler = canvas.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920,1080);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = .5f;
        var old = canvas.transform.Find("MartabakMenuRoot");
        var root = old ? old.gameObject : BuildRoot();
        root.transform.SetParent(canvas.transform, false);
        root.transform.SetAsLastSibling();
        var ui = root.GetComponent<MartabakMenuUI>();
        ui.isMainMenu = true;
        ui.pauseButton.gameObject.SetActive(false);
        ui.mainShopButton.gameObject.SetActive(true);
        PrefabUtility.SaveAsPrefabAsset(root, PrefabPath);
        EditorUtility.SetDirty(root);
        EditorSceneManager.MarkSceneDirty(menuScene);
        EditorSceneManager.SaveScene(menuScene);

        var gameScene = EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        scaler = canvas.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920,1080);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = .5f;
        old = canvas.transform.Find("MartabakMenuRoot");
        var gameRoot = old ? old.gameObject : (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath), gameScene);
        gameRoot.transform.SetParent(canvas.transform, false);
        gameRoot.transform.SetAsLastSibling();
        ui = gameRoot.GetComponent<MartabakMenuUI>();
        ui.isMainMenu = false;
        ui.pauseButton.gameObject.SetActive(false);
        ui.mainShopButton.gameObject.SetActive(false);
        EditorUtility.SetDirty(gameRoot);
        EditorSceneManager.MarkSceneDirty(gameScene);
        EditorSceneManager.SaveScene(gameScene);

        var scenes = EditorBuildSettings.scenes.ToList();
        if (!scenes.Any(s=>s.path=="Assets/Scenes/GamePlay.unity")) scenes.Add(new EditorBuildSettingsScene("Assets/Scenes/GamePlay.unity", true));
        EditorBuildSettings.scenes = scenes.ToArray();
        EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
        return "Built menu prefab and updated MainMenu/GamePlay scenes";
    }
}
#endif
