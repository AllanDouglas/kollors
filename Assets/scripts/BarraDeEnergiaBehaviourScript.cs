using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]



public class BarraDeEnergiaBehaviourScript : MonoBehaviour
{
    [Tooltip("Quantidade de energia que é decrescida por segundo")]
    public float decrescentePorSegundo; //quantidade que pe decrescido por segundo
    public bool estaLigada = false; //status da barra 

    //public Color cor50Porcento, cor30Porcento, corPadrao;  // cores que representam a energia restante da barra

    //evento de barra zerada
    public delegate void QuandoZerrar();
    public static event QuandoZerrar BarraZerada;

    private Slider _slider; // display da barra
    private float _segundo = 0.1f; // controle do tempo para decrecimo

    // Use this for initialization
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if (_segundo <= 0)
        {
            _segundo = 0.1f;
            
        } 
        _segundo -= Time.deltaTime;
        */

        if (estaLigada)
            Descarregar(Time.fixedDeltaTime * decrescentePorSegundo);

    }
    //decrementa um valor da barra
    public void Descarregar(float valor)
    {
        _slider.value -= valor;

        if (_slider.value <= 0 & BarraZerada != null)
        {
            BarraZerada();
        }

    }
    //incrementa um valor
    public void Carregar(int valor)
    {
        _slider.value += valor;
    }


}
