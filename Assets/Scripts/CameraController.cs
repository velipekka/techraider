using RageEvent;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform followTarget;

	public AnimationCurve speedCurve;
	public bool m_GameOver = false;

    Vector2 vel;
	float time;
	void Update()
	{
		Vector3 target = Vector2.zero;
		foreach (Transform child in followTarget)
			target += child.position;

        target /= followTarget.childCount;

		//Vector2 pos = Vector2.SmoothDamp(transform.position, target, ref vel, .2f);
		//transform.position = new Vector3(pos.x, pos.y, transform.position.z);
		time += Time.deltaTime;
		float speed = speedCurve.Evaluate(time);
		transform.Translate (Vector3.right * speed * Time.deltaTime);

		float padding = 0.1f;
		Rect viewRect = new Rect(-padding, -padding, 1 + padding, 1 + padding);
		Vector2 viewPosition = camera.WorldToViewportPoint(target);
		if (!viewRect.Contains(viewPosition) && !m_GameOver)
		{
			m_GameOver = true;
			EventManager.Trigger("GameOver");
		}
	}

}
