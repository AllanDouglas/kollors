using UnityEngine;
using UnityEngine.UI;

public class TelaIniciarBehaviourScript : MonoBehaviour {

    public Text startTxt, rankingTxt, shopTxt; // textos dos botões 

	// Use this for initialization
	void Start () {
        Setup();
	}
	// montagem
    private void Setup()
    {

        StringSystem.Idioma = Application.systemLanguage;

        startTxt.text = StringSystem.START;
        rankingTxt.text = StringSystem.RANKING;
        shopTxt.text = StringSystem.SHOP;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
