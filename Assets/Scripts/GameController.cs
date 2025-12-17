using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Texture2D defaultCursor;
    public Texture2D[] hoverCursorFrames;
    public float cursorFrameRate = 10f;
    public Vector2 hotspot = Vector2.zero;
    public GameObject inventory;

    private bool isHovering = false;
    private int currentFrame = 0;
    private float frameTimer = 0f;

    void Awake()
    {
        Instance = this;
        if (inventory != null)
            inventory.SetActive(false);
    }

    void Start()
    {
        SetDefaultCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }

        if (isHovering && hoverCursorFrames.Length > 0)
        {
            frameTimer += Time.deltaTime;
            if (frameTimer >= 1f / cursorFrameRate)
            {
                frameTimer = 0f;
                currentFrame = (currentFrame + 1) % hoverCursorFrames.Length;
                Cursor.SetCursor(hoverCursorFrames[currentFrame], hotspot, CursorMode.Auto);
            }
        }
    }

    public void ToggleInventory()
    {
        if (inventory != null)
            inventory.SetActive(!inventory.activeSelf);
    }

    public void CloseInventory()
    {
        if (inventory != null)
            inventory.SetActive(false);
    }

    public void SetDefaultCursor()
    {
        isHovering = false;
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }

    public void SetHoverCursor()
    {
        isHovering = true;
        currentFrame = 0;
        frameTimer = 0f;
        if (hoverCursorFrames.Length > 0)
        {
            Cursor.SetCursor(hoverCursorFrames[0], hotspot, CursorMode.Auto);
        }
    }
}
