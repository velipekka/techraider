using RageEvent;
using UnityEngine;
using System.Collections;

public class TestRaider : MonoBehaviour
{
	void Start () 
	{
		EventManager.Initialize(this);
	}

	[Listen("InputChanged")]
	public void InputChanged(object[] parameters)
	{
		int index = (int)parameters[0];
		float value = (float) parameters[1];
		Debug.Log("Input changed index: " + index + ", value: " + value);
	}
}
