using UnityEngine;

public class SpawnerBehaviourScript : MonoBehaviour
{

    public ModeloBehaviourScript[] modelosPrefabs = new ModeloBehaviourScript[8]; // modelos do nivel

    private ModeloBehaviourScript[] modelosPool = new ModeloBehaviourScript[8]; // pool de objetos
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
            modelosPool[index] = Instantiate(modeloPrefab);
            modeloPrefab.gameObject.SetActive(false);
            index++;
        }

    }
    // spawna o objeto
    public void Spawnar()
    {
        ModeloBehaviourScript modelo = modelosPool[Random.Range(0, modelosPool.Length)];
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

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        OnSpawn = null;
    }

}
