using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Sprite idleSprite;
    public Sprite moveRightSprite;
    public Sprite moveLeftSprite;
    public Button jumpButton;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private bool isMovingRight = false;
    private bool isMovingLeft = false;

    [SerializeField] private GameAudioController _gameAudioController;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        bool isGrounded = IsGrounded();

        jumpButton.interactable = isGrounded;

        if (isMovingRight)
        {
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            spriteRenderer.sprite = moveRightSprite;
        }
        else if (isMovingLeft)
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            spriteRenderer.sprite = moveLeftSprite;
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            spriteRenderer.sprite = idleSprite;
        }
    }

    public void OnMoveRightDown()
    {
        isMovingRight = true;
    }

    public void OnMoveRightUp()
    {
        isMovingRight = false;
    }

    public void OnMoveLeftDown()
    {
        isMovingLeft = true;
    }

    public void OnMoveLeftUp()
    {
        isMovingLeft = false;
    }

    public void OnJumpDown()
    {
        if (IsGrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            _gameAudioController.JumpSound();
        }
    }

    private bool IsGrounded()
    {
        Vector2 boxSize = new Vector2(0.5f, 0.1f);

        Vector2 boxCenter = new Vector2(transform.position.x, transform.position.y - 1f);

        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer);

        Debug.DrawLine(boxCenter - new Vector2(boxSize.x / 2, 0), boxCenter + new Vector2(boxSize.x / 2, 0), Color.green);
        Debug.DrawLine(boxCenter - new Vector2(boxSize.x / 2, boxSize.y), boxCenter + new Vector2(boxSize.x / 2, -boxSize.y), Color.green);

        return hit != null;
    }
}