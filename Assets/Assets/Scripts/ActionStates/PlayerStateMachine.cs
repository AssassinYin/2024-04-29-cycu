using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //scriptable object which holds all the player's movement parameters.
    public PlayerData Data;

    #region COMPONENTS
    public PlayerStateFactory StateFactory { get; private set; }
    public Rumbler Rumbler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    #endregion

    #region STATE PARAMETERS
    //control the various actions the player can perform at any time fields
    public PlayerBaseState CurrentState;
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsWallJumping { get; private set; }
    public bool IsDashing { get; private set; }
    public bool IsSliding { get; private set; }

    //timers
    public float LastOnGroundTime { get; private set; }
    public float LastOnWallTime { get; private set; }
    public float LastOnWallRightTime { get; private set; }
    public float LastOnWallLeftTime { get; private set; }

    //jump
    private bool _isJumpCut;
    private bool _isJumpFalling;

    //wall Jump
    private float _wallJumpStartTime;
    private int _lastWallJumpDir;

    //extra Jump
    private int currentExtraJump;

    //dash
    private int _dashesLeft;
    private bool _dashRefilling;
    private Vector2 _lastDashDir;
    private bool _isDashAttacking;
    #endregion

    #region INPUT PARAMETERS
    public Vector2 MoveInput { get; private set; }
    public float LastPressedJumpTime { get; private set; }
    public float LastPressedDashTime { get; private set; }
    #endregion

    #region CHECK PARAMETERS
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    //size of groundCheck depends on the size of character, slightly small than width (for ground) and height. (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [Space(5)]
    [SerializeField] private Transform _frontWallCheckPoint;
    [SerializeField] private Transform _backWallCheckPoint;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    #endregion

    #region LAYERS & TAGS
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;
    #endregion

    #region CAMERA
    [Header("Camera center")]
    [SerializeField] private CameraCenter CameraCenter;
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        //get components
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        SetGravityScale(Data.gravityScale);
        IsFacingRight = true;

        //initialize state
        StateFactory = new PlayerStateFactory(this);
        CurrentState = StateFactory.Grounded();
        CurrentState.EnterState();
    }

    private void Update()
    {
        #region TIMERS
        LastOnGroundTime -= Time.deltaTime;
        LastOnWallTime -= Time.deltaTime;
        LastOnWallRightTime -= Time.deltaTime;
        LastOnWallLeftTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;
        LastPressedDashTime -= Time.deltaTime;
        #endregion

        CurrentState?.UpdateStates();

        /*
        #region GRAVITY
        if (!_isDashAttacking)
        {
            //higher gravity if released the jump input or are falling.
            if (IsSliding)
                SetGravityScale(0);

            else if (Rigidbody.velocity.y < 0 && MoveInput.y < 0)
            {
                //much higher gravity if holding down
                SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
                //caps maximum fall speed, so when falling over large distances don't accelerate to insanely high speeds
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Mathf.Max(Rigidbody.velocity.y, -Data.maxFastFallSpeed));
            }
            else if (_isJumpCut)
            {
                //higher gravity if jump button released
                SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Mathf.Max(Rigidbody.velocity.y, -Data.maxFallSpeed));
            }
            else if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(Rigidbody.velocity.y) < Data.jumpHangTimeThreshold)
                SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);

            else if (Rigidbody.velocity.y < 0)
            {
                //higher gravity if falling
                SetGravityScale(Data.gravityScale * Data.fallGravityMult);
                //caps maximum fall speed, so when falling over large distances don't accelerate to insanely high speeds
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Mathf.Max(Rigidbody.velocity.y, -Data.maxFallSpeed));
            }
            else
                //default gravity if standing on a platform or moving upwards
                SetGravityScale(Data.gravityScale);
        }

        else
            SetGravityScale(0);
        //no gravity when dashing. (returns to normal once initial dashAttack phase over)
        #endregion
        */
    }
    /*
    private void FixedUpdate()
    {
        //handle turn
        if (MoveInput.x != 0)
            CheckDirectionToFace(MoveInput.x > 0);

        //handle run
        if (!IsDashing)
        {
            if (CanJump() && LastPressedJumpTime > 0) //jump
            {
                IsJumping = true;
                IsWallJumping = false;
                _isJumpCut = false;
                _isJumpFalling = false;
                Jump();
            }

            else if (CanWallJump() && LastPressedJumpTime > 0) //wall jump
            {
                IsWallJumping = true;
                IsJumping = false;
                _isJumpCut = false;
                _isJumpFalling = false;

                _wallJumpStartTime = Time.time;
                _lastWallJumpDir = (LastOnWallRightTime > 0) ? -1 : 1;

                WallJump(_lastWallJumpDir);
            }

            else if (currentExtraJump > 0 && LastPressedJumpTime > 0) //extra jump
            {
                currentExtraJump -= 1;
                IsJumping = true;
                IsWallJumping = false;
                _isJumpCut = false;
                _isJumpFalling = false;
                Jump();
            }

            if (IsWallJumping)
                Run(Data.wallJumpRunLerp);

            else
                Run(1);
        }

        //handle dash ending
        else if (_isDashAttacking)
        {
            Run(Data.dashEndRunLerp);
        }

        //handle slide
        if (IsSliding)
            Slide();
    }
    */
    #endregion UNITY METHODS
    #region GENERAL METHODS
    public void SetGravityScale(float scale) => Rigidbody.gravityScale = scale;
    private void Sleep(float duration) => StartCoroutine(nameof(PerformSleep), duration); //method used to call StartCoroutine
    private IEnumerator PerformSleep(float duration)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration); //must be Realtime since timeScale is 0
        Time.timeScale = 1;
    }
    #endregion GENERAL METHODS

    private void ExtraJumpReset() => currentExtraJump = Data.extraJump;

    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        // if (isMovingRight != IsFacingRight)
            // Turn();
    }

    private bool CanWallJump() => (LastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (LastOnWallLeftTime > 0 && _lastWallJumpDir == -1);
    private bool CanJumpCut()
    {
        return IsJumping && Rigidbody.velocity.y > 0;
    }

    private bool CanWallJumpCut()
    {
        return IsWallJumping && Rigidbody.velocity.y > 0;
    }

    public bool CanDash()
    {
        if (!IsDashing && _dashesLeft < Data.dashAmount && LastOnGroundTime > 0 && !_dashRefilling)
            StartCoroutine(nameof(RefillDash), 1);

        return _dashesLeft > 0;
    }

    //short period before the player is able to dash again.
    private IEnumerator RefillDash(int amount)
    {
        //short cooldown, can't constantly dash along the ground.
        _dashRefilling = true;
        yield return new WaitForSeconds(Data.dashRefillTime);
        _dashRefilling = false;
        _dashesLeft = Mathf.Min(Data.dashAmount, _dashesLeft + 1);
    }
    #endregion CHECK METHODS

    #region POSITION CHECK METHODS
    //if grounded set box overlaps with ground, then sets the lastGrounded to coyoteTime
    public bool IsOnGround() => Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer);
    public void OnGround() => LastOnGroundTime = Data.coyoteTime;
    //right wall Check
    public bool IsOnRightWall() => (Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _wallLayer) && IsFacingRight)
                                || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _wallLayer) && !IsFacingRight);
    public void OnRightWall() => LastOnWallRightTime = Data.coyoteTime;
    //left wall Check
    public bool IsOnLeftWall() => (Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _wallLayer) && !IsFacingRight)
                               || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _wallLayer) && IsFacingRight);
    public void OnLeftWall() => LastOnWallLeftTime = Data.coyoteTime;
    public void NotAirborne()
    {
        ExtraJumpReset();
        //two checks needed for both left and right walls since whenever the play turns the wall checkPoints swap sides
        LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
    }
    #endregion POSITION CHECK METHODS

    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_frontWallCheckPoint.position, _wallCheckSize);
        Gizmos.DrawWireCube(_backWallCheckPoint.position, _wallCheckSize);
    }
    #endregion EDITOR METHODS

    #region INPUT CALLBACKS
    //methods handle input detected in Update() or unity event
    public void OnMoveInput(InputAction.CallbackContext context) => MoveInput = context.ReadValue<Vector2>();
    public void OnJumpInput()
    {
        if (PlayerInput.actions["Jump"].WasPressedThisFrame())
            LastPressedJumpTime = Data.jumpInputBufferTime;

        else if (PlayerInput.actions["Jump"].WasReleasedThisFrame())
            OnJumpUpInput();
    }
    public void OnJumpUpInput() => _isJumpCut = (CanJumpCut() || CanWallJumpCut());
    public void OnDashInput() => LastPressedDashTime = Data.dashInputBufferTime;
    #endregion INPUT CALLBACKS
}
