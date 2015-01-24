using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public ShipGenerator shipGenerator;

    Vector2 vel;
	void Update()
    {
        Vector2 target = shipGenerator.slots[0].gameObject.transform.position;

        Vector2 pos = Vector2.SmoothDamp(transform.position, target, ref vel, .2f);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

}
