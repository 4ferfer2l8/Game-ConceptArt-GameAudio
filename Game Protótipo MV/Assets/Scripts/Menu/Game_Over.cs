using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    public GameObject telagameover;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            telagameover.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ReiniciaTutorial()
    {
        SceneManager.LoadScene("FaseInicial");
    }

    public void VaiProMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }

    public void ReiniciaBoss()
    {
               SceneManager.LoadScene("FaseBoss");
    }
}
