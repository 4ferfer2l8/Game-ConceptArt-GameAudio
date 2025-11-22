using UnityEngine;
using FMODUnity;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] EventReference FootsepEvent;
    [SerializeField] EventReference ShootEvent;
    [SerializeField] EventReference HitEvent;
    [SerializeField] EventReference DashEvent;
    [SerializeField] float rate;
    [SerializeField] GameObject player;
    float time;
    [SerializeField] Movimenta_Personagem controlador;
    [SerializeField] Personagem_Tutorial personagemTutorial;
    Movimenta_Personagem.EstadoJogador lastState;
    [SerializeField] Arma controlaArma;   
    void Start()
    {
        
    }
    void Passos()
    {
        RuntimeManager.PlayOneShotAttached(FootsepEvent, player);
    }
    void Dash()
    {
        RuntimeManager.PlayOneShotAttached(DashEvent, player);
    }
    public void Shoot()
    {
        RuntimeManager.PlayOneShotAttached(ShootEvent, player);
    }
    public void Hit()
    {
        RuntimeManager.PlayOneShotAttached(HitEvent, player);
    }
    public void TutorialShoot()
    {
        RuntimeManager.PlayOneShotAttached(ShootEvent, player);
    }
    public void TutorialPassos()
    {
        RuntimeManager.PlayOneShotAttached(FootsepEvent, player);
    }
    void Update()
    {
        player.TryGetComponent<Personagem_Tutorial>(out personagemTutorial);
        time += Time.deltaTime;
        if (controlador.estadoAtual == Movimenta_Personagem.EstadoJogador.correndo)
        {
            if (time >= rate)
            {
                Passos();
                time = 0;
            }
        }
        if (controlador.estadoAtual == Movimenta_Personagem.EstadoJogador.dash && lastState != Movimenta_Personagem.EstadoJogador.dash)
        {
            Dash();
        }
        lastState = controlador.estadoAtual;
        if (controlaArma.atirou)
        {
            Shoot();
            controlaArma.atirou = false;
            Debug.Log("som do tiro parou");
        }
        if (controlador.isHurt)
        {
            Hit();
            controlador.isHurt = false;  
        }
        if (personagemTutorial !=null)
        {
            if (personagemTutorial.atirou)
            {
                TutorialShoot();
                personagemTutorial.atirou = false;
            }
            if (personagemTutorial.estadoAtual == Personagem_Tutorial.EstadoJogador.estado1 || personagemTutorial.estadoAtual == Personagem_Tutorial.EstadoJogador.estado3)
            {
                if (time >= rate)
                {
                    TutorialPassos();
                    time = 0;
                }
            }
        }
    }
}
