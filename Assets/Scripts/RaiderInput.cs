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
	static int sInputIndex = -1;
	static int[] sShuffledIndexes;

	public static int GetRandomIndex()
	{
		if (sShuffledIndexes == null)
		{
			sShuffledIndexes = new[] {0, 1, 2 };
			ShipGenerator.Shuffle (sShuffledIndexes);
		}
		sInputIndex++;
		return sShuffledIndexes[Mathf.Clamp(sInputIndex, 0, sShuffledIndexes.Length - 1)];
	}

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
	public const int k_KeysPerController = 10;
	public const float k_WheelEpsilon = 0.1f;
	public const float k_KnobEpsilon = 0.02f;
	public const float k_FaderEpsilon = 0.3f;
	public const float k_WheelMultiplier = 1000f;

	public GamePadState[] prevState;
	public GamePadState[] currentState;
	
	public bool[] prevValues;

	// Use this for initialization
	void Start ()
	{
		EventManager.Initialize();
		currentState = new GamePadState[4];
		prevState = new GamePadState[4];
		prevValues = new bool[k_ControllerCount * k_KeysPerController];
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < 4; i++)
		{
			prevState[i] = currentState[i];
			currentState[i] = GamePad.GetState((PlayerIndex)i, GamePadDeadZone.None);
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
				s += GetValue(c, k) ? "1" : "0";
			}
		}
		GUILayout.Label(s);

		for (int i = 0; i < 4; i++)
		{
			GUILayout.Label(i + ": " + currentState[i].ThumbSticks.Right.X.ToString());
		}
	}

	private void GameInputHandling()
	{
		if (Time.timeSinceLevelLoad < 0.5f)
			return;

		for (int c = 0; c < k_ControllerCount; c++)
		{
			for (int k = 0; k < k_KeysPerController; k++)
			{
				if (HasChanged(c, k))
				{
					prevValues[(c + 1)*(k + 1) - 1] = GetValue(c, k);
					EventManager.Trigger("InputChanged", (c + 1)*(k + 1) - 1, GetValue(c, k));
				}
			}
		}
	}

	public bool HasChanged(int controlIndex, int keyIndex)
	{
		return prevValues[(controlIndex + 1)*(keyIndex + 1) - 1] != GetValue(controlIndex, keyIndex);
	}

	public bool GetValue(int controlIndex, int keyIndex)
	{
		GamePadState current = currentState[controlIndex];
		GamePadState prev = prevState[controlIndex];
		
		switch (keyIndex)
		{
			case 0:
				return current.Buttons.A == ButtonState.Pressed;
			case 1:
				return current.Buttons.B == ButtonState.Pressed;
			case 2:
				return current.Buttons.X == ButtonState.Pressed;
			case 3:
				return current.Buttons.Y == ButtonState.Pressed;
			case 4:
				return current.ThumbSticks.Left.X * k_WheelMultiplier > k_WheelEpsilon;
			case 5:
				return current.ThumbSticks.Left.X * k_WheelMultiplier < -k_WheelEpsilon;
			case 6:
				return (prev.ThumbSticks.Right.X - current.ThumbSticks.Right.X) > k_KnobEpsilon && Mathf.Abs(prev.ThumbSticks.Right.X - current.ThumbSticks.Right.X) < 0.5f;
			case 7:
				return (prev.ThumbSticks.Right.X - current.ThumbSticks.Right.X) < -k_KnobEpsilon && Mathf.Abs(prev.ThumbSticks.Right.X - current.ThumbSticks.Right.X) < 0.5f;
			case 8:
				return current.ThumbSticks.Right.Y > k_FaderEpsilon;
			case 9:
				return current.ThumbSticks.Right.Y < -k_FaderEpsilon;
		}
		return false;
	}
}
