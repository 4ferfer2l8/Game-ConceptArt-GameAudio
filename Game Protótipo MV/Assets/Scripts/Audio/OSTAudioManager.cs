using UnityEngine;

public class OSTAudioManager : MonoBehaviour
{
    [SerializeField] Movimenta_Personagem controlador;
    [SerializeField] ContaZumbis contador;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Low HP Filter
        if (controlador.currentHealth <= 5f)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Low HP Percussion", 1f);
        }
        else
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Low HP Percussion", 0f);
        }


        // Game Intencity OST
        if (contador.contadorZumbis == 0)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Game Intencity", 0f);
        }
        else if (contador.contadorZumbis >= 3)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Game Intencity", 1f);
        }
        else if (contador.contadorZumbis >= 5)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Game Intencity", 2f);
        }
    }
}
