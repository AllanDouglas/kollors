using UnityEngine;
using System.Collections;

public class GerenciadorDeParticulaBehaviourScript : MonoBehaviour {

    public ParticleSystem particlePrefab; //prefab da particula

    private ParticleSystem[] particlePool = new ParticleSystem[4]; //particle pool controller

	// Use this for initialization
	void Start () {
        Setup();	
	}
	// Montagem
    private void Setup()
    {
        for(int i = 0; i < particlePool.Length; i++)
        {
            particlePool[i] = Instantiate(particlePrefab);
            particlePool[i].Stop();            
        }
    }
    // ativa uma particula disponivel
    public void Play(Vector2 posicao)
    {
        foreach(ParticleSystem particle in particlePool)
        {
            if (particle.isStopped)
            {
                particle.transform.position = posicao;
                particle.Play();
                break;
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
