using UnityEngine;

public class PlayerAnimHandler : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponent<Animator>();
    }

    public void SetMoveAnim(Vector3 _direction)
    {
        float dirLength = _direction.magnitude;
        if (dirLength < Mathf.Epsilon)
        {
            return;
        }

        _animator.SetFloat("dirSpeed", dirLength);
        //월드 velocity에서 로컬 velocity로 전환
        Vector3 localVelocity = transform.InverseTransformDirection(_direction);
        _animator.SetFloat("dirX", localVelocity.x);
        _animator.SetFloat("dirZ", localVelocity.z);
    }

    public void SetJump(bool _bool)
    {
        _animator.SetBool("IsJump", _bool);
    }

    public void SetADSMode(bool _bool)
    {
        _animator.SetBool("OnADS", _bool);
    }
}
