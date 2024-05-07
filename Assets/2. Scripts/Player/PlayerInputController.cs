using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerAnimHandler _playerAnimHandler;

    [Header("Physics")]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _maxWalkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _maxSprintSpeed;
    [Space(10)]
    [SerializeField] private float _jumpForce;

    [Header("Ground Layers")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Character")]
    [SerializeField] private Transform _character;

    public bool _OnADS;
    public bool _OnGround;
    private bool _OnSprinting;
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

        GroundCheck();
    }

    private void FixedUpdate()
    {
        Move();
        SpeedControl();
    }
    private void Move()
    {
        _rb.AddRelativeForce(_direction * (_OnSprinting && !_OnADS ? _sprintSpeed : _walkSpeed), ForceMode.Impulse);
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector3>().normalized;
    }

    public void InputSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _OnSprinting = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _OnSprinting = false;
        }
    }

    public void SpeedControl()
    {
        Vector3 _curSpeed = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        if (_curSpeed.magnitude > (!_OnSprinting ? _maxWalkSpeed : _maxSprintSpeed))
        {
            Vector3 limitVelocity = _curSpeed.normalized * (!_OnSprinting ? _maxWalkSpeed : _maxSprintSpeed);
            _rb.velocity = new Vector3(limitVelocity.x, _rb.velocity.y, limitVelocity.z);
        }
    }


    public void InputADSMode(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (_OnGround)
            {
                _OnADS = true;
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _OnADS = false;
        }
    }

    public void GroundCheck()
    {
        RaycastHit hitInfo;
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Vector3.down, out hitInfo, 0.7f);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Vector3.down, Color.red);
        if (hitInfo.collider != null)
        {
            _OnGround = _groundLayer.value == (_groundLayer.value | (1 << hitInfo.collider.gameObject.layer));
        }
        else
        {
            _OnGround = false;
        }
    }

    public void InputJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && _OnGround)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _playerAnimHandler.SetJump();
        }
    }
}
