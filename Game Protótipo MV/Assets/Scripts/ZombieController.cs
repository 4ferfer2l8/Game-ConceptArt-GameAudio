using JetBrains.Annotations;
using System.Threading;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [Header("Atributos")]
    public float speed = 2f;
    public float attackRange = 0.6f;
    public float attackCooldown = 1.2f;
    public int damage = 1;

    private Transform player;
    private Movimenta_Personagem playerController; // Refer�ncia direta ao script do player
    private Animator anim;
    private Rigidbody2D rb;
    private float attackTimer;
    private bool canAttack = true;
    public bool isWalking = false;
    public bool isAttacking = false;    


    float grunidotimer= 0f;
    float tempogrunido = 15f;

    //audio 

    [SerializeField] AudioSource zombiesource;
    [SerializeField] AudioClip somzumbi;
    [SerializeField] AudioClip somataquezumbi;
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        if (player != null)
        {
            playerController = player.GetComponent<Movimenta_Personagem>();
        }

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null || playerController == null)
        {
            // Tenta reencontrar o player se foi perdido
            player = GameObject.FindWithTag("Player")?.transform;
            if (player != null)
            {
                playerController = player.GetComponent<Movimenta_Personagem>();
            }
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            // Movimento em dire��o ao jogador
            //rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            transform.position += (direction * speed * Time.deltaTime);
            //zombiesource.PlayOneShot(somzumbi);

            // Virar o sprite na dire��o do movimento
            if (direction.x != 0)
                transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);

            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            // Parar de andar quando est� no alcance de ataque
            anim.SetBool("isWalking", false);

            // Atacar quando o cooldown permitir
            if (canAttack && attackTimer <= 0f)
            {
                anim.SetBool("isAttacking", true);
                AttackPlayer();
                attackTimer = attackCooldown;
            }
            else
            {
                anim.SetBool("isAttacking", false);
            }
        }

        // Reduzir o timer de cooldown
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
        TocaGrunido();
    }

    void AttackPlayer()
    {
        if (playerController != null)
        {
            // Verifica a dist�ncia novamente no momento do ataque
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= attackRange + 0.2f) // Pequena margem de erro
            {
                playerController.TakeDamage(damage);
               // zombiesource.clip = somataquezumbi;
                //zombiesource.Play();
                Debug.Log("Zumbi atacou o jogador! Dano: " + damage);
            }
        }
    }

    // M�todo chamado pela anima��o (Animation Event)
    public void EnableAttack()
    {
        canAttack = true;
      
    }

    public void DisableAttack()
    {
        canAttack = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void TocaGrunido()
    {

        if (grunidotimer < tempogrunido)
        {
            grunidotimer += Time.deltaTime;
        }
        else
        {
            zombiesource.PlayOneShot(somzumbi);
            grunidotimer = 0f;
        }
    }
}