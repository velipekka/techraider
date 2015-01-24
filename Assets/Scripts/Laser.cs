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

	bool isCharging;

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

	IEnumerator CoChargeLaser()
	{
		var animator = GetComponentInChildren<Animator> ();
		float charge = 0;

		while (shoot > 0)
		{
			isCharging = true;

			charge += Time.deltaTime / 2;
			if (animator)
				animator.SetFloat("charge", charge);

			yield return null;
		}

		if (animator)
			animator.SetFloat ("charge", 0);
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
		if (isCharging && shoot <= 0)
		{
			isCharging = false;

			var animator = GetComponentInChildren<Animator> ();
			if (animator)
				animator.SetTrigger("use");
		}
	}
}
