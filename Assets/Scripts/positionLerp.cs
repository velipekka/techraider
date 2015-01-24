using UnityEngine;
using System.Collections;

public class positionLerp : MonoBehaviour
{
	public Vector3 m_TargetPosition;
	public float m_Speed;
	public float m_MinTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.timeSinceLevelLoad > m_MinTime)
			transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime*m_Speed);
	}
}
