using UnityEngine;
using FMODUnity;

public class ZombieAudioManager : MonoBehaviour
{
    [SerializeField] EventReference FootsepEvent;
    [SerializeField] EventReference AttackEvent;
    
    [SerializeField] float rate;
    [SerializeField] GameObject zombie;
    [SerializeField] ZombieController controlador;
    float time;

    void Passos()
    {
        RuntimeManager.PlayOneShotAttached(FootsepEvent, zombie);
    }
    void Attack()
    {
        RuntimeManager.PlayOneShotAttached(AttackEvent, zombie);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (controlador.isWalking)
        {
            if (time >= rate)
            {
                Passos();
                Debug.Log("Zumbi Passos");
                time = 0;
            }
        }
        if (controlador.isAttacking)
        {
            Attack();
        }
    }
}
