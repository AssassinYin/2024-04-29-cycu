using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    private PlayerMovement _movement;

    private Vector2 _dir;
    private bool _isCollided;

    public Vector2 Dir { get { return _dir; } set { _dir = value; } }
    public bool IsCollided { get { return _isCollided; } set { _isCollided = value; } }

    private void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _movement = _playerObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        transform.position = _playerObject.transform.position;
        if (_movement.MoveInput != Vector2.zero)
        {
            _dir = (Mathf.Abs(_movement.MoveInput.x) > Mathf.Abs(_movement.MoveInput.y)) ?
                 (_movement.MoveInput.x > 0) ? Vector2.right : Vector2.left :
                 (_movement.MoveInput.y > 0) ? Vector2.up : Vector2.down;

            if (_movement.LastOnGroundTime > 0) {
                transform.eulerAngles = (_dir.y == 0) ?
                    (_dir.x == 1) ? new Vector3(0.0f, 0.0f, 0.0f) : new Vector3(0.0f, 0.0f, 180.0f) :
                    (_dir.y == 1) ? new Vector3(0.0f, 0.0f, 90.0f) :
                    _movement.IsFacingRight ? new Vector3(0.0f, 0.0f, 0.0f) : new Vector3(0.0f, 0.0f, 180.0f);
            }
            else
            {
                transform.eulerAngles = (_dir.y == 0) ?
                    (_dir.x == 1) ? new Vector3(0.0f, 0.0f, 0.0f) : new Vector3(0.0f, 0.0f, 180.0f) :
                    (_dir.y == 1) ? new Vector3(0.0f, 0.0f, 90.0f) : new Vector3(0.0f, 0.0f, 270.0f);
            }
        }

        else
        {
            _dir = _movement.IsFacingRight ? Vector2.right : Vector2.left;
            transform.eulerAngles = _movement.IsFacingRight ? new Vector3(0.0f, 0.0f, 0.0f) : new Vector3(0.0f, 0.0f, 180.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the GameObject colliding with has an EntityHealth script
        if (collision.GetComponent<FoesHealth>())
        {
            Debug.Log("FoesHealth component found on: " + collision.gameObject.name);
            //checks to see what force can be applied to the player when melee attacking
            HandleCollision(collision.GetComponent<FoesHealth>());
        }
    }

    private void HandleCollision(FoesHealth obj)
    {
        _isCollided = true;
        obj.ApplyDamage(10);
    }
}