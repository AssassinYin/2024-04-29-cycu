using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    #region GRAVITY
    [Header("Gravity")]
    //multiplier to the player's gravityScale when falling
    public float fallGravityMult;
	//maximum fall speed (terminal velocity) of the player when falling
	public float maxFallSpeed;

    [Space(5)]

	//larger multiplier to the player's gravityScale when they are falling and a downwards input is pressed
	public float fastFallGravityMult;
	//maximum fall speed(terminal velocity) of the player when performing a faster fall
	public float maxFastFallSpeed;
    //downwards force (gravity) needed for the desired jumpHeight and jumpTimeToApex.
    [HideInInspector] public float gravityStrength;
    //strength of the player's gravity as a multiplier of gravity (set in ProjectSettings/Physics2D) also the value the player's rigidbody2D.gravityScale is set to
    [HideInInspector] public float gravityScale;
    #endregion GRAVITY

    #region MOVE
    [Space(20)]

	[Header("Run")]
	//target speed we want the player to reach
	public float runMaxSpeed;
	//the speed at which our player accelerates to max speed, can be set to runMaxSpeed for instant acceleration down to 0 for none at all
	public float runAcceleration;
	//the actual force (multiplied with speedDiff) applied to the player
	[HideInInspector] public float runAccelAmount;
	//the speed at which our player decelerates from their current speed, can be set to runMaxSpeed for instant deceleration down to 0 for none at all
	public float runDecceleration;
	//actual force (multiplied with speedDiff) applied to the player
	[HideInInspector] public float runDeccelAmount;
	
	[Space(5)]
	
	//multipliers applied to acceleration rate when airborne.
	[Range(0f, 1)] public float accelInAir;
	[Range(0f, 1)] public float deccelInAir;

	[Space(5)]
	public bool doConserveMomentum;
    #endregion MOVE

    #region SLIDE
    [Space(20)]

    [Header("Slide")]
    public float slideSpeed;
    public float slideAccel;
    #endregion SLIDE

    #region JUMP
    [Space(20)]

	[Header("Jump")]
	//height of the player's jump
	public float jumpHeight;
	//time between applying the jump force and reaching the desired jump height. These values also control the player's gravity and jump force
	public float jumpTimeToApex;
	//the actual force applied (upwards) to the player when they jump
	[HideInInspector] public float jumpForce;

    [Space(20)]

    [Header("Wall Jump")]
    //the actual force (this time set by us) applied to the player when wall jumping.
    public Vector2 wallJumpForce;
    //reduces the effect of player's movement while wall jumping.
    [Range(0f, 1f)] public float wallJumpRunLerp;
    //time after wall jumping the player's movement is slowed for.
    [Range(0f, 1.5f)] public float wallJumpTime;
    //player will rotate to face wall jumping direction
    public bool doTurnOnWallJump;

    [Space(20)]

    [Header("Both Jumps")]
	//multiplier to increase gravity if the player releases thje jump button while still jumping
	public float jumpCutGravityMult;
	//reduces gravity while close to the apex (desired max height) of the jump
	[Range(0f, 1)] public float jumpHangGravityMult;
	//speeds (close to 0) where the player will experience extra "jump hang"
	//the player's velocity.y is closest to 0 at the jump's apex (think of the gradient of a parabola or quadratic function)
	public float jumpHangTimeThreshold;
	public float jumpHangAccelerationMult; 
	public float jumpHangMaxSpeedMult;
    //extra time player can jump before landed.
    public int extraJump;

    [Space(20)]

    [Header("Jump Assists")]
    //grace period after falling off a platform, where you can still jump
    [Range(0.01f, 0.5f)] public float coyoteTime;
    //grace period after pressing jump where a jump will be automatically performed once the requirements (eg. being grounded) are met.
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime;
    #endregion JUMP

    #region DASH
    [Space(20)]

	[Header("Dash")]
    //amount for dash to refilled
    public int dashAmount;
    //
	public float dashSpeed;
	//duration for which the game freezes when we press dash but before read directional input and apply a force
	public float dashSleepTime;
    //time dash takes to end
    public float dashAttackTime;
	//time after you finish the inital drag phase, smoothing the transition back to idle (or any standard state)
	public float dashEndTime;
	//slows down player, makes dash feel more responsive (used in Celeste)
	public Vector2 dashEndSpeed;
	//slows the affect of player movement while dashing
	[Range(0f, 1f)] public float dashEndRunLerp;
    //time before dash refilled
    public float dashRefillTime;
	[Range(0.01f, 0.5f)] public float dashInputBufferTime;

    [Space(5)]
    public bool doLimitedDashDir;
    #endregion DASH

    #region ATTACK
    [Space(20)]

    [Header("Attack")]
    //how much force should player move either downwards or horizontally when melee attack collides
    public float defaultForce;
    //how much force should player felt when melee attack collides
    public float upwardsForce;

    //how long the player should move when melee attack collides
    public float attackReadyTime;
    //time attack takes to end
    public float attackTime;
    //time after finish attack, the immobilized time of attack
    public float attackEndTime;
    //time before attack refilled
    public float attackRefillTime;
    [Range(0.01f, 0.5f)] public float attackInputBufferTime;
    #endregion ATTACK

    #region CAMERA
    #endregion CAMERA

    #region UNITY METHODS
    //Unity Callback, called when the inspector updates
    private void OnValidate()
    {
		//calculate gravity strength using the formula (gravity = 2 * jumpHeight / timeToJumpApex^2) 
		gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
		
		//calculate the rigidbody's gravity scale (ie: gravity strength relative to unity's gravity value, see project settings/Physics2D)
		gravityScale = gravityStrength / Physics2D.gravity.y;

		//calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
		runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
		runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

		//calculate jumpForce using the formula (initialJumpVelocity = gravity * timeToJumpApex)
		jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

		#region Variable Ranges
		runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
		runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
		#endregion
	}
    #endregion UNITY METHODS
}