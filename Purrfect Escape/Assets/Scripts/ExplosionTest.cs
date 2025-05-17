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
        gameObject.SetActive(false);
        Invoke("DisablePhysics", explosionDuration);
    }

    void CreatePieces()
    {
        float pieceWidth = originalSpriteRenderer.bounds.size.x / gridWidth;
        float pieceHeight = originalSpriteRenderer.bounds.size.y / gridHeight;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(transform.position.x + (x * pieceWidth), transform.position.y + (y * pieceHeight), transform.position.z);
                CreatePiece(position, pieceWidth, pieceHeight);
            }
        }
    }

    void CreatePiece(Vector3 position, float width, float height)
    {
        GameObject piece = new GameObject("Piece");
        piece.transform.SetParent(transform);
        SpriteRenderer pieceRenderer = piece.AddComponent<SpriteRenderer>();
        pieceRenderer.sprite = originalSpriteRenderer.sprite;
        piece.transform.position = position;
        piece.transform.localScale = new Vector3(width / originalSpriteRenderer.sprite.bounds.size.x, height / originalSpriteRenderer.sprite.bounds.size.y, 1);

        Rigidbody2D rb = piece.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        BoxCollider2D collider = piece.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(width, height);

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

        Destroy(piece, 5f);
    }

    void DisablePhysics()
    {
        Rigidbody2D[] pieces = Object.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);
        foreach (var piece in pieces)
        {
            piece.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}