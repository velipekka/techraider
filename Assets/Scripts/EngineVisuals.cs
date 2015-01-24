using UnityEngine;
using System.Collections;

public class EngineVisuals : MonoBehaviour {

	public ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		particleSystem.emissionRate = GetComponent<Animator> ().GetFloat ("power") * 100f;
		particleSystem.startSpeed = GetComponent<Animator> ().GetFloat ("power") * 5f;
		particleSystem.startLifetime = .25f + GetComponent<Animator> ().GetFloat ("power") * .75f;
	}
}
