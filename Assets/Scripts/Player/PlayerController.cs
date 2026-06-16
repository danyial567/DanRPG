using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// کنترل‌کننده کاراکتر اصلی - دان
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("حرکت")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private CharacterController characterController;
    
    [Header("انیمیشن")]
    [SerializeField] private Animator animator;
    
    [Header("مبارزه")]
    [SerializeField] private PlayerCombat playerCombat;
    
    private Vector2 moveInput;
    private Vector3 moveDirection;
    private float verticalVelocity;
    private float groundDrag = 5f;
    
    private bool isMoving;
    private bool isGrounded;

    private void Start()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
        if (animator == null)
            animator = GetComponent<Animator>();
        if (playerCombat == null)
            playerCombat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        ApplyGravity();
        UpdateAnimations();
    }

    /// <summary>
    /// مدیریت حرکت کاراکتر
    /// </summary>
    private void HandleMovement()
    {
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        isMoving = moveDirection.magnitude > 0;
        
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = verticalVelocity;
        
        characterController.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// مدیریت چرخش کاراکتر
    /// </summary>
    private void HandleRotation()
    {
        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(
                transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }
    }

    /// <summary>
    /// اعمال گرانش
    /// </summary>
    private void ApplyGravity()
    {
        isGrounded = characterController.isGrounded;
        
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity -= 9.81f * Time.deltaTime;
        }
    }

    /// <summary>
    /// به‌روزرسانی انیمیشن‌ها
    /// </summary>
    private void UpdateAnimations()
    {
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("Speed", moveDirection.magnitude);
    }

    /// <summary>
    /// دریافت ورودی حرکت (از Input System)
    /// </summary>
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    /// <summary>
    /// دریافت ورودی حمله
    /// </summary>
    public void OnAttack(InputValue value)
    {
        if (playerCombat != null)
            playerCombat.Attack();
    }

    /// <summary>
    /// دریافت ورودی استفاده از مهارت
    /// </summary>
    public void OnSkill(InputValue value)
    {
        if (playerCombat != null)
            playerCombat.UseSkill();
    }

    /// <summary>
    /// دریافت موقعیت
    /// </summary>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// تنظیم سرعت حرکت
    /// </summary>
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public bool IsMoving => isMoving;
    public bool IsGrounded => isGrounded;
}
