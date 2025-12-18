using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSlider : MonoBehaviour
{
    public Slider slider;
    public Movimenta_Personagem player;

    void Start()
    {
        slider.maxValue = player.currentHealth;
        slider.value = player.currentHealth;
    }

    void Update()
    {
        slider.value = player.currentHealth;
    }
}
