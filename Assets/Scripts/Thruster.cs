using RageEvent;
using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour
{
	static int sInputIndex = 0;
	int thrustInput;
	int rotateInput;

	void Start()
	{
		thrustInput = sInputIndex++;

		EventManager.Initialize (this);
	}

	float inputValue;

	[Listen ("InputChanged")]
	public void InputChanged(object[] parameters)
	{
		int index = (int)parameters[0];

		if (thrustInput == index)
		{
			inputValue = (float) parameters[1];

			var animator = GetComponentInChildren<Animator>();
			if (animator)
				animator.SetFloat("power", inputValue);
		}
	}

	void Update()
	{
		if (inputValue > 0)
			rigidbody2D.AddForce(transform.up*1000);
	}
}
