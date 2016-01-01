using UnityEngine;

using UnityEngine.SceneManagement;

public class NavegacaoBehaviourScript : MonoBehaviour {

    // navega para
    public void IrPara(string cena)
    {
        SceneManager.LoadScene(cena);
                
    }

}
