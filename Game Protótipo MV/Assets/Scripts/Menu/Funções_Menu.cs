using UnityEngine;
using UnityEngine.SceneManagement;

public class Funções_Menu : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("FaseInicial");
    }

    public void TelaCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
