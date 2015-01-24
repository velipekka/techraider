using UnityEngine;
using System.Collections;

public class ManualSortingLayer : MonoBehaviour {

	public Renderer r;
	public string sortingLayerName;
	public int orderInLayer;

	void Update()
	{
		if (r == null)return;
		r.sortingLayerName = sortingLayerName;
		r.sortingOrder = orderInLayer;
	}

}
