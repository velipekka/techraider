using UnityEngine;
using System.Collections;

public class DownscaleImageEffect : ImageEffectBase {

    public RenderTexture renderTex;
    public float panicOffset;
    Material m;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(m == null) m = new Material(shader);
        m.SetTexture("_RenderTex", renderTex);
        m.SetFloat("_Offset", panicOffset);
        Graphics.Blit(source, destination, m);

    }
}
