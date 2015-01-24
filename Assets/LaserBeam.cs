using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour
{
	public float speed;

	IEnumerator Start ()
	{
		rigidbody2D.velocity = -transform.right * speed;

		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		if (hit.gameObject.rigidbody2D)
			hit.gameObject.rigidbody2D.velocity = transform.right * -3;
	}
}
