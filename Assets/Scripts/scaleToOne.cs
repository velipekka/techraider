using UnityEngine;
using System.Collections;

public class scaleToOne : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime);
	}
}
