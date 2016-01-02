using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
[RequireComponent(typeof(Image))]

public class BotaoBehaviourScript : MonoBehaviour {

    public Sprite sprite; //sprite do botão que vai corresponder ao modelo
    public ModeloBehaviourScript.Tipo tipo; // tipo do modelo correspondente

    //evento 
    public delegate void Evento(BotaoBehaviourScript botao);
    public static event Evento BotaoPressionado; // disparado quando o botão é precioando

    public Color cor // definição da cor do botão
    {
        set
        {
            this._cor = value;
            this._imagem.color = this._cor;
        }

        get
        {
            return this._cor;
        }
    }

    private Color _cor; //cor da imagem
    private Image _imagem; //imagem do botão
    private Button _botao; // botão ;)
    
    void Awake()
    {
        // configura os componentes
        this._imagem = GetComponent<Image>();
        this._botao = GetComponent<Button>();
    }

	// Use this for initialization
	void Start () {

        // configura a imagem do botão 
        this._imagem.sprite = sprite;
        // configura a ação do pressionamento do botão
        _botao.onClick.AddListener(delegate { Pressionar(); });
	}

    // controla o disparo do botão
    private void Pressionar()
    {
        BotaoPressionado(this);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        BotaoPressionado = null;
    }
}
