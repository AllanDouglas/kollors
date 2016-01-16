using UnityEngine;
using System.Collections.Generic;

public class SpawnerBehaviourScript : MonoBehaviour
{
    private const float AJUTES_DE_POSICIONAMENTO = 1.2F; // AJUSTE DE POSICIONAMENTO PARA O SPAWN MULTIPLO
    private const float PRIMEIRA_X_POSICAO = -1.79F; // PRIMEIRA POSICAO ONDE SERÁ SPAWNADA O PRIMEIRO MODELO DO CONJUNTO



    public ModeloBehaviourScript[] modelosPrefabs = new ModeloBehaviourScript[8]; // modelos do nivel
    public bool multiplo = false; // flag que define se o spanw é de 1 ou de 4

    private List<ModeloBehaviourScript> modelosPool = new List<ModeloBehaviourScript>(); // pool de objetos
    [HideInInspector]
    public LevelControllerBehaviourScript levelController = null; //controle do level

    // eventos
    public delegate void Evento(ModeloBehaviourScript modelo);
    public static event Evento OnSpawn; //disparado quando um modelo é spawnado


    // Use this for initialization
    void Awake()
    {
        Setup();
    }
    // monta
    private void Setup()
    {

        //levelController = LevelControllerBehaviourScript.GetInstance();
        int index = 0;
        foreach (ModeloBehaviourScript modeloPrefab in modelosPrefabs)
        {
            ModeloBehaviourScript modelo = Instantiate(modeloPrefab);

            modelosPool.Add(modelo);

            if (multiplo)
            {
                //ajusta o tamanho
                modelo.transform.localScale = new Vector3(modelo.transform.localScale.x / 2, modelo.transform.localScale.y / 2);
            }

            modelo.gameObject.SetActive(false);
            index++;
        }



    }
    // spawna o objeto
    public void Spawnar()
    {
        ModeloBehaviourScript modelo = modelosPool[Random.Range(0, modelosPool.Count)];
        // garante que o objeto esteja ativo ativa o obejto
        modelo.gameObject.SetActive(true);
        modelo.transform.position = transform.position;
        // seta a cor do objeto
        modelo.cor = levelController.cores[Random.Range(0, levelController.cores.Length)];

        if (OnSpawn != null)
        {
            OnSpawn(modelo);
        }

    }

    public ModeloBehaviourScript[] SpawnarConjunto()
    {
        System.Random rnd = new System.Random();
        // vamos embaralhar o ArrayList
        for (int i = 0; i < modelosPool.Count; i++)
        {
            int a = rnd.Next(modelosPool.Count);
            ModeloBehaviourScript temp = modelosPool[i];
            modelosPool[i] = modelosPool[a];
            modelosPool[a] = temp;
        }
        ModeloBehaviourScript[] modelos = modelosPool.GetRange(0, 4).ToArray();
        //controle de posicionamento
        float aux = PRIMEIRA_X_POSICAO;
        foreach (ModeloBehaviourScript modelo in modelos)
        {

            // garante que o objeto esteja ativo ativa o obejto
            modelo.gameObject.SetActive(true);
            //calculo da posicao
            float x = aux;
            modelo.transform.position = new Vector2(x, transform.position.y);
            // seta a cor do objeto
            modelo.cor = levelController.cores[Random.Range(0, levelController.cores.Length)];
            //incrementa
            aux += AJUTES_DE_POSICIONAMENTO;

        }


        // vamos obter as quantidade de
        // que queremos
        return modelos;


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        OnSpawn = null;
    }

}
