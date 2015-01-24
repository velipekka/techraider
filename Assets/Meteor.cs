using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
	IEnumerator Start()
	{
		rigidbody2D.velocity = Vector2.right * -3;

		yield return new WaitForSeconds(20);
		Destroy(gameObject);
	}
}
