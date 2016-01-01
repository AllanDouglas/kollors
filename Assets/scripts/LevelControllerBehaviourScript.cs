using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AudioSource))]

public class LevelControllerBehaviourScript : MonoBehaviour
{
    [Header("Cores")]
    public List<Color> cores; //cores do game
    [Header("Externos")]
    public BarraDeEnergiaBehaviourScript barra; // bara para contagem do tempo
    public SpawnerBehaviourScript spawner; //spawner do level
    public UiInGameBehaviourScript UiInGame; // interface do jogo
    public GerenciadorDeParticulaBehaviourScript particulaDeAcerto; //gerenciador da particula do acerto
    [Header("Controle do nivel")]
    public int nivelMaximo = 1; // nivel maximo
    public int moduladorDoNivel = 5; // controle de pontos para passar de nivel
    public float velocidadeDaBarraPorNivel; // velocidade da queda da barra por nivel
    [Header("Efeitos Sonoros")]
    public AudioClip somAcerto;//efeito do acerto 
    public AudioClip somErro; //efeito do erro

    [HideInInspector]
    public int pontos, combo = 1, record, _nivel = 1;

    private static LevelControllerBehaviourScript Instance = null; // controle de singleton
    private ModeloBehaviourScript _modelo; // instancia do modelo atual para comparação
    private AudioSource _audioSource; // reproduror do som

    private bool nivelStartado = false; // controle da inicialização do nivel
    private float _contagemRegressiva = 5; // contagem regressiva






    // Use this for initialization
    void Start()
    {
        // configura o level
        Setup();
        //inicia o nivel
        //StartLevel();
    }
    // monta o level
    private void Setup()
    {
        this._audioSource = GetComponent<AudioSource>();

        // configura os eventos
        SpawnerBehaviourScript.OnSpawn += CapturarModelo;

        BotaoBehaviourScript.BotaoPressionado += Comparar;

        BarraDeEnergiaBehaviourScript.BarraZerada += BarraZerada;



    }

    public void Restart()
    {

        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        // para a barra
        barra.estaLigada = false;

        // iniciando paramentros
        this._nivel = 1;
        this.pontos = 0;
        this.combo = 0;
        this.barra.Carregar(60);
        this._contagemRegressiva = 5;

        if (_modelo)
        {
            _modelo.gameObject.SetActive(false);
        }
        //reseta as labels
        UiInGame.contagemRegressiva.gameObject.SetActive(true);
        UiInGame.ContagemRegressiva("5");

       

        // status do evel
        nivelStartado = false;

    }

    // gerencia o zerar da barra
    private void BarraZerada()
    {

        barra.estaLigada = false;
        Debug.Log("Barra zerada");
    }

    // captura o modeo atual
    private void CapturarModelo(ModeloBehaviourScript modelo)
    {
        this._modelo = modelo;
    }

    // compara a ação do botão com o bloco vigente
    private void Comparar(BotaoBehaviourScript botao)
    {
        // controle do acerto
        bool acertou = false;

        // verifica a orientação do modelo
        if (_modelo.orientacao == Orientacao.SIMBOLO)
        {
            //... se for SIMBOLO verifica o tipo do botão com o tipo do modelo
            acertou = (botao.tipo == _modelo.tipo);

        }
        else
        {
            //... se for COR verifica a cor do botão com o modelo
            acertou = (botao.cor == _modelo.cor);

        }
        //verifica o acerto
        if (acertou)
        {
            Acerto();
            return;
        }

        Erro();


    }
    // adiciona um ponto
    private void AdicionarPonto()
    {
        AdicionarPontos(1);
    }

    // gerencia o contro dos pontos
    private void AdicionarPontos(int pontos)
    {
        this.pontos += pontos * combo;

        UiInGame.PontosTxt(this.pontos.ToString());

    }

    // gerecia o erro do jogador
    private void Erro()
    {

        this._audioSource.PlayOneShot(somErro);

        // retonar o combo para 1
        combo = 1;

        UiInGame.ComboTxt("1x" + combo.ToString());

        barra.Descarregar(6);

        Debug.Log("O jogador ERROU");
    }
    // gerencia o acerto do Jogador
    private void Acerto()
    {

        // play a particula
        particulaDeAcerto.Play(_modelo.transform.position);
        // efeito sonoro
        this._audioSource.PlayOneShot(somAcerto);
        // adiciona o combo até 4 no maximo
        if (combo < 4)
        {
            combo++;
        }
        UiInGame.ComboTxt("1x" + combo.ToString());
        // executa a animção do acerto
        // axecuta o som do acerto
        // desativa o modelo atual
        _modelo.gameObject.SetActive(false);

        // adiciona pontos

        AdicionarPonto();

        if (pontos % moduladorDoNivel == 0 & _nivel < nivelMaximo)
        {

            LevelUp();
        }

        // coloca outro modelo

        spawner.Spawnar();

        barra.Carregar(15);
        Debug.Log("O jogador ACERTOU");
    }
    // incrementa o nivel
    private void LevelUp()
    {

        _nivel++;
        barra.decrescentePorSegundo += velocidadeDaBarraPorNivel;
        UiInGame.NivelTxt(StringSystem.NIVEL + " " + _nivel);

    }

    // iniciando o nivel
    private void StartLevel()
    {


        // flaga o jogo 
        this.nivelStartado = true;
        // configura as labels do jogo
        UiInGame.PontosTxt(pontos.ToString());
        UiInGame.NivelTxt(StringSystem.NIVEL + " " + _nivel.ToString());
        UiInGame.ComboTxt("1x" + combo.ToString());

        // configura a velocidade da barra
        barra.decrescentePorSegundo = 4;
        // liga a barra 
        barra.estaLigada = true;
        // spawna o primeiro modelo
        spawner.levelController = this;
        spawner.Spawnar();

    }

    // Update is called once per frame
    void Update()
    {
        // exibe a contagem regressiva
        UiInGame.ContagemRegressiva(Math.Round(_contagemRegressiva).ToString());
        //inicia a contagem regressiva
        if (!nivelStartado)
        {
            if (_contagemRegressiva <= 0)
            {
                UiInGame.contagemRegressiva.gameObject.SetActive(false);
                StartLevel();

            }

            _contagemRegressiva -= Time.deltaTime;
        }

    }
}
