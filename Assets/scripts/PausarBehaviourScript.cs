using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class PausarBehaviourScript : MonoBehaviour {

    public Sprite pausar, play;

    private bool pausado = false;
    private Image _image;

    public void Pausar()
    {
        pausado = !pausado;

        if (pausado)
        {
            this._image.sprite = play;
        }
        else
        {
            this._image.sprite = pausar;
        }

        Time.timeScale= (pausado) ? 0.0f : 1.0f;

        LevelControllerBehaviourScript.ESTA_JOGANDO = !pausado;



    }

	// Use this for initialization
	void Start () {
        this._image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
