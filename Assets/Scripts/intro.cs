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

	}

	[Listen("InputChanged")]
	public void InputChanged(object[] parameters)
	{
		if (Time.timeSinceLevelLoad > 5f)
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

	public void LoadGame()
	{
		Debug.Log("LOADGAME");
		Application.LoadLevel("j-asdf");
	}
}
