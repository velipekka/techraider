using UnityEngine;
using System.Collections;

internal static class HealthContainer
{
	static Hashtable healthMap = new Hashtable();
	const int maxHitpoint = 2;

	public static void InflictDamage(GameObject gameObject)
	{
		int key = gameObject.GetInstanceID();
		if (healthMap.ContainsKey(key))
			healthMap[key] = (int)healthMap[key] - 1;
		else
			healthMap.Add (key, maxHitpoint - 1);
	}

	public static bool IsDestroyed(GameObject gameObject)
	{
		int key = gameObject.GetInstanceID ();
		bool value = false;
		if (healthMap.ContainsKey (key))
			value = (int)healthMap[key] <= 0;
		else
			healthMap.Add (key, maxHitpoint);

		return value;
	}
}

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
		//if (hit.gameObject.rigidbody2D)
		//	hit.gameObject.rigidbody2D.velocity = transform.right * -3;

		HealthContainer.InflictDamage (hit.gameObject);

		if (HealthContainer.IsDestroyed (hit.gameObject))
			Destroy(hit.gameObject);

		Destroy(gameObject);
	}
}
