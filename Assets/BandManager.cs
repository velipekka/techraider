using RageEvent;
using UnityEngine;
using System.Collections;

public class BandManager : MonoBehaviour
{
	public GameObject[] bandMembers;

	void Start () {

		foreach (var m in bandMembers)
		{
			var animator = m.GetComponentInChildren<Animator>();
			animator.SetBool("ingame", true);
		}

		EventManager.Initialize(this);
	}

	[Listen ("ShipHit")]
	public void ShipHit()
	{
		foreach (var m in bandMembers)
		{
			var animator = m.GetComponentInChildren<Animator> ();
			animator.SetTrigger("hit");
		}
	}
}
