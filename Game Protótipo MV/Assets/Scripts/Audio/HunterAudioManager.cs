using UnityEngine;
using FMODUnity;

public class HunterAudioManager : MonoBehaviour
{
    [SerializeField] EventReference FootsepEvent;
    [SerializeField] EventReference AttackEvent1;
    [SerializeField] EventReference SummonEvent;
    
    [SerializeField] float rate;
    [SerializeField] GameObject boss;
    [SerializeField] BossController controlador;
    float time;
    void Start()
    {

    }
    void Passos()
    {
        RuntimeManager.PlayOneShotAttached(FootsepEvent, boss);
    }
    void Attack()
    {
        RuntimeManager.PlayOneShotAttached(AttackEvent1, boss);
    }
    void HeavyAttack()
    {
        RuntimeManager.PlayOneShotAttached(AttackEvent1, boss);
    }
    void Summon()
    {
        RuntimeManager.PlayOneShotAttached(SummonEvent, boss);
    }

    // Update is called once per frame
    void Update()
    {
        if (controlador.isShooting)
        {
            Attack();
        }
        if (controlador.isSummoning)
        {
            Summon();
        }
    }
}
