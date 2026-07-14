using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region References
    MyInputActions _inputActions;
    [HideInInspector] public Rigidbody rb;
    #endregion

    #region Serialized Fields
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [Range(0f, 1f)] [SerializeField] private float airControlMultiplier = 0.2f;
    
    [Header("Ground Check Settings")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float maxDistance = 0.5f;
    [SerializeField] private float radius = 0.5f;
    #endregion
    
    #region Private Fields
    private float _movementInput;
    private Vector3 _movement;
    private bool _shouldJump;
    #endregion
    
    private void Awake()
    {
        _inputActions = new MyInputActions();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.Movement.performed += OnMovePerformed;
        _inputActions.Player.Movement.canceled += OnMoveCanceled;
        _inputActions.Player.Jump.performed += OnJumpPerformed;
    }
    private void OnDisable()
    {
        _inputActions.Disable();
        _inputActions.Player.Movement.performed -= OnMovePerformed;
        _inputActions.Player.Movement.canceled -= OnMoveCanceled;
        _inputActions.Player.Jump.performed -= OnJumpPerformed;
    }
    
    private void FixedUpdate()
    {
        Move();
        
        if (_shouldJump)
        {
            Jump();  
        }
    }
    private void Move()
    {
        Vector3 forceToApply = moveSpeed * _movement;

        if (!IsGrounded())
        {
            forceToApply *= airControlMultiplier;
        }

        rb.AddForce(forceToApply, ForceMode.Force);
    }
    private void Jump()
    {
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        _shouldJump = false;
    }
    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.SphereCast(ray, radius, maxDistance, groundLayer);
    }
    void OnMovePerformed(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<float>();
        _movement = new Vector3(_movementInput, 0, 0);
    }
    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _movement = Vector3.zero;
    }
    void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            _shouldJump = true;
        }
    }
}