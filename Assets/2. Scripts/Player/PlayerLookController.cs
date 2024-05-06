using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookController : MonoBehaviour
{
    [Header("Controll Objects")]
    [SerializeField] private Transform _playerObj;
    [SerializeField] private Transform _camContainer;

    [Header("Mouse Stat")]
    public float _mouseSpeed;

    private float _maxClamp = 70;
    private float _minClamp = -30;
    private float _viewX = 0;
    private float _viewY = 0;

    private Vector2 _mouseDelta;

    private void Update()
    {
        RotatePlayerObj();
    }

    private void LateUpdate()
    {
        _viewX += _mouseDelta.x * _mouseSpeed * Time.deltaTime;
        _viewY -= _mouseDelta.y * _mouseSpeed * Time.deltaTime;

        _viewY = Mathf.Clamp(_viewY, _minClamp, _maxClamp);

        _camContainer.rotation = Quaternion.Euler(_viewY, _viewX, 0f);
        _camContainer.position = new Vector3(_playerObj.position.x, 1.3f, _playerObj.position.z);
    }

    public void InputMouseMove(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }

    private void RotatePlayerObj()
    {
        _playerObj.rotation = Quaternion.Euler(0f, _camContainer.eulerAngles.y, 0f);
    }
}
