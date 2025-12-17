using UnityEngine;

public class Menu_de_Pause : MonoBehaviour
{
    [SerializeField] GameObject menudepause;
    [SerializeField] GameObject opçoes;
    [SerializeField] GameObject arma;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.Escape))
       {
            Time.timeScale = 0;
            Pause();
        }
        if (opçoes.activeSelf == true || menudepause.activeSelf == true)
        {
            arma.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            arma.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    
   public void BVoltar()
    {
        Time.timeScale = 1;
        menudepause.SetActive(false);
    }
    public void Pause()
    {
        menudepause.SetActive(true);
        if (opçoes != null)
        {
            opçoes.SetActive(false);
        }
    }

    public void Opçoes()
    {
        opçoes.SetActive(true);
    }
}
