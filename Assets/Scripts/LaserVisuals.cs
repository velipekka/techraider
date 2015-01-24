using UnityEngine;
using System.Collections;

public class LaserVisuals : MonoBehaviour {

    public ParticleSystem chargeParticle;

    void Update()
    {
        chargeParticle.emissionRate = GetComponent<Animator>().GetFloat("charge") * 200f;
    }

}
