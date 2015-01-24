using RageEvent;
using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour
{
	static int sInputIndex = 0;

	int thrustIndex;
	int rotateLeftIndex;
	int rotateRightIndex;

	bool thrust;
	bool rotateLeft;
	bool rotateRight;

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
			thrust = (bool)parameters[1];

		if (rotateLeftIndex == index)
			rotateLeft = (bool)parameters[1];

		if (rotateRightIndex == index)
			rotateRight = (bool)parameters[1];
	}

	void Update()
	{
		var child = transform.GetChild (0);

		// Rotate
		if (rotateLeft)
			child.Rotate (Vector3.forward, -1f);

		if (rotateRight)
			child.Rotate (Vector3.forward, 1f);

		// Thrust
		var animator = GetComponentInChildren<Animator> ();
		if (animator)
			animator.SetFloat ("power", 0);

		if (thrust)
		{
			if (animator)
				animator.SetFloat ("power", 1);

			rigidbody2D.AddForce(child.right*20);
		}
	}
}
