using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //scriptable object which holds all the player's movement parameters.
    public PlayerData Data;

    #region COMPONENTS
    public Rumbler Rumbler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    #endregion COMPONENTS

    #region STATE PARAMETERS
    //control the various actions the player can perform at any time fields
    //allow other sctipts to read
    public bool IsFacingRight { get; private set; }
    public bool IsSliding { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsWallJumping { get; private set; }
    public bool IsDashing { get; private set; }
    public bool IsAttacking { get; private set; }

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

    //attack
    private bool _attackRefilling;
    #endregion STATE PARAMETERS

    #region INPUT PARAMETERS
    public Vector2 MoveInput { get; private set; }
    public float LastPressedJumpTime { get; private set; }
    public float LastPressedDashTime { get; private set; }
    public float LastPressedAttackTime { get; private set; }
    #endregion INPUT PARAMETERS

    #region CHECK PARAMETERS
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    //size of groundCheck depends on the size of character, slightly small than width (for ground) and height. (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.5f, 0.025f);
    [Space(5)]
    [SerializeField] private Transform _frontWallCheckPoint;
    [SerializeField] private Transform _backWallCheckPoint;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.025f, 0.9f);
    #endregion CHECK PARAMETERS

    #region LAYERS & TAGS
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;
    #endregion LAYERS & TAGS

    #region CAMERA
    [Header("Camera center")]
    [SerializeField] private CameraCenter CameraCenter;
    #endregion CAMERA

    #region ATTACK
    [Header("Attack")]
    [SerializeField] private Attack Attack;
    #endregion ATTACK

    #region UNITY METHODS
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetGravityScale(Data.gravityScale);
        IsFacingRight = true;
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
        LastPressedAttackTime -= Time.deltaTime;
        #endregion TIMERS

        #region INPUT HANDLER
        /* CAN BE REPLACED WITH UNITY EVENTS
        MoveInput = PlayerInput.actions["Move"].ReadValue<Vector2>();

        if (PlayerInput.actions["Jump"].WasPerformedThisFrame())
            OnJumpInput();

        if (PlayerInput.actions["Dash"].WasPressedThisFrame())
            OnDashInput(); 

        if (PlayerInput.actions["Attack"].WasPressedThisFrame())
            OnAttackInput(); */
        #endregion INPUT HANDLER

        #region COLLISION CHECKS
        if (!IsDashing && !IsJumping)
        {
            //if grounded set box overlaps with ground, then sets the lastGrounded to coyoteTime
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping)
            {
                LastOnGroundTime = Data.coyoteTime;
                ExtraJumpReset();
            }

            //right wall Check
            else if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _wallLayer) && IsFacingRight)
                    || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _wallLayer) && !IsFacingRight)) && !IsWallJumping)
            {
                LastOnWallRightTime = Data.coyoteTime;
                ExtraJumpReset();
            }

            //left wall Check
            else if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _wallLayer) && !IsFacingRight)
                || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _wallLayer) && IsFacingRight)) && !IsWallJumping)
            {
                LastOnWallLeftTime = Data.coyoteTime;
                ExtraJumpReset();
            }

            //two checks needed for both left and right walls since whenever the play turns the wall checkPoints swap sides
            LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
        }
        #endregion COLLISION CHECKS

        #region ATTACK CHECKS
        if (!IsAttacking && !_attackRefilling)
            StartCoroutine(nameof(RefillAttack), 1);

        else if (!IsDashing && LastPressedAttackTime > 0)
        {
            IsAttacking = true;
            StartCoroutine(nameof(StartAttack));
        }
        #endregion ATTACK CHECKS

        #region JUMP CHECKS
        if (IsJumping && Rigidbody.velocity.y < 0)
        {
            IsJumping = false;

            if (!IsWallJumping)
                _isJumpFalling = true;
        }

        if (IsWallJumping && Time.time - _wallJumpStartTime > Data.wallJumpTime)
        {
            IsWallJumping = false;
        }

        if (LastOnGroundTime > 0 && !IsJumping && !IsWallJumping)
        {
            _isJumpCut = false;

            if (!IsJumping)
                _isJumpFalling = false;
        }
        #endregion JUMP CHECKS

        #region SLIDE CHECKS
        if (CanSlide() && ((LastOnWallLeftTime > 0 && MoveInput.x < 0) || (LastOnWallRightTime > 0 && MoveInput.x > 0)))
            IsSliding = true;
        else
            IsSliding = false;
        #endregion SLIDE CHECKS
    }

    private void FixedUpdate()
    {
        #region TURN
        //handle turn
        if (MoveInput.x != 0)
            CheckDirectionToFace(MoveInput.x > 0);
        #endregion TURN

        #region DASH
        if (CanDash() && LastPressedDashTime > 0)
        {
            //freeze game for split second. Adds juiciness and a bit of forgiveness over directional input.
            Sleep(Data.dashSleepTime);

            //if not direction pressed, dash forward.
            if (MoveInput != Vector2.zero)
                if (Data.doLimitedDashDir)
                    _lastDashDir = (Mathf.Abs(MoveInput.x) < Mathf.Abs(MoveInput.y)) ? new Vector2(0, MoveInput.y) : new Vector2(MoveInput.x, 0);

                else
                    _lastDashDir = MoveInput;

            else
                _lastDashDir = IsFacingRight ? Vector2.right : Vector2.left;

            IsDashing = true;
            IsJumping = false;
            IsWallJumping = false;
            _isJumpCut = false;

            StartCoroutine(nameof(StartDash), _lastDashDir);
        }
        #endregion DASH

        #region MOVEMENT
        //handle run
        if (!IsDashing)
        {
            #region JUMP
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
            #endregion JUMP

            #region RUN
            if (IsWallJumping)
                Run(Data.wallJumpRunLerp);

            else
                Run(1);
            #endregion RUN

            #region ATTACK
            if (Attack.IsCollided)
            {
                ApplyReactionForce(Attack.Dir);
                Attack.IsCollided = false;
            }
            #endregion ATTACK
        }

        //handle dash ending
        else if (_isDashAttacking)
            Run(Data.dashEndRunLerp);
        #endregion MOVEMENT

        #region SLIDE
        //handle slide
        if (IsSliding)
            Slide();
        #endregion SLIDE

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
        #endregion GRAVITY
    }
    #endregion UNITY METHODS

    #region INPUT CALLBACKS
    //methods handle input detected in Update() or unity event
    public void OnMoveInput(InputAction.CallbackContext context) => MoveInput = context.ReadValue<Vector2>();

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
            LastPressedJumpTime = Data.jumpInputBufferTime;

        else if (context.action.WasReleasedThisFrame())
            OnJumpUpInput();
    }

    public void OnJumpUpInput() => _isJumpCut = (CanJumpCut() || CanWallJumpCut());

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
            LastPressedDashTime = Data.dashInputBufferTime;
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
            LastPressedAttackTime = Data.attackInputBufferTime;
    }
    #endregion INPUT CALLBACKS

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

    #region RUN METHODS
    private void Run(float lerpAmount)
    {
        //calculate the direction we want to move in and our desired velocity
        float targetSpeed = MoveInput.x * Data.runMaxSpeed;
        //we can reduce are control using Lerp() this smooths changes to are direction and speed
        targetSpeed = Mathf.Lerp(Rigidbody.velocity.x, targetSpeed, lerpAmount);

        #region Calculate AccelRate
        float accelRate;

        //gets an acceleration value based on if we are accelerating (includes turning) or trying to decelerate (stop).
        //as well as applying a multiplier if we're airborne.
        if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
        #endregion

        #region Add Bonus Jump Apex Acceleration
        //increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural.
        if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(Rigidbody.velocity.y) < Data.jumpHangTimeThreshold)
        {
            accelRate *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }
        #endregion

        #region Conserve Momentum
        //won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed.
        //prevent any deceleration from happening. (conserve current momentum)
        if (Data.doConserveMomentum && Mathf.Abs(Rigidbody.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(Rigidbody.velocity.x)
            == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
            accelRate = 0;
        #endregion

        //calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - Rigidbody.velocity.x;

        //calculate force along x-axis to apply to the player
        float movement = speedDif * accelRate;

        //convert to a vector and apply to rigidbody
        Rigidbody.AddForce(movement * Vector2.right, ForceMode2D.Force);

        // Rigidbody.velocity = new Vector2(Rigidbody.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / Rigidbody.mass, Rigidbody.velocity.y);
        // Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second.
    }

    private void Turn()
    {
        //Stores scale and flips the player with y rotation.
        if (IsFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180.0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0.0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
        }
        IsFacingRight = !IsFacingRight;
        CameraCenter.CallTurn();
    }

    private void Slide()
    {
        //works the same as the Run but only in the y-axis
        float speedDif = Data.slideSpeed - Rigidbody.velocity.y;
        float movement = speedDif * Data.slideAccel;

        //clamp the movement here to prevent any over corrections (these aren't noticeable in the Run)
        //force applied can't be greater than the (negative) speedDifference * by how many times a second FixedUpdate() is called
        //more detail in how force are applied to rigidbodies
        movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime),
                                          Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));

        Rigidbody.AddForce(movement * Vector2.up);
    }
    #endregion RUN METHODS

    #region JUMP METHODS
    private void Jump()
    {
        //ensures can't call Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;

        #region Perform Jump
        //increase the force applied if we are falling, will always feel like jump same amount.
        float force = Data.jumpForce;
        if (Rigidbody.velocity.y < 0)
            force -= Rigidbody.velocity.y;

        Rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        #endregion
    }

    private void WallJump(int dir)
    {
        //ensures can't call wall Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;
        LastOnWallRightTime = 0;
        LastOnWallLeftTime = 0;

        #region Perform Wall Jump
        //apply force in opposite direction of wall
        Vector2 force = new Vector2(Data.wallJumpForce.x, Data.wallJumpForce.y);
        force.x *= dir;

        if (Mathf.Sign(Rigidbody.velocity.x) != Mathf.Sign(force.x))
            force.x -= Rigidbody.velocity.x;

        //checks whether player is falling, if so subtract the velocity.y (counteracting force of gravity)
        //ensures the player always reaches desired jump force or greater
        if (Rigidbody.velocity.y < 0)
            force.y -= Rigidbody.velocity.y;

        //default apply are force instantly ignoring mass
        Rigidbody.AddForce(force, ForceMode2D.Impulse);
        #endregion
    }

    private void ExtraJumpReset() => currentExtraJump = Data.extraJump; //reset extra jump
    #endregion JUMP METHODS

    #region DASH METHODS
    private IEnumerator StartDash(Vector2 dir)
    {
        //ensures can't call Dash multiple times from one press
        LastOnGroundTime = 0;
        LastPressedDashTime = 0;

        float startTime = Time.time;

        _dashesLeft--;
        _isDashAttacking = true;

        SetGravityScale(0);

        //keep the player's velocity at the dash speed during the "attack" phase
        while (Time.time - startTime <= Data.dashAttackTime)
        {
            Rigidbody.velocity = dir.normalized * Data.dashSpeed;
            //pauses the loop until the next frame, creating something of a Update loop
            yield return null;
        }

        startTime = Time.time;

        _isDashAttacking = false;

        //begins the "end" of dash where return some control to the player but still limit run acceleration
        SetGravityScale(Data.gravityScale);
        Rigidbody.velocity = Data.dashEndSpeed * dir.normalized;

        while (Time.time - startTime <= Data.dashEndTime)
            yield return null;

        //dash over
        IsDashing = false;
    }

    //short period before the player is able to dash again.
    private IEnumerator RefillDash()
    {
        //short cooldown, can't constantly dash along the ground.
        _dashRefilling = true;
        yield return new WaitForSeconds(Data.dashRefillTime);
        _dashRefilling = false;
        _dashesLeft = Mathf.Min(Data.dashAmount, _dashesLeft + 1);
    }
    #endregion DASH METHODS

    #region ATTACK METHODS
    private IEnumerator StartAttack()
    {
        //ensures can't call Attack multiple times from one press
        LastPressedAttackTime = 0;
        LastPressedJumpTime = 0;

        //attack ready phase
        float startTime = Time.time;
        while (Time.time - startTime <= Data.attackReadyTime)
            yield return null;

        //attacking phase
        startTime = Time.time;
        Attack.GetComponent<Collider2D>().enabled = true;
        while (Time.time - startTime <= Data.attackTime)
            yield return null;

        //attack end phase
        startTime = Time.time;
        Attack.GetComponent<Collider2D>().enabled = false;
        while (Time.time - startTime <= Data.attackEndTime)
            yield return null;

        //attack over
        IsAttacking = false;
    }

    //short period before the player is able to attack again.
    private IEnumerator RefillAttack()
    {
        _attackRefilling = true;
        yield return new WaitForSeconds(Data.attackRefillTime);
        _attackRefilling = false;
    }

    private void ApplyReactionForce(Vector2 dir)
    {
        //increase the force applied if falling
        float force = Data.jumpForce;
        if (Rigidbody.velocity.y < 0)
            force -= Rigidbody.velocity.y;
        //propels the player upwards by the amount of jumpForce
        Rigidbody.AddForce(-(dir * force), ForceMode2D.Impulse);
    }

    private void Block()
    {

    }
    #endregion

    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    private bool CanSlide() => (LastOnWallTime > 0 && !IsJumping && !IsWallJumping && !IsDashing && LastOnGroundTime <= 0);

    private bool CanJump() => LastOnGroundTime > 0 && !IsJumping;

    private bool CanWallJump() => LastPressedJumpTime > 0 && LastOnWallTime > 0 && LastOnGroundTime <= 0 &&
        (!IsWallJumping || (LastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (LastOnWallLeftTime > 0 && _lastWallJumpDir == -1));

    private bool CanJumpCut() => IsJumping && Rigidbody.velocity.y > 0;

    private bool CanWallJumpCut() => IsWallJumping && Rigidbody.velocity.y > 0;

    private bool CanDash()
    {
        if (!IsDashing && _dashesLeft < Data.dashAmount && LastOnGroundTime > 0 && !_dashRefilling)
            StartCoroutine(nameof(RefillDash), 1);
        return _dashesLeft > 0;
    }
    #endregion CHECK METHODS

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
}