using UnityEngine;
using UnityEngine.UI;

public class BossLifeSlider : MonoBehaviour
{
    public Slider slider;
    public BossController boss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.maxValue = boss.currentHealth;
        slider.value = boss.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = boss.currentHealth;
    }
}
