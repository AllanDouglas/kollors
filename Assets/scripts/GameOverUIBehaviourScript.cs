using UnityEngine;
using UnityEngine.UI;

public class GameOverUIBehaviourScript : MonoBehaviour {

    
    public Text lbPontos, lbRecord, lbAcertos, txtRecord, txtPontos, txtAcertos, lbRanking, lbJogar; //labels 

	// Use this for initialization
	void Start () {
        Setup();
	}
	
    private void Setup()
    {
        // traduções
        lbPontos.text = StringSystem.PONTOS;
        lbRecord.text = StringSystem.RECORD;
        lbJogar.text = StringSystem.START;
        lbRanking.text = StringSystem.RANKING;
    }

    // configura o record
    public void RecordTxt(string str)
    {
        txtRecord.text = str;
    }

    // configura os pontosx
    public void PontosTxt(string str)
    {
        txtPontos.text = str;
    }

    // configura os acertos
    public void AcertosTxt(string str)
    {
        txtAcertos.text = str;
    }

}
