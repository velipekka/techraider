using RageEvent;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
	private float m_GameOverTime = 0f;
	public bool m_GameOver;
	private float delay = 1f;
	// Use this for initialization
	void Start () {
		EventManager.Initialize(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_GameOver && Time.timeSinceLevelLoad - m_GameOverTime > delay)
		{
			GameObject.Find("RetryText").GetComponent<Text>().enabled = true;
		}
	}

	[Listen("InputChanged")]
	public void InputChanged(object[] parameters)
	{
		Debug.Log("INPUT " + m_GameOver + ", " + (Time.timeSinceLevelLoad - m_GameOverTime));
		if (m_GameOver && Time.timeSinceLevelLoad - m_GameOverTime > delay)
		{
			GameObject.Find("raider_bg2").GetComponent<musicfade>().m_TargetVolume = 0f;
			GameObject.Find("raider_bg2").GetComponent<musicfade>().m_Speed = 10f;
			Invoke("Restart", 0.66f);
		}
	}

	[Listen("GameOver")]
	public void GameOver()
	{
		GameObject.Find("GameOverText").GetComponent<Text>().enabled = true;
		GameObject.Find("raider_bg").GetComponent<musicfade>().m_Speed = 4f;
		GameObject.Find("raider_bg2").GetComponent<musicfade>().m_TargetVolume = 1f;
		GameObject.Find("raider_bg2").GetComponent<musicfade>().m_Speed = 4f;
		m_GameOver = true;
		m_GameOverTime = Time.timeSinceLevelLoad;
	}

	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
