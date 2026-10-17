using UnityEngine;
using UnityEngine.UI;

// Mengganti latar luar dan ikon jam sesuai jam gim: siang -> sore -> malam.
public class BackgroundLuarSwitcher : MonoBehaviour
{
    [SerializeField] GameClock clock;
    [SerializeField] GameObject siang, sore, malam;
    [SerializeField] Image ikonJam;                            // opsional
    [SerializeField] Sprite ikonSiang, ikonSore, ikonMalam;    // opsional
    [SerializeField] float soreMulai = 15f;    // sore mulai sekitar jam 15.00
    [SerializeField] float malamMulai = 18f;   // matahari terbenam di Indonesia sekitar 17.30-18.00

    int phase = -1;   // 0 = siang, 1 = sore, 2 = malam

    void Update()
    {
        float h = clock.GameHour;
        int now = h < soreMulai ? 0 : h < malamMulai ? 1 : 2;
        if (now == phase) return;   // ganti hanya saat fase berubah
        phase = now;

        siang.SetActive(now == 0);
        sore.SetActive(now == 1);
        malam.SetActive(now == 2);

        if (ikonJam != null)
            ikonJam.sprite = now == 0 ? ikonSiang : now == 1 ? ikonSore : ikonMalam;
    }
}
