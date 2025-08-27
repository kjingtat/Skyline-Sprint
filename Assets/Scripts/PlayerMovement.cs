using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 5f;

    [Header("Duck Settings")]
    public Vector2 duckColliderSize = new Vector2(1f, 1f);
    public Vector2 duckColliderOffset = new Vector2(1f, -1f);
    public Vector3 slideOffset = new Vector3(0, -0.3f, 0);
    public float standCheckPadding = 0.02f;

    [Header("Walk Settings")]
    public float walkForce = 1f;

    [Header("Scroll Settings")]
    public float scrollSpeed = 2f;      
    public float forwardBoost = 1.5f;   
    public float backwardPenalty = 0.5f;

    [Header("References")]
    public Transform graphics;

    [HideInInspector] public bool controlsEnabled = true;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator anim;

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private Vector3 originalGraphicsPos;

    private int JumpCount = 0;
    private int MaxJump = 2;
    private bool isJumping;
    private bool isDucking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        Transform graphicsTransform = transform.Find("Graphics");
        graphics = graphicsTransform;
        anim = graphics.GetComponent<Animator>();

        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;
        originalGraphicsPos = graphics.localPosition;
    }

    void Update()
    {
        if (!controlsEnabled) return; 

        HandleJump();
        HandleDuck();
        HandleLateral();
        UpdateAnimator();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            JumpCount = 0;
            isJumping = false;
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && JumpCount < MaxJump && !isDucking)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            JumpCount++;
            isJumping = true;
        }
    }

    void HandleDuck()
    {
        if ((Input.GetKey(KeyCode.S) && JumpCount == 0))
        {
            if (!isDucking)
            {
                boxCollider.size = duckColliderSize;
                boxCollider.offset = duckColliderOffset;
                graphics.localPosition = originalGraphicsPos + slideOffset;
                isDucking = true;
            }
        }
        else if (isDucking)
        {
            if (CanStandUp())
            {
                boxCollider.size = originalColliderSize;
                boxCollider.offset = originalColliderOffset;
                graphics.localPosition = originalGraphicsPos;
                isDucking = false;
            }
        }
    }

    void HandleLateral()
    {
        float baseSpeed = scrollSpeed;

        float targetX = baseSpeed;
        if (Input.GetKey(KeyCode.D))
        {
            targetX += forwardBoost;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            targetX -= backwardPenalty;
            if (targetX < 0.5f) targetX = 0.5f;
        }

        float diff = targetX - rb.velocity.x;

        rb.AddForce(new Vector2(diff * 10f, 0f));
    }

    void UpdateAnimator()
    {
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isSliding", isDucking);
        anim.SetBool("isRunning", true);
    }

    bool CanStandUp()
    {
        var b = boxCollider.bounds;
        Vector2 standSize = new Vector2(
            Mathf.Abs(originalColliderSize.x * transform.lossyScale.x),
            Mathf.Abs(originalColliderSize.y * transform.lossyScale.y) + 0.02f
        );
        float bottom = b.min.y;
        Vector2 standCenter = new Vector2(b.center.x, bottom + standSize.y * 0.5f);

        Collider2D[] hits = Physics2D.OverlapBoxAll(standCenter, standSize, transform.eulerAngles.z);

        float topEdge = b.max.y;

        foreach (Collider2D hit in hits)
        {
            if ((hit.CompareTag("Ground") || hit.CompareTag("Obstacle")) && hit.bounds.min.y > topEdge)
            {
                return false; 
            }
        }
        return true;
    }

}
