using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public float speed;
	public Transform followTarget;

    Vector2 vel;
	void Update()
	{
		/*
		Vector3 target = Vector2.zero;
		foreach (Transform child in followTarget)
			target += child.position;

        target /= followTarget.childCount;

        Vector2 pos = Vector2.SmoothDamp(transform.position, target, ref vel, .2f);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
		 */

		transform.Translate (Vector3.right * speed * Time.deltaTime);
    }

}
