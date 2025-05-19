using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    public int gridWidth = 4;
    public int gridHeight = 4;
    public float bounceStrength = 5f;
    public float sidewaysStrength = 1f;
    public float explosionDuration = 1f;

    private bool exploded = false;
    private SpriteRenderer originalSpriteRenderer;

    void Start()
    {
        originalSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !exploded)
        {
            Explode();
        }
    }

    void Explode()
    {
        exploded = true;
        CreatePieces();
        gameObject.SetActive(false); // Hide the original lamp
        Invoke("DisablePhysics", explosionDuration);
    }

    void CreatePieces()
    {
        Texture2D originalTexture = originalSpriteRenderer.sprite.texture;
        Rect spriteRect = originalSpriteRenderer.sprite.textureRect;
        float pixelsPerUnit = originalSpriteRenderer.sprite.pixelsPerUnit;

        int piecePixelWidth = Mathf.RoundToInt(spriteRect.width / gridWidth);
        int piecePixelHeight = Mathf.RoundToInt(spriteRect.height / gridHeight);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                int pixelX = Mathf.RoundToInt(spriteRect.x + x * piecePixelWidth);
                int pixelY = Mathf.RoundToInt(spriteRect.y + y * piecePixelHeight);

                Color[] pixelBlock = originalTexture.GetPixels(pixelX, pixelY, piecePixelWidth, piecePixelHeight);

                Texture2D pieceTexture = new Texture2D(piecePixelWidth, piecePixelHeight);
                pieceTexture.SetPixels(pixelBlock);
                pieceTexture.Apply();

                Sprite pieceSprite = Sprite.Create(pieceTexture, new Rect(0, 0, piecePixelWidth, piecePixelHeight), new Vector2(0.5f, 0.5f), pixelsPerUnit);

                float worldX = ((float)piecePixelWidth / pixelsPerUnit) * (x - gridWidth / 2f + 0.5f);
                float worldY = ((float)piecePixelHeight / pixelsPerUnit) * (y - gridHeight / 2f + 0.5f);
                Vector3 piecePosition = transform.position + new Vector3(worldX, worldY, 0f);

                CreatePiece(piecePosition, pieceSprite);
            }
        }
    }

    void CreatePiece(Vector3 position, Sprite pieceSprite)
    {
        GameObject piece = new GameObject("Piece");
        piece.transform.position = position;

        SpriteRenderer pieceRenderer = piece.AddComponent<SpriteRenderer>();
        pieceRenderer.sprite = pieceSprite;
        pieceRenderer.sortingOrder = originalSpriteRenderer.sortingOrder;

        Rigidbody2D rb = piece.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        BoxCollider2D collider = piece.AddComponent<BoxCollider2D>();
        collider.size = pieceSprite.bounds.size;

        int explosionLayer = LayerMask.NameToLayer("ExplosionPieces");
        if (explosionLayer == -1)
        {
            Debug.LogError("Layer 'ExplosionPieces' not found. Please create this layer in the Tags & Layers section of the Project Settings.");
            return;
        }

        piece.layer = explosionLayer;

        Physics2D.IgnoreLayerCollision(explosionLayer, LayerMask.NameToLayer("Cat"), true);

        float randomX = Random.Range(-sidewaysStrength, sidewaysStrength);
        float randomY = Random.Range(0.5f, bounceStrength);
        rb.linearVelocity = new Vector2(randomX, randomY);

        Destroy(piece, 5f); // Optional: Clean up after 5 seconds
    }

    void DisablePhysics()
    {
        Rigidbody2D[] pieces = Object.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);
        foreach (var piece in pieces)
        {
            piece.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}