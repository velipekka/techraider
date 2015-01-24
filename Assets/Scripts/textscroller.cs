using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class textscroller : MonoBehaviour
{
	public string m_Text;
	public Font m_Font;
	public List<Transform> m_Chars;
	private float m_CharWidth = 0.2f;
		
	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < m_Text.Length; i++)
		{
			GameObject go = new GameObject();
			TextMesh tm = go.AddComponent<TextMesh>();
			tm.characterSize = 0.02f;
			tm.text = m_Text[i].ToString();
			tm.font = m_Font;
			go.renderer.material = m_Font.material;
			go.transform.parent = this.transform;
			go.transform.localPosition = new Vector3(m_CharWidth * i, 0f, 0f);
			go.renderer.material.color = new Color(1f, 1f, 1f, 0.3f);
			m_Chars.Add(go.transform);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		int i = 0;
		foreach (Transform t in m_Chars)
		{
			t.Translate(-Time.deltaTime, 0f, 0f);

			if (t.localPosition.x < -(m_Chars.Count + 1f)*m_CharWidth)
				t.localPosition = new Vector3(0f, t.localPosition.y, t.localPosition.z);

			t.localPosition = new Vector3(t.localPosition.x, Mathf.Sin(Time.timeSinceLevelLoad * 5f + (i++) * 0.1f) * 0.6f, t.localPosition.z);
			t.renderer.material.color = new Color(45f / 64f, 22f / 64f, 49f / 64f, (Mathf.Cos(Time.timeSinceLevelLoad * Mathf.PI * 2.26f + i * 0.1f) + 1.5f) * 0.2f);
		}
	}
}
