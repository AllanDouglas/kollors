using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiTutorialBehaviourScript : MonoBehaviour {

    public Text dicaDaForma, dicaDaCor; // dicas
    public bool pausaOJogo = false; // caso true para o até fechar

    public delegate void Evento();
    public static event Evento Fechado; // disparado quando fechamos a sala

	// Use this for initialization
	void Start () {
     
        dicaDaCor.text = StringSystem.DICA_COR;
        dicaDaForma.text = StringSystem.DICA_FORMA;

        if (pausaOJogo)
        {
            Time.timeScale = 0.0f;
        }

	}

    public void Fechar()
    {

        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        if(Fechado != null) Fechado();
    }

    public void Abrir()
    {

        if (pausaOJogo)
        {
            Time.timeScale = 0.0f;
        }
        gameObject.SetActive(true);
        
    }


    // Update is called once per frame
    void Update () {
	
	}
}
