using RageEvent;
using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
	int shootIndex;
	int rotateLeftIndex;
	int rotateRightIndex;

	float shoot = 1;
	float rotateLeft;
	float rotateRight;

	void Start()
	{
		shootIndex = RaiderInput.GetRandomIndex();
		rotateLeftIndex = RaiderInput.GetRandomIndex ();
		rotateRightIndex = RaiderInput.GetRandomIndex ();

		EventManager.Initialize (this);
	}

	[Listen ("InputChanged")]
	public void InputChanged(object[] parameters)
	{
		int index = (int)parameters[0];

		if (shootIndex == index)
			shoot = (float)parameters[1];

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
		if (shoot <= 0)
		{
			shoot = 1;

			var animator = GetComponentInChildren<Animator> ();
			if (animator)
				animator.SetTrigger("use");
		}
	}
}
