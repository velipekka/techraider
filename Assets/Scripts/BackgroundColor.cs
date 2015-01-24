using System;
using UnityEngine;
using System.Collections;

public class BackgroundColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Color c1 = new Color(45f / 256f, 22f / 256f, 49f / 256f);
		Color c2 = new Color(22f / 256f, 45f / 256f, 49f / 256f);
		float v = Mathf.Abs(Mathf.Cos(Time.timeSinceLevelLoad*Mathf.PI*2.26f + 0.8f));
		float v2 = (1f - v);
		Camera.main.backgroundColor = c1*v + c2*v2;
	}
}
