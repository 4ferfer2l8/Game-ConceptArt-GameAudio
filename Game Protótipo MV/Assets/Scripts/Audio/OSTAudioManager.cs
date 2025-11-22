using UnityEngine;

public class OSTAudioManager : MonoBehaviour
{
    [SerializeField] Movimenta_Personagem controlador;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controlador.currentHealth <= 5f)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Low HP Percussion", 1f);
        }
    }
}
