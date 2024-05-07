using UnityEngine;

public class PlayerAnimHandler : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
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
        //���� velocity���� ���� velocity�� ��ȯ
        Vector3 localVelocity = transform.InverseTransformDirection(_direction);
        _animator.SetFloat("dirX", localVelocity.x);
        _animator.SetFloat("dirZ", localVelocity.z);
    }

    public void SetJump()
    {
        _animator.SetTrigger("OnJump");
    }

    public void SetADSMode(bool _bool)
    {
        _animator.SetBool("OnADS", _bool);
    }
}
