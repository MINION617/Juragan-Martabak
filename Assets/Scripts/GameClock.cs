using UnityEngine;
using TMPro;

// Jam gim: 10.00-21.00, 1 jam gim = 45 detik nyata (GDD). Tampil seperti "5:45 PM".
public class GameClock : MonoBehaviour
{
    [SerializeField] TMP_Text clockText;
    [SerializeField] TMP_Text levelText;                    // opsional: tampil "Level 1"
    [SerializeField] int level = 1;                         // nomor level yang ditampilkan
    [SerializeField] float realSecondsPerGameHour = 45f;    // turunkan (mis. 3) saat testing
    [SerializeField] float startHour = 10f;
    [SerializeField] float endHour = 21f;
    [SerializeField] int minutesPerStep = 15;               // jam bergeser per 15 menit gim (1 = per menit)

    float elapsed;

    public bool IsOpen { get; private set; } = true;
    public float GameHour => startHour + elapsed / realSecondsPerGameHour;

    void Start()
    {
        if (levelText != null) levelText.text = $"Level {level}";
    }

    void Update()
    {
        if (!IsOpen) return;

        elapsed += Time.deltaTime;

        if (GameHour >= endHour)
        {
            elapsed = (endHour - startHour) * realSecondsPerGameHour;
            IsOpen = false;
            Debug.Log("Warung tutup");
        }

        clockText.text = FormatTime(GameHour);
    }

    string FormatTime(float hour)
    {
        int step = Mathf.Max(1, minutesPerStep);
        int total = Mathf.FloorToInt(hour * 60f / step) * step;   // menit sejak 00.00
        int h24 = total / 60;
        int m = total % 60;
        int h12 = h24 % 12 == 0 ? 12 : h24 % 12;
        string ampm = h24 >= 12 ? "PM" : "AM";
        return $"{h12}:{m:00} {ampm}";
    }
}
