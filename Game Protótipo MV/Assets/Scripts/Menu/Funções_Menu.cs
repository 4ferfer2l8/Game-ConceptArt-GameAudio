using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Funções_Menu : MonoBehaviour
{
    public bool isClicking = false;
    public bool Unclicked = false;
    
    public void Jogar()
    {
        SceneManager.LoadScene(1);
        StartCoroutine(ClickTimer());
    }

    public void TelaCreditos()
    {
        SceneManager.LoadScene("Creditos");
        StartCoroutine(ClickTimer());
    }


    public void Sair()
    {
        Application.Quit();
        StartCoroutine(ClickTimer());
    }

    IEnumerator ClickTimer()
    {   
        isClicking = true;
        yield return new WaitForSeconds(0.05f);
        isClicking = false;
        StartCoroutine(UnclickTimer());


    }
    IEnumerator UnclickTimer()
    {   
        Unclicked = true;
        yield return new WaitForSeconds(0.05f);
        Unclicked = false;
    }
}
