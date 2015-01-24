using UnityEngine;
using System.Collections;

public class PanicGenerator : MonoBehaviour {

    public enum PanicMode { None, LowRes }
    public PanicMode panicMode;

    public RenderTexture renderTex;

    void Update()
    {
        Camera.main.GetComponent<DownscaleImageEffect>().enabled = panicMode == PanicMode.LowRes;

        renderTex.Release();
        renderTex.width = panicMode == PanicMode.LowRes ? 64 : 512;
        renderTex.height = panicMode == PanicMode.LowRes ? 64 :512;

    }


}
