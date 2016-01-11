using UnityEngine;
using UnityEngine.UI;

public class SpecialBehaviourScript : MonoBehaviour {

    public Slider barra; // slider da barra
    public float tempoDeDescarga; // tempo que leva para descarregar
    public int cargaMaxima; // carga maxima da barra
    public Color corAtivacaoCamera; // cor da barra quando ela estiver cheia
    //public Image imagemDaBarra; // imagem que representa o carregamento
    public ParticleSystem particulaDeAtivacao; // play quando a barra é ativada
    public Animator cameraAnimator; // camera do game
    public Animator textoAnimatior; // texto da animação

    private Color corPadrao; // cor padrão da camera
    private bool estaAtivo = false;
    private float m_tempo_restante; // tempo restante do calculo
    private float m_velocidade_descarga; // velocidade da descarga da barra
	// Use this for initialization
	void Start () {
        Setup();
	}
    //configurações iniciais
    private void Setup()
    {
        
        barra.maxValue = cargaMaxima;
        m_tempo_restante = tempoDeDescarga;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if (estaAtivo)
        {
            //enquanto ainda tem tempo vai descontando
            if (m_tempo_restante > 0)
            {
                float descarga = (cargaMaxima / tempoDeDescarga) * Time.fixedDeltaTime;

                barra.value -= descarga;

            }
            else // se o tempo acabar desativa a barra
            {
                Desativar();
            }
            
            m_tempo_restante -= Time.fixedDeltaTime;

        }
	}

    //retorna o status atual
    public bool EstaAtiva()
    {
        return estaAtivo;
    }

    //retorna true se o valor da carga maxima é igual ao valor atual da parra
    public bool EstaCheia()
    {
        return barra.value == barra.maxValue;
    }

    public void Desativar()
    {
        estaAtivo = false;
        m_tempo_restante = tempoDeDescarga;
        cameraAnimator.SetBool("special", false);
        textoAnimatior.SetBool("special", false);
    }

    // ativa as funcionalidade    
    public void Ativar(float velocidade)
    {
        Debug.Log("Barrade special ativada");
        estaAtivo = true;
        cameraAnimator.SetBool("special", true);
        textoAnimatior.SetBool("special", true);
        m_velocidade_descarga = velocidade;
        particulaDeAtivacao.Play();
    }
    // carrega a barra
    public void Carregar(int carga)
    {
        barra.value += carga;
        
    }
    //descarrega a barra totalmente
    public void Descarregar()
    {
        Descarregar(barra.maxValue);
    }
    //decarrega uma quantidade da barra
    public void Descarregar(float valor)
    {
        barra.value -= valor;
    }

}
