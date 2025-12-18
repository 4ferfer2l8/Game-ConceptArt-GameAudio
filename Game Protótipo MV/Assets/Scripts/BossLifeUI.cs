using UnityEngine;
using UnityEngine.UI;

public class BossLifeUI : MonoBehaviour
{
    public Slider slider;

    void Awake()
    {
        gameObject.SetActive(false); // começa escondida
    }

    public void Setup(float maxLife)
    {
        slider.maxValue = maxLife;
        slider.value = maxLife;
        gameObject.SetActive(true);
    }

    public void UpdateLife(float currentLife)
    {
        slider.value = currentLife;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
