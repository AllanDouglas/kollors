using UnityEngine;
using System.Collections;


public enum Orientacao { SIMBOLO, COR }

[RequireComponent(typeof(SpriteRenderer))]

public class ModeloBehaviourScript : MonoBehaviour
{

    //Tipos possiveis de modelos

    public enum Tipo { TIPO_1, TIPO_2, TIPO_3, TIPO_4 };

    public Orientacao orientacao; // oritentação do modelo
    public Tipo tipo; //tipo do modelo

    public Color cor  // definição da cor do modelo 
    {
        set // seta a cor do modelo
        {
            this._cor = value;
            this._spriteRenderer.color = this._cor;
        }
        get
        {
            return this._cor;
        }
    }

    private Color _cor; // cor do modelo
    private SpriteRenderer _spriteRenderer;

    // primeiro metodo
    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
