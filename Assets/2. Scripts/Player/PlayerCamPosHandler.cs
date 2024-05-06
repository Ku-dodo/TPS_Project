using UnityEngine;

public class PlayerCamPosHandler : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, _camera.transform.position, out hitInfo, 3f))
        {
            if (hitInfo.collider.gameObject.layer != gameObject.layer)
            {
                _camera.transform.localPosition = new Vector3(0.3f, 0f, Mathf.Clamp(-(hitInfo.distance - 0.3f), -2f, 0f));
            }
        }
    }
}
