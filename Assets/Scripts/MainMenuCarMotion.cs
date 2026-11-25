using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public sealed class MainMenuCarMotion : MonoBehaviour
{
    [Header("Empat frame dari sprite sheet mobil")]
    // Roda bergerak berurutan: kiri atas, kanan atas, kiri bawah, kanan bawah.
    [SerializeField] Sprite[] frames = new Sprite[4];
    [SerializeField, Min(0.1f)] float framesPerSecond = 8f;

    [Header("Perjalanan di jalan Main Menu")]
    [SerializeField, Min(1f)] float speed = 350f;
    [SerializeField, Min(0f)] float passDelay = 5f;
    [SerializeField] float roadBottomY = 35f;
    [SerializeField] Vector2 carSize = new Vector2(808f, 490f);
    [SerializeField, Min(0f)] float leftOutsideMargin = 40f;
    [SerializeField, Min(0f)] float rightOutsideMargin = 40f;
    [SerializeField] bool startsMovingRight = true;
    [SerializeField] bool flipX;

    Image carImage;
    RectTransform carRect;
    RectTransform canvasRect;
    int direction;
    bool waitingForPass;
    bool hasCompletedPass;
    float waitRemaining;
    float animationTime;

    void Awake()
    {
        carImage = GetComponent<Image>();
        carRect = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>()?.rootCanvas.GetComponent<RectTransform>();
        carImage.raycastTarget = false;
    }

    void OnEnable()
    {
        if (!carRect) carRect = GetComponent<RectTransform>();
        if (!carImage) carImage = GetComponent<Image>();
        if (!canvasRect) canvasRect = GetComponentInParent<Canvas>()?.rootCanvas.GetComponent<RectTransform>();
        direction = startsMovingRight ? 1 : -1;
        waitingForPass = true;
        hasCompletedPass = false;
        waitRemaining = passDelay;
        animationTime = 0f;
        ApplySizeAndDirection();
        SetHorizontalPosition(direction > 0 ? LeftLimit() : RightLimit());
        ShowFrame(0);
    }

    void Update()
    {
        if (frames == null || frames.Length == 0 || !canvasRect) return;

        animationTime += Time.unscaledDeltaTime;
        ShowFrame(Mathf.FloorToInt(animationTime * framesPerSecond) % frames.Length);

        if (waitingForPass)
        {
            waitRemaining -= Time.unscaledDeltaTime;
            if (waitRemaining > 0f) return;
            if (hasCompletedPass)
            {
                direction = -direction;
                ApplySizeAndDirection();
            }
            SetHorizontalPosition(direction > 0 ? LeftLimit() : RightLimit());
            waitingForPass = false;
        }

        float x = carRect.anchoredPosition.x + direction * speed * Time.unscaledDeltaTime;
        float limit = direction > 0 ? RightLimit() : LeftLimit();
        if ((direction > 0 && x >= limit) || (direction < 0 && x <= limit))
        {
            x = limit;
            waitingForPass = true;
            hasCompletedPass = true;
            waitRemaining = passDelay;
        }
        SetHorizontalPosition(x);
    }

    void ApplySizeAndDirection()
    {
        carRect.sizeDelta = new Vector2(Mathf.Max(1f, carSize.x), Mathf.Max(1f, carSize.y));
        carRect.anchoredPosition = new Vector2(carRect.anchoredPosition.x, roadBottomY);
        // UI Image has no SpriteRenderer.flipX; a negative X scale mirrors the sprite.
        flipX = direction < 0;
        carRect.localScale = new Vector3(flipX ? -1f : 1f, 1f, 1f);
    }

    float LeftLimit() => -canvasRect.rect.width * .5f - carRect.rect.width * .5f - leftOutsideMargin;
    float RightLimit() => canvasRect.rect.width * .5f + carRect.rect.width * .5f + rightOutsideMargin;

    void SetHorizontalPosition(float x)
    {
        carRect.anchoredPosition = new Vector2(x, roadBottomY);
    }

    void ShowFrame(int index)
    {
        if (frames == null || frames.Length == 0) return;
        carImage.sprite = frames[index];
    }

    void OnValidate()
    {
        speed = Mathf.Max(1f, speed);
        framesPerSecond = Mathf.Max(.1f, framesPerSecond);
        passDelay = Mathf.Max(0f, passDelay);
        carSize.x = Mathf.Max(1f, carSize.x);
        carSize.y = Mathf.Max(1f, carSize.y);
        leftOutsideMargin = Mathf.Max(0f, leftOutsideMargin);
        rightOutsideMargin = Mathf.Max(0f, rightOutsideMargin);
    }
}
