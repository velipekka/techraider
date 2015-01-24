using System;
using System.Collections.Generic;
using RageEvent;
using UnityEditorInternal;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using XInputDotNetPure;

public class RaiderInput : MonoBehaviour
{
	public class InputSource
	{
		public int controlIndex;
		public int keyIndex;
		
		public InputSource(int c, int k)
		{
			controlIndex = c;
			keyIndex = k;
		}
	}

	public const int k_ControllerCount = 4;
	public const int k_KeysPerController = 8;
	public const float k_Epsilon = 0.01f;
	public const float k_WheelMultiplier = 1000f;

	public GamePadState[] prevState;
	public GamePadState[] currentState;
	public float[] knobValues;

	// Use this for initialization
	void Start ()
	{
		EventManager.Initialize();
		currentState = new GamePadState[4];
		prevState = new GamePadState[4];
		knobValues = new float[4];
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < 4; i++)
		{
			prevState[i] = currentState[i];
			currentState[i] = GamePad.GetState((PlayerIndex)i, GamePadDeadZone.None);
			float knobDelta = currentState[i].ThumbSticks.Right.X - prevState[i].ThumbSticks.Right.X;
			if (Math.Abs(knobDelta) < 0.5f)
				knobValues[i] = Mathf.Clamp (knobValues[i] + (currentState[i].ThumbSticks.Right.X - prevState[i].ThumbSticks.Right.X), -1f, 1f);
		}

		GameInputHandling();
	}

	void OnGUI()
	{
		string s = "";
		for (int c = 0; c < k_ControllerCount; c++)
		{
			for (int k = 0; k < k_KeysPerController; k++)
			{
				s += ((int)(GetValue(c, k)*10f)) != 0 ? "1" : "0";
			}
		}
		GUILayout.Label(s);
	}

	private void GameInputHandling()
	{
		if (Time.timeSinceLevelLoad < 0.5f)
			return;

		for (int c = 0; c < k_ControllerCount; c++)
		{
			for (int k = 0; k < k_KeysPerController; k++)
			{
				if (HasChanged(c,k))
					EventManager.Trigger("InputChanged", (c+1)*(k+1)-1, GetValue(c, k));
			}
		}
	}

	public bool HasChanged(int controlIndex, int keyIndex)
	{
		GamePadState current = currentState[controlIndex];
		GamePadState prev = prevState[controlIndex];

		switch (keyIndex)
		{
			case 0:
				return current.Buttons.A != prev.Buttons.A;
			case 1:
				return current.Buttons.B != prev.Buttons.B;
			case 2:
				return current.Buttons.X != prev.Buttons.X;
			case 3:
				return current.Buttons.Y != prev.Buttons.Y;
			case 4:
				return Mathf.Abs((current.ThumbSticks.Left.X - prev.ThumbSticks.Left.X) * k_WheelMultiplier) > k_Epsilon;
			case 5:
				return Mathf.Abs((current.ThumbSticks.Left.Y - prev.ThumbSticks.Left.Y) * k_WheelMultiplier) > k_Epsilon;
			case 6:
				return Mathf.Abs(current.ThumbSticks.Right.X - prev.ThumbSticks.Right.X) > k_Epsilon;
			case 7:
				return Mathf.Abs(current.ThumbSticks.Right.Y - prev.ThumbSticks.Right.Y) > k_Epsilon;
		}
		return false;
	}

	public float GetValue(int controlIndex, int keyIndex)
	{
		GamePadState current = currentState[controlIndex];
		
		switch (keyIndex)
		{
			case 0:
				return current.Buttons.A == ButtonState.Pressed ? 1f : 0f;
			case 1:
				return current.Buttons.B == ButtonState.Pressed ? 1f : 0f;
			case 2:
				return current.Buttons.X == ButtonState.Pressed ? 1f : 0f;
			case 3:
				return current.Buttons.Y == ButtonState.Pressed ? 1f : 0f;
			case 4:
				return current.ThumbSticks.Left.X * k_WheelMultiplier;
			case 5:
				return current.ThumbSticks.Left.Y * k_WheelMultiplier;
			case 6:
				return knobValues[controlIndex];
			case 7:
				return Mathf.Abs(current.ThumbSticks.Right.Y) > 0.1f ? current.ThumbSticks.Right.Y : 0f;
		}
		return 0f;
	}
}
