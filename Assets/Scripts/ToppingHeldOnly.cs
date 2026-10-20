using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Prototype interaksi topping: klik area tray -> sprite Held mengikuti mouse.
/// Belum ada sistem tabur, menaruh, memasak, ataupun mengocok.
/// Pasang pada GameObject GameManager atau GameObject kosong di scene.
/// </summary>
public class ToppingHeldOnly : MonoBehaviour
{
    [Serializable]
    public class ToppingSlot
    {
        public string nama;
        public RectTransform areaKlik;
        public Sprite spriteHeld;
    }

    [Header("Referensi")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<ToppingSlot> toppingSlots = new List<ToppingSlot>();

    [Header("Tampilan Held")]
    [SerializeField, Min(1f)] private float ukuranHeld = 96f;
    [SerializeField] private Vector2 offsetKursor = new Vector2(35f, 35f);

    private RectTransform heldRect;
    private Image heldImage;
    private RectTransform canvasRect;
    private Camera canvasCamera;
    private bool sedangMemegang;

    private void Awake()
    {
        if (canvas == null) canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("[ToppingHeldOnly] Canvas belum diisi di Inspector.", this);
            enabled = false;
            return;
        }

        canvasRect = canvas.transform as RectTransform;
        canvasCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        var objekHeld = new GameObject("HeldTopping_Runtime", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        objekHeld.transform.SetParent(canvas.transform, false);
        heldRect = objekHeld.GetComponent<RectTransform>();
        heldRect.anchorMin = new Vector2(0.5f, 0.5f);
        heldRect.anchorMax = new Vector2(0.5f, 0.5f);
        heldRect.pivot = new Vector2(0.5f, 0.5f);
        heldRect.sizeDelta = new Vector2(ukuranHeld, ukuranHeld);
        heldImage = objekHeld.GetComponent<Image>();
        heldImage.preserveAspect = true;
        heldImage.raycastTarget = false; // Jangan memblokir klik pada tray.
        objekHeld.SetActive(false);
    }

    private void Update()
    {
        if (BatalDitekan())
        {
            BatalkanHeld();
            return;
        }

        Vector2 mouse = PosisiMouse();
        if (KlikKiriDitekan())
        {
            // Dibaca mundur supaya slot terakhir pada daftar menjadi prioritas jika overlap.
            for (int i = toppingSlots.Count - 1; i >= 0; i--)
            {
                ToppingSlot slot = toppingSlots[i];
                if (slot == null || slot.areaKlik == null || slot.spriteHeld == null) continue;

                if (RectTransformUtility.RectangleContainsScreenPoint(slot.areaKlik, mouse, canvasCamera))
                {
                    PilihTopping(slot);
                    break;
                }
            }
        }

        if (sedangMemegang && heldRect != null &&
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mouse, canvasCamera, out Vector2 lokal))
        {
            heldRect.anchoredPosition = lokal + offsetKursor;
        }
    }

    private void PilihTopping(ToppingSlot slot)
    {
        heldImage.sprite = slot.spriteHeld;
        heldRect.sizeDelta = new Vector2(ukuranHeld, ukuranHeld);
        heldRect.SetAsLastSibling();
        heldRect.gameObject.SetActive(true);
        sedangMemegang = true;
        Debug.Log("[ToppingHeldOnly] Dipilih: " + slot.nama, this);
    }

    public void BatalkanHeld()
    {
        sedangMemegang = false;
        if (heldRect != null) heldRect.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (heldRect != null) Destroy(heldRect.gameObject);
    }

    private static Vector2 PosisiMouse()
    {
#if ENABLE_INPUT_SYSTEM
        return Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;
#else
        return Input.mousePosition;
#endif
    }

    private static bool KlikKiriDitekan()
    {
#if ENABLE_INPUT_SYSTEM
        return Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
#else
        return Input.GetMouseButtonDown(0);
#endif
    }

    private static bool BatalDitekan()
    {
#if ENABLE_INPUT_SYSTEM
        return (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame) ||
               (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame);
#else
        return Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1);
#endif
    }
}
