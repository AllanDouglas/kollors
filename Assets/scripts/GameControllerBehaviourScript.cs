using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameControllerBehaviourScript : MonoBehaviour {

    [Header("Level")]
    public LevelControllerBehaviourScript level;

    [Header("Botão de pause")]
    public Sprite PauseSprite;
    public Sprite PlaySprite;
    public Image ImagemBtPause;

    [Header("Botão de mute")]
    public Sprite MuteSprite;
    public Sprite SomSprite;
    public Image ImagemBtSom;


    private bool mute = false; // flag do som

    private bool pausado = false; // flag do status do game

    public void Pausar()
    {
        pausado = !pausado;

        if (pausado)
        {
            this.ImagemBtPause.sprite = PauseSprite;
        }
        else
        {
            this.ImagemBtPause.sprite = PlaySprite;
        }

        Time.timeScale = (pausado) ? 0.0f : 1.0f;

        LevelControllerBehaviourScript.ESTA_JOGANDO = !pausado;

    }

    public void Mute()
    {
        mute = !mute;
        if (!mute) {            
            AudioListener.volume = 1;
            this.ImagemBtSom.sprite = SomSprite;
        }
        else
        {   
            AudioListener.volume = 0;
            this.ImagemBtSom.sprite = MuteSprite;
        }

    }

    public void Restart()
    {
        NavegacaoBehaviourScript.Carregar(SceneManager.GetActiveScene());
    }

}
