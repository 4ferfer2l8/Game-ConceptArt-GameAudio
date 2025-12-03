using UnityEngine;

public class OSTAudioManager : MonoBehaviour
{
    [SerializeField] Movimenta_Personagem controlador;
    [SerializeField] ContaZumbis contador;
    [SerializeField] GameObject PauseMenu;
    void Start()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("GameState", "Resumed");
    }

    // Update is called once per frame
    void Update()
    {

        // Low HP Filter
        if (controlador.currentHealth <= 5f && PauseMenu.activeSelf == false)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Low HP", "Low HP");
        }
        else
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Low HP", "Normal");
        }

        if(contador == null)
        {
            return;
        }
        // Game Intencity OST
        if (contador.contadorZumbis == 0)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Game Intencity", 0f);
        }
        else if (contador.contadorZumbis >= 2)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Game Intencity", 1f);
        }
        else if (contador.contadorZumbis >= 5)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Game Intencity", 2f);
        }
    }
}
