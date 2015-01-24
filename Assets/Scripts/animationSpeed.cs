using UnityEngine;
using System.Collections;

public class animationSpeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Animator>().speed = -1f;
	}
}
