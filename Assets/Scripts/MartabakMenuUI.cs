using UnityEngine;
using UnityEngine.SceneManagement;

public class MartabakMenuUI : MonoBehaviour
{
    [Header("Scene dan tombol")]
    [SerializeField] public string mainMenuScene = "MainMenu";
    [SerializeField] public bool isMainMenu;
    [SerializeField] public UnityEngine.UI.Button menuButton;
    [SerializeField] public UnityEngine.UI.Button settingsButton;
    [SerializeField] public UnityEngine.UI.Button homeButton;
    [SerializeField] public UnityEngine.UI.Button shopButton;
    [SerializeField] public UnityEngine.UI.Button pauseButton;
    [SerializeField] public UnityEngine.UI.Button mainShopButton;
    [SerializeField] public GameObject dropdown;

    [Header("Panel")]
    [SerializeField] public GameObject settingsPanel;
    [SerializeField] public GameObject shopPanel;
    [SerializeField] public GameObject pausePanel;
    [SerializeField] public GameObject homeConfirmPanel;
    [SerializeField] public UnityEngine.UI.Button settingsCloseButton;
    [SerializeField] public UnityEngine.UI.Button shopCloseButton;
    [SerializeField] public UnityEngine.UI.Button resumeButton;
    [SerializeField] public UnityEngine.UI.Button pauseSettingsButton;
    [SerializeField] public UnityEngine.UI.Button pauseHomeButton;
    [SerializeField] public UnityEngine.UI.Button homeYesButton;
    [SerializeField] public UnityEngine.UI.Button homeNoButton;
    [SerializeField] public UnityEngine.UI.Slider musicSlider;
    [SerializeField] public UnityEngine.UI.Slider sfxSlider;

    bool menuPausedGame;
    float timeScaleBeforePause;
    UnityEngine.UI.Button mainShopIllustrationButton;
    const string MusicKey = "Martabak_MusicVolume";
    const string SfxKey = "Martabak_SfxVolume";
    public static float MusicVolume => PlayerPrefs.GetFloat(MusicKey, 1f);
    public static float SfxVolume => PlayerPrefs.GetFloat(SfxKey, 1f);

    void Awake()
    {
        if (dropdown) dropdown.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (shopPanel) shopPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
        if (homeConfirmPanel) homeConfirmPanel.SetActive(false);
        ConfigurePauseOverlay();
        if (menuButton) menuButton.onClick.AddListener(ToggleMenu);
        if (settingsButton) settingsButton.onClick.AddListener(OpenSettingsFromMenu);
        if (homeButton) homeButton.onClick.AddListener(HomeFromMenu);
        if (shopButton) shopButton.onClick.AddListener(OpenShop);
        if (mainShopButton) mainShopButton.onClick.AddListener(OpenShop);
        if (mainShopButton)
        {
            var illustration = mainShopButton.transform.Find("MainShopIllustration");
            if (illustration) mainShopIllustrationButton = illustration.GetComponent<UnityEngine.UI.Button>();
            if (mainShopIllustrationButton) mainShopIllustrationButton.onClick.AddListener(OpenShop);
        }
        if (settingsCloseButton) settingsCloseButton.onClick.AddListener(CloseSettings);
        if (shopCloseButton) shopCloseButton.onClick.AddListener(CloseShop);
        if (homeYesButton) homeYesButton.onClick.AddListener(ConfirmHome);
        if (homeNoButton) homeNoButton.onClick.AddListener(CancelHome);
        if (musicSlider)
        {
            musicSlider.SetValueWithoutNotify(MusicVolume);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        if (sfxSlider)
        {
            sfxSlider.SetValueWithoutNotify(SfxVolume);
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }
        ApplyVolumes();
    }

    void OnDestroy()
    {
        if (menuButton) menuButton.onClick.RemoveListener(ToggleMenu);
        if (settingsButton) settingsButton.onClick.RemoveListener(OpenSettingsFromMenu);
        if (homeButton) homeButton.onClick.RemoveListener(HomeFromMenu);
        if (shopButton) shopButton.onClick.RemoveListener(OpenShop);
        if (mainShopButton) mainShopButton.onClick.RemoveListener(OpenShop);
        if (mainShopIllustrationButton) mainShopIllustrationButton.onClick.RemoveListener(OpenShop);
        if (settingsCloseButton) settingsCloseButton.onClick.RemoveListener(CloseSettings);
        if (shopCloseButton) shopCloseButton.onClick.RemoveListener(CloseShop);
        if (homeYesButton) homeYesButton.onClick.RemoveListener(ConfirmHome);
        if (homeNoButton) homeNoButton.onClick.RemoveListener(CancelHome);
        if (musicSlider) musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        if (sfxSlider) sfxSlider.onValueChanged.RemoveListener(SetSfxVolume);
        if (menuPausedGame) Time.timeScale = timeScaleBeforePause;
    }

    public void ToggleMenu()
    {
        if (isMainMenu)
        {
            if (dropdown) dropdown.SetActive(!dropdown.activeSelf);
            return;
        }

        if (menuPausedGame) Resume();
        else Pause();
    }

    public void OpenSettingsFromMenu()
    {
        if (dropdown) dropdown.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(true);
    }

    public void OpenSettingsFromPause()
    {
        OpenSettingsFromMenu();
    }

    public void CloseSettings()
    {
        if (settingsPanel) settingsPanel.SetActive(false);
        if (menuPausedGame && dropdown) dropdown.SetActive(true);
    }

    public void OpenShop()
    {
        if (dropdown) dropdown.SetActive(false);
        if (shopPanel) shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        if (shopPanel) shopPanel.SetActive(false);
        if (menuPausedGame && dropdown) dropdown.SetActive(true);
    }

    public void Pause()
    {
        if (isMainMenu || menuPausedGame) return;
        timeScaleBeforePause = Time.timeScale;
        menuPausedGame = true;
        if (pausePanel) pausePanel.SetActive(true);
        if (dropdown) dropdown.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        if (dropdown) dropdown.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (shopPanel) shopPanel.SetActive(false);
        if (homeConfirmPanel) homeConfirmPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
        if (menuPausedGame) Time.timeScale = timeScaleBeforePause;
        menuPausedGame = false;
    }

    public void HomeFromMenu()
    {
        if (dropdown) dropdown.SetActive(false);
        if (isMainMenu) return;
        if (homeConfirmPanel) homeConfirmPanel.SetActive(true);
    }

    public void HomeFromPause()
    {
        HomeFromMenu();
    }

    public void CancelHome()
    {
        if (homeConfirmPanel) homeConfirmPanel.SetActive(false);
        if (menuPausedGame && dropdown) dropdown.SetActive(true);
    }

    public void ConfirmHome()
    {
        if (menuPausedGame) Time.timeScale = timeScaleBeforePause;
        menuPausedGame = false;
        SceneManager.LoadScene(mainMenuScene);
    }

    void ConfigurePauseOverlay()
    {
        if (pauseButton) pauseButton.gameObject.SetActive(false);
        if (!pausePanel) return;

        // The overlay must draw below the hamburger and its dropdown, but above gameplay.
        pausePanel.transform.SetAsFirstSibling();
        var backdrop = pausePanel.GetComponent<UnityEngine.UI.Image>();
        if (backdrop)
        {
            backdrop.color = new Color(.35f, .35f, .35f, .55f);
            backdrop.raycastTarget = true;
        }

        var card = pausePanel.transform.Find("Card");
        if (card) card.gameObject.SetActive(false);

        // Reuse the existing pause artwork as a non-interactive centered icon.
        if (pauseButton)
        {
            pauseButton.enabled = false;
            var icon = pauseButton.GetComponent<UnityEngine.UI.Image>();
            if (icon)
            {
                icon.color = Color.white;
                icon.raycastTarget = false;
            }
            var rect = pauseButton.GetComponent<RectTransform>();
            rect.SetParent(pausePanel.transform, false);
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(.5f, .5f);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = new Vector2(110f, 110f);
            pauseButton.gameObject.SetActive(true);
        }
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MusicKey, value);
        PlayerPrefs.Save();
        ApplyVolumes();
    }

    public void SetSfxVolume(float value)
    {
        PlayerPrefs.SetFloat(SfxKey, value);
        PlayerPrefs.Save();
        ApplyVolumes();
    }

    void ApplyVolumes()
    {
        // Project ini belum memiliki AudioMixer atau AudioSource. Sumber audio yang
        // ditambahkan kelak dapat memakai dua nilai tersimpan ini.
        foreach (var source in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            bool music = source.loop || source.name.ToLowerInvariant().Contains("music");
            source.volume = music ? MusicVolume : SfxVolume;
        }
    }
}
