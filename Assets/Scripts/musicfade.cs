using UnityEngine;
using System.Collections;

public class musicfade : MonoBehaviour 
{
	public float m_Speed = 0f;
	public float m_TargetVolume = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		AudioSource a = GetComponent<AudioSource>();
		a.volume = Mathf.Lerp(a.volume, m_TargetVolume, Time.deltaTime * m_Speed);
	}
}
