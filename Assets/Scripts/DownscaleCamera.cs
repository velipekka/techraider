using UnityEngine;
using System.Collections;

public class DownscaleCamera : MonoBehaviour {

	
    void Update()
    {
        camera.orthographicSize = Camera.main.orthographicSize;
        camera.rect = new Rect(0f, 0f, 1f, (float)Screen.height / (float)Screen.width);
    }


}
