using UnityEngine;
using System.Collections;

public class logofx : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float v = Mathf.Abs(Mathf.Cos(Time.timeSinceLevelLoad * Mathf.PI * 2.26f + 0.8f)) * 0.03f + 1f;
		transform.localScale = Vector3.one * 0.22f * v;
		float r = Mathf.Cos(Time.timeSinceLevelLoad * Mathf.PI * 2.26f / 8f + 0.8f) * 3f;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, r);
	}
}
