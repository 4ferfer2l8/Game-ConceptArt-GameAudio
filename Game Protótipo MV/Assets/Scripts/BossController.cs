using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player; // referência ao player
    public GameObject arrowPrefab; // projétil da balestra

    [Header("Zumbis")]
    public GameObject[] zombiePrefabs; // <<< array com os 3 tipos de zumbi
    public Transform[] spawnPoints; // locais possíveis para spawn de zumbis

    private Animator anim;
    public float attackCooldown = 2f;
    public float summonCooldown = 6f;
    public float arrowSpeed = 8f;
    public float arrowSpawnOffset = 0.8f; // distância para spawnar a flecha fora do boss
    public int maxHealth = 100;
    public int damage = 5;

    private int currentHealth;
    private float attackTimer;
    private float summonTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        attackTimer = attackCooldown;
        summonTimer = summonCooldown;

        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (currentHealth <= 0) return; // morto, não faz nada

        attackTimer -= Time.deltaTime;
        summonTimer -= Time.deltaTime;

        // ataque de balestra
        if (attackTimer <= 0f)
        {
            StartShooting();
            attackTimer = attackCooldown;
        }

        // invocar zumbis
        if (summonTimer <= 0f)
        {
            StartSummon();
            summonTimer = summonCooldown;
        }
    }

    void StartShooting()
    {
        if (anim != null)
        {
            anim.SetTrigger("Shoot");
        }
    }

    void StartSummon()
    {
        if (anim != null)
        {
            anim.SetTrigger("Summon");
        }
    }

    // Chamado pelo evento de animação do boss (Summon)
    public void SummonZombie()
    {
        if (spawnPoints.Length == 0 || zombiePrefabs.Length == 0) return;

        // escolhe ponto aleatório
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        Transform spawn = spawnPoints[randomSpawn];

        // escolhe tipo de zumbi aleatório
        int randomZombie = Random.Range(0, zombiePrefabs.Length);
        GameObject chosenZombie = zombiePrefabs[randomZombie];

        // instancia o zumbi
        Instantiate(chosenZombie, spawn.position, Quaternion.identity);
    }

    public void SpawnArrow()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        Vector3 spawnPos = transform.position + (Vector3)dir * arrowSpawnOffset;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
        GameObject arrow = Instantiate(arrowPrefab, spawnPos, Quaternion.Euler(0f, 0f, angle));

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = dir * arrowSpeed;
            rb.gravityScale = 0f;
        }

        Collider2D bossCol = GetComponent<Collider2D>();
        Collider2D arrowCol = arrow.GetComponent<Collider2D>();
        if (bossCol != null && arrowCol != null)
        {
            Physics2D.IgnoreCollision(bossCol, arrowCol);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }

    void Die()
    {
        Debug.Log("Boss morreu!");
        // animação / drop / abrir caminho etc.
    }
}
