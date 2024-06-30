using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    private PlayerMovement _movement;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _movement = _playerObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        transform.position = _playerObject.transform.position;
    }
}
