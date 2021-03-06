﻿using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class LevelControllerBehaviourScript :  MonoBehaviour
{
    public enum Modo {Normal , Rush };
    [Header("Modo do jogo")]
    public Modo modo = Modo.Normal; // modo do jogo
    [Header("Cores")]
    public Color[] cores; //cores do game
    [Header("Externos")]
    public BarraDeEnergiaBehaviourScript barra; // bara para contagem do tempo
    public SpawnerBehaviourScript spawner; //spawner do level
    public UiInGameBehaviourScript UiInGame; // interface do jogo
    public GerenciadorDeParticulaBehaviourScript particulaDeAcerto; //gerenciador da particula do acerto
    public GameOverUIBehaviourScript GameOverUi; //interface do gameover
    public LevelUpAnimationBehaviourScript LevelUPAnimation; // interação do nivel 
    public UiTutorialBehaviourScript UiTutorial; // interface de tutorial
    public NovoRecordBehaviourScript UiNovoRecord; // interface do novo record 
    public SpecialBehaviourScript barraSpecial; // barra que marca o special
    public ParticleSystem particulaBackGround; // particula de fundo

    [Header("Controle do nivel")]
    public int nivelMaximo = 1; // nivel maximo
    public int moduladorDoNivel = 5; // controle de pontos para passar de nivel
    public float velocidadeDaBarraPorNivel; // velocidade da queda da barra por nivel
    [Header("Controle do special")]
    public int restauracaoBarra; // quanto a barra de energia é restaurada
    [Header("Efeitos Sonoros")]
    public AudioClip somAcerto;//efeito do acerto 
    public AudioClip somErro; //efeito do erro
    public AudioClip levelUp; //  level up
    public AudioClip somNovoRecord;// novo record
    public AudioClip somSpecial; // som da ativação do Special

    [HideInInspector]
    public int pontos, combo = 1, record, _nivel = 1;

    private ModeloBehaviourScript _modelo; // instancia do modelo atual para comparação
    private AudioSource _audioSource; // reproduror do som
    private int _acertos_seguidos = 0, _maximo_acertos_seguidos; // quantidade de acertos seguidos na partida
    private bool nivelStartado = false; // controle da inicialização do nivel
    private float _contagemRegressiva = 3; // contagem regressiva
    private int _moduladorDoNivel; //controla quantos pontos são nescessários para passar de nivel
    public static bool ESTA_JOGANDO = true; //status do game

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
        
        // garante que o jogo esteja ativo
        Time.timeScale = 1.0f;

        //desativa a interface de tutorial
        UiTutorial.gameObject.SetActive(false);
        // verifica se o tutorial já foi visto
        if (PlayerPrefs.GetInt("_tutorial_") == 0)
        {
            Time.timeScale = 0.0f;
            UiTutorial.gameObject.SetActive(true);
        }


        // cotrolador do nivel
        _moduladorDoNivel = moduladorDoNivel;

        //recupera o record anterior
        record = PlayerPrefs.GetInt("_record_");
        //captura a fonte de audio
        this._audioSource = GetComponent<AudioSource>();
        // configura as labels do jogo
        UiInGame.PontosTxt(pontos.ToString());
        UiInGame.NivelTxt(StringSystem.NIVEL + " " + _nivel.ToString());
        UiInGame.ComboTxt("1x1");
        // configura os eventos       
        SpawnerBehaviourScript.OnSpawn += this.CapturarModelo;
        BotaoBehaviourScript.BotaoPressionado += this.Comparar;
        BarraDeEnergiaBehaviourScript.BarraZerada += this.BarraZerada;
        UiTutorialBehaviourScript.Fechado += TutorialFechado;
    }

    private void TutorialFechado()
    {
        Debug.Log("Tutorial Fechado");
        // seta a configuração de tutorial
        PlayerPrefs.SetInt("_tutorial_", 1);

    }
    //reinicia o nivel
    public void Restart()
    {

        // remove os elementos
        SpawnerBehaviourScript.OnSpawn -= this.CapturarModelo;
        BotaoBehaviourScript.BotaoPressionado -= this.Comparar;
        BarraDeEnergiaBehaviourScript.BarraZerada -= this.BarraZerada;
        NavegacaoBehaviourScript.Carregar(SceneManager.GetActiveScene());

        //remove a interface de gameover
        GameOverUi.gameObject.SetActive(false);

        //reativa a ui in game
        UiInGame.gameObject.SetActive(true);

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
    //salva a pontuação do celular
    private void GravarPontuacao()
    {

        int _record = PlayerPrefs.GetInt("_record_");

        if (_record < pontos)
        {
            PlayerPrefs.SetInt("_record_", pontos);
            record = pontos;
        }



    }
    //game over
    private void GameOver()
    {

        GravarPontuacao();

        _modelo.gameObject.SetActive(false);

        UiInGame.gameObject.SetActive(false);
        GameOverUi.gameObject.SetActive(true);

        GameOverUi.PontosTxt(pontos.ToString());
        GameOverUi.RecordTxt(record.ToString());
        GameOverUi.AcertosTxt(_maximo_acertos_seguidos.ToString());

        GameOverUi.gameObject.SetActive(true);
        barra.estaLigada = false;

    }

    // gerencia o zerar da barra
    private void BarraZerada()
    {
        Debug.Log("Barra zerada");
        GameOver();
    }

    // captura o modeo atual
    private void CapturarModelo(ModeloBehaviourScript modelo)
    {
        this._modelo = modelo;
    }

    // compara a ação do botão com o bloco vigente
    private void Comparar(BotaoBehaviourScript botao)
    {
        // verifica se o jogo está rodando
        if (ESTA_JOGANDO == false) return;

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
        if (barraSpecial.EstaAtiva() == false)
            AdicionarPontos(1);
        else
            AdicionarPontos(2);
    }

    // gerencia o contro dos pontos
    private void AdicionarPontos(int pontos)
    {
        this.pontos += pontos * combo;


        if (this.pontos > record & !UiNovoRecord.gameObject.activeSelf)
        {
            UiNovoRecord.gameObject.SetActive(true);
            this._audioSource.PlayOneShot(somNovoRecord);
        }

        UiInGame.PontosTxt(this.pontos.ToString());



    }

    // gerecia o erro do jogador
    private void Erro()
    {
        //verifica se os acertos seguidos foram maior do que até i ultimo acerto
        if(_acertos_seguidos > _maximo_acertos_seguidos)
        {
            _maximo_acertos_seguidos = _acertos_seguidos;
        }
        _acertos_seguidos = 0;

        //reproduz o som do erro
        this._audioSource.PlayOneShot(somErro);

        // retonar o combo para 1
        combo = 1;

        UiInGame.ComboTxt("1x" + combo.ToString());
        //se errar zera a barra de special
        barraSpecial.Descarregar();
        /*
        se a barra especial estava ativa zera a barra de tempo
        if (barraSpecial.EstaAtiva())
        {
            barra.Descarregar(60);
        }
        */

        barra.Descarregar(6);

        Debug.Log("O jogador ERROU");
    }
    // gerencia o acerto do Jogador
    private void Acerto()
    {
        //adiciona o acerto seguido
        _acertos_seguidos++;
        //troca a cor da particula de fundo
        Color nova_cor = _modelo.cor;
        nova_cor.a = 0.3f;
        particulaBackGround.startColor = nova_cor;

        // adiciona o combo até 4 no maximo
        if (combo < 4)
        {
            combo++;
        }
        //a cada acerto carrega um pouco a barra de special
        if (barraSpecial.EstaAtiva() == false)
        {
            UiInGame.ComboTxt("1x" + combo.ToString());
            barraSpecial.Carregar(1);

            if (barraSpecial.EstaCheia())
            {
                this._audioSource.PlayOneShot(somSpecial);
                barraSpecial.Ativar(10);
            }

        }
        else
        {// se a barra esta ativa
            UiInGame.ComboTxt("2x" + combo.ToString());
            barra.Carregar(restauracaoBarra); // carrega mais 5 pontos de energia se a barra de special está ativa
        }
        //calcula o nivel
        _moduladorDoNivel--;
        if (_moduladorDoNivel == 0 & _nivel < nivelMaximo)
        {
            _moduladorDoNivel = moduladorDoNivel;
            LevelUp();
        }

        // play a particula
        particulaDeAcerto.Play(_modelo.transform.position);
        // efeito sonoro
        this._audioSource.PlayOneShot(somAcerto);
       
    
        // executa a animção do acerto
        // axecuta o som do acerto
        // desativa o modelo atual
        _modelo.gameObject.SetActive(false);

        // adiciona pontos

        AdicionarPonto();

        // coloca outro modelo

        spawner.Spawnar();

        barra.Carregar(15);
        Debug.Log("O jogador ACERTOU");
    }
    // incrementa o nivel
    private void LevelUp()
    {

        Debug.Log("######## LevelUp #########");

        //reproduz o som
        this._audioSource.PlayOneShot(levelUp);
        // ativa a animação
        this.LevelUPAnimation.gameObject.SetActive(true);

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
        UiInGame.ComboTxt("1x1");

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
        _contagemRegressiva -= Time.deltaTime;
        //inicia a contagem regressiva
        if (!nivelStartado)
        {
            if (_contagemRegressiva <= 0)
            {

                UiInGame.contagemRegressiva.gameObject.SetActive(false);
                StartLevel();

            }


        }


        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        /*
        if (Input.acceleration.sqrMagnitude > 2 & barraSpecial.EstaCheia() & )
        {
            this._audioSource.PlayOneShot(somSpecial);
            barraSpecial.Ativar(10);
        }
        */

    }
    // quando 
    void OnDestroy()
    {
        SpawnerBehaviourScript.OnSpawn -= this.CapturarModelo;

        BotaoBehaviourScript.BotaoPressionado -= this.Comparar;

        BarraDeEnergiaBehaviourScript.BarraZerada -= this.BarraZerada;
        UiTutorialBehaviourScript.Fechado -= this.TutorialFechado;
    }
}
