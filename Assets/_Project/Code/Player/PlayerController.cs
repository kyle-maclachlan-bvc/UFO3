using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    MyInputActions _inputActions;
    private Rigidbody _rb;
    
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    private float _movementInput;
    private Vector3 _movement;
    private bool _shouldJump;
    

    private void Awake()
    {
        _inputActions = new MyInputActions();
        _rb = GetComponent<Rigidbody>();
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
            if (IsGrounded())
            {
                Jump();  
                Debug.Log("Can Jump!");
            }
            else
            {
                Debug.Log("Can't Jump!");
                _shouldJump = false;
            }
            
        }
    }

    private void Move()
    {
        _rb.AddForce(moveSpeed * _movement , ForceMode.Force);
    }
    private void Jump()
    {
        _rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        _shouldJump = false;
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.SphereCast(ray, 0.1f, 0.5f,groundLayer);
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
        _shouldJump = true;
    }

   
}
