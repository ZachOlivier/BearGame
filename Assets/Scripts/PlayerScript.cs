using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerScript : MonoBehaviour
{
	public AudioClip WalkSound;
	public AudioClip WalkOnDebris;
	public AudioClip RunSound;

	public float WalkVolume;
	public float RunVolume;

	public int StartHealth;
	public int Health;

	public float MoveSpeed;
	public float BackSpeed;

	public float SprintModifier;
	public float StealthModifier;
	public float JumpModifier;
	
	// Variables to tell what the player is currently doing
	public bool IsIdle;
	public bool IsWalking;
	public bool IsSprinting;
	public bool IsStealth;
	public bool IsJump;
	
	public bool CanTalk;
	public bool CanSound;
	
	private CharacterMotor _movement;

    #region public

    public void Awake()
	{
		_movement = this.GetComponentInParent<CharacterMotor>();
	}

    // Use this for initialization
    public void Start()
	{
		GetComponent<AudioSource>().clip = WalkSound;
		GetComponent<AudioSource>().loop = true;
		
		Health = StartHealth;

        CanSound = true;
	}

    // Update is called once per frame
    public void Update()
	{
		if (!_movement.enabled && GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().Stop();
		}
		
		if (Input.GetKey (KeyCode.W) && CanSound)
		{
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}
		
		if (Input.GetKey (KeyCode.D) && CanSound)
		{
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}
		
		if (Input.GetKey (KeyCode.A) && CanSound)
		{
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}
		
		if (Input.GetKey (KeyCode.S) && CanSound)
		{
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}
		
		if (Input.GetKeyUp (KeyCode.W))
		{
			GetComponent<AudioSource>().Stop();
		}
		
		if (Input.GetKeyUp (KeyCode.D))
		{
			GetComponent<AudioSource>().Stop();
		}
		
		if (Input.GetKeyUp (KeyCode.A))
		{
			GetComponent<AudioSource>().Stop();
		}
		
		if (Input.GetKeyUp (KeyCode.S))
		{
			GetComponent<AudioSource>().Stop();
		}

		if (!CanSound)
		{
			GetComponent<AudioSource>().Stop();
		}
		
		// If the player isn't moving
		if (_movement.movement.velocity.magnitude <= 0.0f && _movement.grounded)
		{
            Idle();
		}
        /* Else if the player is moving slow and isn't jumping or stealthed
		else if (movement.movement.velocity.magnitude > 1.0f && movement.movement.velocity.magnitude <= 9.0f && movement.grounded != false && !isStealth)
		{
			Walk();
		}
		Else if the player is moving fast and isn't jumping
		else if (movement.movement.velocity.magnitude > 9.0f && movement.grounded != false)
		{
			Sprint();
		}*/
        // Else if the player is jumping and isn't stealthed
        else if (!_movement.grounded)
		{
            Jump();
		}
		
		// If the player is holding the shift key
		if (Input.GetButton("Sprint") && _movement.grounded)
		{
			Sprint();
		}
        // Else if the player let go of the shift key
        else if (Input.GetButtonUp("Sprint"))
        {
            Walk();
        }

        // If the player pressed the stealth key and he wasn't currently stealthed
        if (Input.GetButtonDown("Stealth") && !IsStealth)
		{
			Stealth();
		}	
		// Else if the player pressed the stealth key and he was currently stealthed
		else if (Input.GetButtonDown("Stealth") && IsStealth)
		{
			Walk();
		}
		
		if (_movement.grounded && _movement.movement.maxForwardSpeed <= 0.0f)
		{
			Walk();
		}
	}

    #endregion public

    #region private

    private void Idle()
    {
        // Set idle to true, and everything else to false
        IsIdle = true;
        IsWalking = false;
        IsSprinting = false;
        IsJump = false;
        IsStealth = false;
    }

    private void Walk()
	{
		_movement.movement.maxForwardSpeed = MoveSpeed;
		_movement.movement.maxSidewaysSpeed = MoveSpeed;
		_movement.movement.maxBackwardsSpeed = BackSpeed;
		
		GetComponent<AudioSource>().volume = WalkVolume;
		GetComponent<AudioSource>().clip = WalkSound;

        IsIdle = false;
        IsWalking = true;
        IsSprinting = false;
        IsJump = false;
        IsStealth = false;

		CanSound = true;
	}

    private void Sprint()
	{
		_movement.movement.maxForwardSpeed = MoveSpeed * SprintModifier;
		_movement.movement.maxSidewaysSpeed = MoveSpeed * SprintModifier;
		_movement.movement.maxBackwardsSpeed = BackSpeed * SprintModifier;

		GetComponent<AudioSource>().volume = RunVolume;
		GetComponent<AudioSource>().clip = RunSound;

        IsIdle = false;
        IsWalking = false;
        IsSprinting = true;
        IsJump = false;
        IsStealth = false;
		
		CanSound = true;
	}

    private void Stealth()
	{
		_movement.movement.maxForwardSpeed = MoveSpeed * StealthModifier;
		_movement.movement.maxSidewaysSpeed = MoveSpeed * StealthModifier;
		_movement.movement.maxBackwardsSpeed = BackSpeed * StealthModifier;

        IsIdle = false;
        IsWalking = false;
        IsSprinting = false;
        IsJump = false;
        IsStealth = true;
		
		CanSound = false;
	}

    private void Jump()
	{
		_movement.movement.maxForwardSpeed = MoveSpeed * JumpModifier;
		_movement.movement.maxSidewaysSpeed = MoveSpeed * JumpModifier;
		_movement.movement.maxBackwardsSpeed = BackSpeed * JumpModifier;

        IsIdle = false;
        IsWalking = false;
        IsSprinting = false;
        IsJump = true;
        IsStealth = false;
		
		CanSound = false;
	}

    #endregion private
}