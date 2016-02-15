using UnityEngine;

public class ParentScript : MonoBehaviour
{
    // Variable for holding which state the enemy is currently in
    public enum State
    {
        // Variables for holding the different states the enemy can be in
        Idle = 0,
        Wandering = 1,
        Aware = 2,
        Chasing = 3,
        Lost = 4
    }

    public State CurrentState;
	
	// Variable for holding the time since last detection
	public float LastDetect;
	
	// Variables for holding how long the enemy should be idle and wander
	public float TimeIdle;
	public int TimeWander;
	public int TimeAware;
	
	// Variables for holding which place the enemy is going to go
	public int Direction;
	
	public int WaypointTarget;
	
	public float SnapDist;
	public int MinDist;
	public int Zone1;
	public int Zone2;
	public int Zone3;
	public int MaxDist;
	
	public int Damping;
	public int MoveSpeed;
	
	public float LookAtTime;
	public float LookAtTimer;
	
	// Variables for holding where the enemy is when it detects someone
	public Transform DetectedPosition;
	public Transform EnemyPosition;
	
	public Quaternion Rotation;
	
	// Variable for holding the player game object
	public GameObject PC;
	
	// Variable for holding which waypopublic int is currently picked
	public Transform WaypointTargeted;
	
	// Variables for holding the waypopublic int game objects
	public Transform WaypointOne;
	public Transform WaypointTwo;
	public Transform WaypointThree;
	public Transform WaypointFour;
	public Transform WaypointFive;
	
	private PlayerScript _player;

    #region public

    public void Awake()
    {
		_player = PC.GetComponent<PlayerScript>();
	}

    // Use this for initialization
    public void Start()
    {
		// Set the first timer to a random number range for how long the enemy should be initially idle
		TimeIdle = Time.time + Random.Range(2, 4);
		
		WaypointTarget = 5;
		
		EnemyPosition.position = transform.position;
		DetectedPosition.position = transform.position;
		WaypointTargeted.position = WaypointFive.position;
	}

    // Update is called once per frame
    public void Update()
    {
		if (Vector3.Distance(transform.position, PC.transform.position) <= Zone3)
		{
			if (_player.IsSprinting || _player.IsJump)
            {
				// If the enemy's state is currently chasing
				if (CurrentState == State.Chasing)
                {
					// We need this in here to have the enemy not change states if it is already chasing
				}
				// If the enemy's state is not currently chasing
				else
                {
				// Set the timer for how long the enemy will remain aware, currently set to 4 seconds
				TimeAware = 4;
				LastDetect = TimeAware + Time.time;
				
				// Keep track of the enemy's current x, y and z coordinates
				DetectedPosition.position = transform.position;

                    // Set the enemy's state to aware
                    CurrentState = State.Aware;
				}
			}
		}
			
		if (Vector3.Distance(transform.position, PC.transform.position) <= Zone2)
		{
			// If the player is currently walking, do the same as above
			if (_player.IsWalking)
            {
				if (CurrentState == State.Chasing)
                {
				
				}
				else
                {
					TimeAware = 4;
					LastDetect = TimeAware + Time.time;
				
					DetectedPosition.position = transform.position;

                    CurrentState = State.Aware;
                }
			}
			else if (_player.IsSprinting || _player.IsJump)
            {
				if (CurrentState == State.Chasing)
                {
				
				}
				else
                {
					TimeAware = 6;
					LastDetect = TimeAware + Time.time;
				
					DetectedPosition.position = transform.position;

                    // Set the enemy's state to chasing
                    CurrentState = State.Chasing;
				}
			}
		}

		if (Vector3.Distance(transform.position, PC.transform.position) <= Zone1)
		{
			if (_player.IsWalking || _player.IsSprinting || _player.IsJump)
            {
				if (CurrentState == State.Chasing)
                {
			
				}
				else
                {
					TimeAware = 8;
					LastDetect = TimeAware + Time.time;
			
					DetectedPosition.position = transform.position;

                    CurrentState = State.Chasing;
				}
			}	
		}
		
		// If the current state is idle
		if (CurrentState == State.Idle)
        {
			// Set the enemy's position variables to its current position
			EnemyPosition.position = transform.position;
			
			// This makes sure the enemy isn't moving
			transform.position = EnemyPosition.position;
			
			if (transform.position == WaypointOne.position)
			{
				
			}
			
			// If the time set has passed by
			if (Time.time > TimeIdle)
            {
				if (transform.position == WaypointTargeted.position)
				{
					WaypointTarget++;
				}
				
				// If the set waypopublic int is waypopublic int 1 and the last waypopublic int was not waypopublic int 1
				if (WaypointTarget == 1)
                {
					// Set the waypopublic int to waypopublic int one
					WaypointTargeted = WaypointOne;
				}
				else if (WaypointTarget == 2)
                {
					WaypointTargeted = WaypointTwo;
				}
				else if (WaypointTarget == 3)
                {
					WaypointTargeted = WaypointThree;
				}
				else if (WaypointTarget == 4)
                {
					WaypointTargeted = WaypointFour;
				}
				else if (WaypointTarget == 5)
                {
					WaypointTargeted = WaypointFive;
					WaypointTarget = 0;
				}

                // Set the current state to wandering
                CurrentState = State.Wandering;
			}
		}
		else if (CurrentState == State.Wandering)
        {
			// If the waypopublic int picked was waypopublic int 1
			if (WaypointTargeted == WaypointOne)
            {
				Rotation = Quaternion.LookRotation(WaypointOne.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * Damping);
				
				if (Vector3.Distance(transform.position, WaypointOne.position) > SnapDist)
				{
					transform.position += transform.forward * MoveSpeed * Time.deltaTime;
					
					//animation.CrossFade(animal_walk.name);
				}
				
				if (Vector3.Distance(transform.position, WaypointOne.position) <= SnapDist)
				{
					transform.position = WaypointOne.position;
				}
			}
			else if (WaypointTargeted == WaypointTwo)
            {
				Rotation = Quaternion.LookRotation(WaypointTwo.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * Damping);
				
				if (Vector3.Distance(transform.position, WaypointTwo.position) > SnapDist)
				{
					transform.position += transform.forward * MoveSpeed * Time.deltaTime;
					
					//animation.CrossFade(animal_walk.name);
				}
				
				if (Vector3.Distance(transform.position, WaypointTwo.position) <= SnapDist)
				{
					transform.position = WaypointTwo.position;
				}
			}
			else if (WaypointTargeted == WaypointThree)
            {
				Rotation = Quaternion.LookRotation(WaypointThree.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * Damping);
				
				if (Vector3.Distance(transform.position, WaypointThree.position) > SnapDist)
				{
					transform.position += transform.forward * MoveSpeed * Time.deltaTime;
					
					//animation.CrossFade(animal_walk.name);
				}
				
				if (Vector3.Distance(transform.position, WaypointThree.position) <= SnapDist)
				{
					transform.position = WaypointThree.position;
				}
			}
			else if (WaypointTargeted == WaypointFour)
            {
				Rotation = Quaternion.LookRotation(WaypointFour.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * Damping);
				
				if (Vector3.Distance(transform.position, WaypointFour.position) > SnapDist)
				{
					transform.position += transform.forward * MoveSpeed * Time.deltaTime;
					
					//animation.CrossFade(animal_walk.name);
				}
				
				if (Vector3.Distance(transform.position, WaypointFour.position) <= SnapDist)
				{
					transform.position = WaypointFour.position;
				}
			}
			else if (WaypointTargeted == WaypointFive)
            {
				Rotation = Quaternion.LookRotation(WaypointFive.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * Damping);
				
				if (Vector3.Distance(transform.position, WaypointFive.position) > SnapDist)
				{
					transform.position += transform.forward * MoveSpeed * Time.deltaTime;
					
					//animation.CrossFade(animal_walk.name);
				}
				
				if (Vector3.Distance(transform.position, WaypointFive.position) <= SnapDist)
				{
					transform.position = WaypointFive.position;
				}
			}
			
			if (transform.position == WaypointOne.position)
            {
				TimeIdle = Time.time + Random.Range(7, 10);

                CurrentState = State.Idle;
			}
			else if (transform.position == WaypointTwo.position)
            {
				TimeIdle = Time.time + Random.Range(2, 4);

                CurrentState = State.Idle;
            }
			else if (transform.position == WaypointThree.position)
            {
				TimeIdle = Time.time + Random.Range(2, 4);

                CurrentState = State.Idle;
            }
			else if (transform.position == WaypointFour.position)
            {
				TimeIdle = Time.time + Random.Range(2, 4);

                CurrentState = State.Idle;
            }
			else if (transform.position == WaypointFive.position)
            {
				TimeIdle = Time.time + Random.Range(2, 4);

                CurrentState = State.Idle;
            }
		}
		else if (CurrentState == State.Aware)
        {
			// Set the enemy's position variables to its current position
			EnemyPosition.position = transform.position;
			
			// This makes sure the enemy isn't moving
			transform.position = EnemyPosition.position;
			
			// If too much time has passed by since last detection
			if (Time.time > LastDetect)
            {
                // Set current state to lost
                CurrentState = State.Lost;
			}
		}
		else if (CurrentState == State.Chasing)
        {
			Rotation = Quaternion.LookRotation(PC.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * Damping);
			
			if (Vector3.Distance(transform.position, PC.transform.position) > MinDist)
			{
				transform.position += transform.forward * MoveSpeed * Time.deltaTime;
			}
			
			if (Time.time > LastDetect)
            {
				LastDetect = Time.time + 3;

                CurrentState = State.Aware;
			}
		}
		else if (CurrentState == State.Lost)
        {
			Rotation = Quaternion.LookRotation(DetectedPosition.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * Damping);
			
			if (Vector3.Distance(transform.position, DetectedPosition.position) > SnapDist)
			{
				transform.position += transform.forward * MoveSpeed * Time.deltaTime;
				
				//animation.CrossFade(animal_walk.name);
			}
			
			if (Vector3.Distance(transform.position, DetectedPosition.position) <= SnapDist)
			{
				transform.position = DetectedPosition.position;
			}
			
			// If the enemy's current position is the detected object's current position
			if (transform.position == DetectedPosition.position)
            {
				TimeIdle = Time.time + Random.Range(2, 4);

                CurrentState = State.Idle;
			}
		}
		// Else if the state isn't set to one of the above, just reset the enemy's state to idle
		else
        {
			TimeIdle = Time.time + Random.Range(2, 4);

            CurrentState = State.Idle;
		}
	}

    #endregion public
}