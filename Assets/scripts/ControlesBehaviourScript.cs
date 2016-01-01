using UnityEngine;
using System.Collections;

public class ControlesBehaviourScript : MonoBehaviour {

    public BotaoBehaviourScript[] botoes; // botoes do controle


    public LevelControllerBehaviourScript levelControle; //controlador do nivel

	// Use this for initialization
	void Start () {
        Setup();
	}
    // montagem do controle 
    private void Setup()
    {
        
        int index = 0;
        // mapeia as cores de acordo com as cores do nivel
        foreach(Color cor in levelControle.cores)
        {
            botoes[index].cor = cor;
            index++;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
