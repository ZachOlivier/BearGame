using UnityEngine;

public class PenguinScript : MonoBehaviour
{
	public Transform PointToFollow;

	public float SnapDistance;

	public float Damping;
	public float PenguinMoveSpeed;
	public float CurrentMoveSpeed;

    #region public

    // Use this for initialization
    public void Start()
    {
		CurrentMoveSpeed = PenguinMoveSpeed;
	}
	
	// Update is called once per frame
	public void Update()
    {
		if (Vector3.Distance(transform.position, PointToFollow.position) <= SnapDistance)
		{
			if (CurrentMoveSpeed != 0)
			{
				CurrentMoveSpeed = 0;
			}
		}
		else if (Vector3.Distance(transform.position, PointToFollow.position) > SnapDistance)
		{
			var rotation = Quaternion.LookRotation(PointToFollow.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);

			if (CurrentMoveSpeed != PenguinMoveSpeed)
			{
				CurrentMoveSpeed = PenguinMoveSpeed;
			}

			transform.position += transform.forward * CurrentMoveSpeed * Time.deltaTime;
		}
	}

    #endregion public
}