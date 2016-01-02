using UnityEngine;

using UnityEngine.SceneManagement;

public class NavegacaoBehaviourScript : MonoBehaviour {

    public static void Carregar(Scene cena)
    {
        SceneManager.LoadScene(cena.name);
    }

    // navega para
    public void IrPara(string cena)
    {
        SceneManager.LoadScene(cena);
                
    }

}
