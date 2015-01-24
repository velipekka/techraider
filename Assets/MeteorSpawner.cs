using UnityEngine;
using System.Collections;

public class MeteorSpawner : MonoBehaviour
{
	public float startTime;
	public float minTime;
	public float maxTime;

	public GameObject meteorPrefab;
	public float radius;
	void Start()
	{
		Invoke ("Spawn", startTime);
	}

	void Spawn()
	{
		Vector3 position = Random.insideUnitCircle * radius;
		Instantiate(meteorPrefab, transform.position + position, Quaternion.identity);

		Invoke ("Spawn", Random.Range (minTime, maxTime));
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}
