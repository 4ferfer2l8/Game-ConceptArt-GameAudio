using UnityEngine;

public class Menu_de_Pause : MonoBehaviour
{
    [SerializeField] GameObject menudepause;
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
            menudepause.SetActive(true);
        }
    }
    
   public void BVoltar()
    {
        Time.timeScale = 1;
        menudepause.SetActive(false);
    }
}
