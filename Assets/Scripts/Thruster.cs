using RageEvent;
using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour
{
	static int sInputIndex = 0;

	int thrustIndex;
	int rotateLeftIndex;
	int rotateRightIndex;

	float thrust;
	float rotateLeft;
	float rotateRight;

	void Start()
	{
		thrustIndex = sInputIndex++;
		rotateLeftIndex = sInputIndex++;
		rotateRightIndex = sInputIndex++;

		EventManager.Initialize (this);
	}

	[Listen ("InputChanged")]
	public void InputChanged(object[] parameters)
	{
		int index = (int)parameters[0];

		if (thrustIndex == index)
			thrust = (float)parameters[1];

		if (rotateLeftIndex == index)
			rotateLeft = (float)parameters[1];

		if (rotateRightIndex == index)
			rotateRight = (float)parameters[1];
	}

	void Update()
	{
		var child = transform.GetChild (0);

		// Rotate
		if (rotateLeft > 0)
			child.Rotate (Vector3.forward, -rotateLeft);

		if (rotateRight > 0)
			child.Rotate (Vector3.forward, rotateRight);

		// Thrust
		var animator = GetComponentInChildren<Animator> ();
		if (animator)
			animator.SetFloat ("power", 0);

		if (thrust > 0)
		{
			if (animator)
				animator.SetFloat ("power", 1);

			rigidbody2D.AddForce(child.right*20);
		}
	}
}
