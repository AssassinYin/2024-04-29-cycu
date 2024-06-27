using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public Vector3 TrackedObjectOffset;

    private Vector2 _cameraDir;
    private CinemachineFramingTransposer _cinemachine;

    private void Awake()
    {
        _cinemachine = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
        if (_cameraDir != Vector2.zero)
            _cinemachine.m_TrackedObjectOffset = TrackedObjectOffset + new Vector3(_cameraDir.x, _cameraDir.y, 0);

        else
            _cinemachine.m_TrackedObjectOffset = TrackedObjectOffset;
    }

    public void OnCameraInput(InputAction.CallbackContext context) => _cameraDir = context.ReadValue<Vector2>().normalized;
}
