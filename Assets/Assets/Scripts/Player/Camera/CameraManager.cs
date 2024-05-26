using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCamera;
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;
    public float _fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping {  get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _lerpYPanCoroutine;

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private float _normYPanAmount = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        foreach (CinemachineVirtualCamera camera in _allVirtualCamera)
            if (camera.enabled)
            {
                _currentCamera = camera;
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }

        _normYPanAmount = _framingTransposer.m_YDamping;
    }

    public void LerpYDamping(bool isPlayerFalling) {
        _lerpYPanCoroutine = StartCoroutine(StartLerpYAction(isPlayerFalling));
    }

    public IEnumerator StartLerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        float startDampAmount = _framingTransposer.m_YDamping, endDampAmount = 0f;

        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }

        else
            endDampAmount = _normYPanAmount;

        float elapsedTime = 0f;

        while (elapsedTime < _fallYPanTime)
        {
            elapsedTime += Time.deltaTime;
            _framingTransposer.m_YDamping = Mathf.Lerp(startDampAmount, endDampAmount, elapsedTime / _fallYPanTime);
            
            yield return null;
        }

        IsLerpingYDamping = false;
    }
}
