using System.CodeDom;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
	float rotation;
	IEnumerator Start()
	{
		rotation = Random.Range(-1f, 1f);

		rigidbody2D.velocity = Vector2.right*-3;

		yield return new WaitForSeconds(30);
		Destroy(gameObject);
	}

	void Update()
	{
		transform.Rotate(Vector3.forward, rotation);
	}
}
