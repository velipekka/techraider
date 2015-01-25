using System;
using RageEvent;
using UnityEngine;
using System.Collections;

public class BackgroundColor : MonoBehaviour
{
	private bool startingGame;
	// Use this for initialization
	void Start () {
		EventManager.Initialize(this);
	}
	
	// Update is called once per frame
	void Update () {
		Color c1 = new Color(45f / 256f, 22f / 256f, 49f / 256f);
		Color c2 = new Color(22f / 256f, 45f / 256f, 49f / 256f);
		float v = Mathf.Abs(Mathf.Cos(Time.timeSinceLevelLoad*Mathf.PI*2.26f + 0.8f));
		float v2 = (1f - v);
		if (!startingGame)
			Camera.main.backgroundColor = c1*v + c2*v2;
		else
			Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, Time.deltaTime);
	}

	[Listen("GameStart")]
	public void GameStart()
	{
		startingGame = true;
	}
}
