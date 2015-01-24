﻿using RageEvent;
using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
	int shootIndex;
	int rotateLeftIndex;
	int rotateRightIndex;

	bool shoot;
	bool rotateLeft;
	bool rotateRight;

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
			shoot = (bool)parameters[1];

		if (rotateLeftIndex == index)
			rotateLeft = (bool)parameters[1];

		if (rotateRightIndex == index)
			rotateRight = (bool)parameters[1];
	}

	IEnumerator CoChargeLaser()
	{
		var animator = GetComponentInChildren<Animator> ();
		float charge = 0;

		while (shoot)
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
		if (rotateLeft)
			child.Rotate (Vector3.forward, -1);

		if (rotateRight)
			child.Rotate (Vector3.forward, 1);

		// Thrust
		if (isCharging && !shoot)
		{
			isCharging = false;

			var animator = GetComponentInChildren<Animator> ();
			if (animator)
				animator.SetTrigger("use");
		}
	}
}