using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения
    public float jumpForce = 10f; // Сила прыжка
    public LayerMask groundLayer; // Слой земли
    public Sprite idleSprite; // Спрайт для статичного состояния
    public Sprite moveRightSprite; // Спрайт для движения вправо
    public Sprite moveLeftSprite; // Спрайт для движения влево
    public Button jumpButton; // Кнопка прыжка

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
        // Проверка на земле
        bool isGrounded = IsGrounded();

        // Включение/выключение кнопки прыжка
        jumpButton.interactable = isGrounded;

        // Движение
        if (isMovingRight)
        {
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            spriteRenderer.sprite = moveRightSprite; // Меняем спрайт
        }
        else if (isMovingLeft)
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            spriteRenderer.sprite = moveLeftSprite; // Меняем спрайт
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            spriteRenderer.sprite = idleSprite; // Возвращаем спрайт для статичного состояния
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
        float rayLength = 0.5f; // Длина луча
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - 1f); // Смещение вниз
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);

        // Визуализация луча для отладки
        Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.red);

        return hit.collider != null;
    }
}
