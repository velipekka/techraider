using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform followTarget;

    Vector2 vel;
	void Update()
    {
        Vector2 target = followTarget.position;

        Vector2 pos = Vector2.SmoothDamp(transform.position, target, ref vel, .2f);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

}
