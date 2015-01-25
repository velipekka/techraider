using RageEvent;
using UnityEngine;
using System.Collections;

public class intro : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventManager.Initialize(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//IntroAnimationFromWheel(4, true);
	}

	[Listen("InputChanged")]
	public void InputChanged(object[] parameters)
	{
		int index = (int) parameters[0];
		bool value = (bool) parameters[1];

		var isWheel = IntroAnimationFromWheel(index, value);

		if (Time.timeSinceLevelLoad > 5f && !isWheel)
		{
			GameObject go = GameObject.Find("donutsus");
			go.GetComponent<positionLerp>().m_TargetPosition = go.transform.position + new Vector3(0f, -3f, 0f);
			go = GameObject.Find("leggy");
			go.GetComponent<positionLerp>().m_TargetPosition = go.transform.position + new Vector3(0f, -3f, 0f);
			go = GameObject.Find("owen");
			go.GetComponent<positionLerp>().m_TargetPosition = go.transform.position + new Vector3(0f, -3f, 0f);
			go = GameObject.Find("jackson");
			go.GetComponent<positionLerp>().m_TargetPosition = go.transform.position + new Vector3(0f, -3f, 0f);
			go = GameObject.Find("vektorilogo");
			go.GetComponent<positionLerp>().m_TargetPosition = go.transform.position + new Vector3(0f, 3f, 0f);
			go = GameObject.Find("TextScroller");
			go.GetComponent<positionLerp>().m_TargetPosition = go.transform.position + new Vector3(0f, 10f, 0f);
			go = Camera.main.gameObject;
			go.GetComponent<musicfade>().m_Speed = 5f;
			EventManager.Trigger("GameStart");
			Invoke("LoadGame", 1.2f);
		}
	}

	private static bool IntroAnimationFromWheel(int index, bool value)
	{
		bool isWheel = index == 4 || index == 14 || index == 24 || index == 34 ||
					   index == 5 || index == 15 || index == 25 || index == 35;

		if (isWheel)
		{
			Animator leggy = GameObject.Find("leggy_body").GetComponent<Animator>();
			Animator owen = GameObject.Find("owen_body").GetComponent<Animator>();
			Animator jackson = GameObject.Find("jackson_lowerhand").GetComponent<Animator>();
			Animator donutsus = GameObject.Find("donutsus_body").GetComponent<Animator>();

			switch (index/10)
			{
				case 0:
					if (index == 4)
						jackson.SetBool("ingame", value);
					else
						jackson.SetBool("hit", value);
					break;
				case 1:
					if (index == 14)
						leggy.SetBool("ingame", value);
					else
						leggy.SetBool("hit", value);
					break;
				case 2:
					if (index == 24)
						owen.SetBool("ingame", value);
					else
						owen.SetBool("hit", value);
					break;
				case 3:
					if (index == 34)
						donutsus.SetBool("ingame", value);
					else
						donutsus.SetBool("hit", value);
					break;
			}
		}
		return isWheel;
	}

	public void LoadGame()
	{
		Debug.Log("LOADGAME");
		Application.LoadLevel("j-asdf");
	}
}
