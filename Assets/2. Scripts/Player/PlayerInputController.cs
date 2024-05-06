using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerAnimHandler _playerAnimHandler;

    [Header("Physics")]
    [SerializeField] private float _SprintSpeed;
    [SerializeField] float _WalkSpeed;
    [SerializeField] private float _JumpForce;

    [Header("Character")]
    [SerializeField] private Transform _character;

    public bool _OnADS;
    private bool _IsSprinting;
    private Vector3 _direction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerAnimHandler = GetComponent<PlayerAnimHandler>();
    }

    private void Update()
    {
        _playerAnimHandler.SetMoveAnim(_rb.velocity);
        if (!_OnADS && _rb.velocity.sqrMagnitude > 0.5f)
        {
            _character.rotation = Quaternion.Lerp(_character.rotation, Quaternion.Euler(0f, Mathf.Atan2(_rb.velocity.x, _rb.velocity.z) * Mathf.Rad2Deg, 0f), 0.3f);
        }
        else if (_OnADS)
        {
            _character.rotation = transform.rotation;
        }
        _playerAnimHandler.SetADSMode(_OnADS);
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        _rb.AddRelativeForce(_direction * (_IsSprinting && !_OnADS ? _SprintSpeed : _WalkSpeed), ForceMode.Impulse);
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector3>().normalized;
    }

    public void InputSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _IsSprinting = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _IsSprinting = false;
        }
    }

    public void InputADSMode(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _OnADS = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _OnADS = false;
        }
    }

    public void InputJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _rb.AddForce(Vector3.up * _JumpForce, ForceMode.Impulse);
        }
    }
}
