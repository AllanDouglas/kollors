using UnityEngine;
using System.Collections;
using UnityEngine.UI;   

public class UiInGameBehaviourScript : MonoBehaviour {

    public Text pontos, combo, nivel, contagemRegressiva; // labels 
    
    // configura o nivel
    public void NivelTxt(string nivel)
    {
        this.nivel.text = nivel; 
    }
    
    // configura o combo
    public void ComboTxt(string combo)
    {
        this.combo.text = combo;
    }

    // configura o combo
    public void ContagemRegressiva(string combo)
    {
        this.contagemRegressiva.text = combo;
    }

    //configura os pontos 
    public void PontosTxt(string pontos)
    {
        this.pontos.text = pontos;
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
