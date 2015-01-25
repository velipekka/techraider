using RageEvent;
using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
	int shootIndex;
	int rotateLeftIndex;
	int rotateRightIndex;

	bool shoot;
	bool rotateLeft;
	bool rotateRight;

	bool isCharging;

	public ParticleSystem laserParticle;
	public GameObject laserBeamPrefab;
	public float laserForce;
	public AudioClip[] laserSFX;

	void Start()
	{
		var indexes = RaiderInput.GetRandomIndexes();
		shootIndex = indexes[3];
		rotateLeftIndex = indexes[4];
		rotateRightIndex = indexes[5];

		EventManager.Initialize (this);
	}

	[Listen ("InputChanged")]
	public void InputChanged(object[] parameters)
	{
		int index = (int)parameters[0];

		Debug.Log(index);

		if (shootIndex == index)
			shoot = (bool)parameters[1];

		if (rotateLeftIndex == index)
			rotateLeft = (bool)parameters[1];

		if (rotateRightIndex == index)
			rotateRight = (bool)parameters[1];
	}

	IEnumerator CoChargeLaser()
	{
		if (isCharging)
			yield break;

		var animator = GetComponentInChildren<Animator> ();
		float charge = 0;

		while (shoot)
		{
			isCharging = true;

			charge += Time.deltaTime / 2;
			if (animator)
				animator.SetFloat("charge", charge);

			yield return null;
		}

		if (animator)
			animator.SetFloat ("charge", 0);
	}

	void Update()
	{
		var child = transform.GetChild (0);

		// Rotate
		if (rotateLeft)
			child.Rotate (Vector3.forward, -3);

		if (rotateRight)
			child.Rotate (Vector3.forward, 3);

		// Charge
		if (shoot)
		{
			StartCoroutine(CoChargeLaser());
		}

		// Shoot
		if (isCharging && !shoot)
		{
			isCharging = false;
			Instantiate (laserBeamPrefab, transform.GetChild (0).position, transform.GetChild (0).rotation);
			laserParticle.Emit(1);
			rigidbody2D.AddForce (transform.GetChild(0).right * 5);

			audio.PlayOneShot (laserSFX[Random.Range (0, laserSFX.Length)]);
		}
	}
}
