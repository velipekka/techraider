using UnityEngine;
using System.Collections;

public class starfield : MonoBehaviour {

    public int starCount = 10000;
    public float sizeMin = .15f;
    public float sizeMax = 2f;

    public Gradient colorSpace;
    public Rect spaceBounds;

    ParticleSystem.Particle[] stars;

	void Start()
    {
        stars = new ParticleSystem.Particle[starCount];

        for (int i = 0; i < starCount; i++)
        {
            ParticleSystem.Particle p = new ParticleSystem.Particle();
            p.position = new Vector2(Random.Range(spaceBounds.x, spaceBounds.x + spaceBounds.width), Random.Range(spaceBounds.y, spaceBounds.y + spaceBounds.height));
            p.color = colorSpace.Evaluate(Random.Range(0f, 1f));
            p.lifetime = .5f;
            p.startLifetime = 1f;
            p.size = Random.Range(sizeMin, sizeMax);
            p.velocity = Vector3.zero;
            p.rotation = Random.Range(0f, 360f);

            stars[i] = p;

        }

        GetComponent<ParticleSystem>().Stop();
        GetComponent<ParticleSystem>().SetParticles(stars, starCount);

        lastCameraPos = Camera.main.transform.position;
    }

    Vector2 lastCameraPos;
    void Update()
    {
        Vector2 delta = (Vector2)Camera.main.transform.position - lastCameraPos;

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].position += (Vector3)(delta * (1f - Mathf.InverseLerp(sizeMin, sizeMax, stars[i].size)));
        }

        GetComponent<ParticleSystem>().SetParticles(stars, starCount);


        lastCameraPos = Camera.main.transform.position;
    }

}
