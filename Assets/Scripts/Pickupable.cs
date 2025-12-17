using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Pickupable : MonoBehaviour
{
    public WeaponData weaponData;
    [SerializeField] private float bobAmount = 0.1f;
    [SerializeField] private float bobSpeed = 2f;
    
    private SpriteRenderer spriteRenderer;
    private Vector3 startPos;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Player";
        
        var collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        
        if (weaponData != null && weaponData.icon != null)
        {
            spriteRenderer.sprite = weaponData.icon;
        }
    }

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    void OnValidate()
    {
        // Update sprite in editor when weaponData changes
        if (weaponData != null && weaponData.icon != null)
        {
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null) sr.sprite = weaponData.icon;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventoryController.Instance.AddItem(weaponData))
            {
                Destroy(gameObject);
            }
        }
    }
}

