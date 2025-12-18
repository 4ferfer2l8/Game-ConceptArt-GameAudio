using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeUI : MonoBehaviour
{
    public Image fillimage;
    public Movimenta_Personagem player;
    public float maxLife = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateLife();    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLife();
    }

    void UpdateLife()
    {
        fillimage.fillAmount = player.currentHealth / maxLife;
    }
}
