using UnityEngine;

public class GerenciadorDeParticulaBehaviourScript : MonoBehaviour
{

    public ParticleSystem particlePrefab; //prefab da particula

    private ParticleSystem[] particlePool = new ParticleSystem[4]; //particle pool controller

    // Use this for initialization
    void Awake()
    {
        Setup();
    }
    // Montagem
    private void Setup()
    {
        for (int i = 0; i < particlePool.Length; i++)
        {
            particlePool[i] = Instantiate(particlePrefab);
            particlePool[i].gameObject.SetActive(false);
            particlePool[i].transform.parent = transform;
            particlePool[i].transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }
    }
    // ativa uma particula disponivel
    public void Play(Vector2 posicao)
    {
        foreach (ParticleSystem particle in particlePool)
        {

            if(particle.time >= particle.duration)
            {
                particle.gameObject.SetActive(false);
            }

            if (particle.isStopped)
            {
                Debug.Log("Tocando");
                particle.gameObject.SetActive(true);               
                break;
            }


        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
