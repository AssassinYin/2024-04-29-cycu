using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    [SerializeField] private GameObject _playerTransform;
    [SerializeField] private float _flipYRotationTime;

    private Movement _playerMovement;

    private bool _isFacingRight;

    private void Awake()
    {
        _playerMovement = _playerTransform.GetComponent<Movement>();
        _isFacingRight = _playerMovement.IsFacingRight;
    }
    private void Update()
    {
        transform.position = _playerTransform.transform.position;
    }

    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipYRotationTime).setEaseInOutSine();
    }
    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;
        if (_isFacingRight)
            return 180f;
        else
            return 0f;
    }
}
