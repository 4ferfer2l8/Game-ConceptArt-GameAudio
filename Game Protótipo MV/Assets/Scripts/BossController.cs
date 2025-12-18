using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public Transform player;
    public GameObject arrowPrefab;
    private BossLifeUI bossLifeUI;

    [Header("Zumbis")]
    public GameObject[] zombiePrefabs;
    public Transform[] spawnPoints;

    private Animator anim;
    public float attackCooldown = 2f;
    public float summonCooldown = 6f;
    public float arrowSpeed = 8f;
    public float arrowSpawnOffset = 0.8f;
    public int maxHealth = 100;
    public int damage = 5;

    public int currentHealth;
    private float attackTimer;
    private float summonTimer;
    public bool isShooting { get; private set; } = false;
    public bool isSummoning { get; private set; } = false;


    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = 100;
        attackTimer = attackCooldown;
        summonTimer = summonCooldown;
    }

    void Start()
    {
        
        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

        bossLifeUI = FindFirstObjectByType<BossLifeUI>();
        if (bossLifeUI != null)
        {
            bossLifeUI.Setup(maxHealth);
        }
    }

    
    void Update()
    {
        if (currentHealth <= 0) return;

        attackTimer -= Time.deltaTime;
        summonTimer -= Time.deltaTime;

        if (attackTimer <= 0f && !isShooting && !isSummoning)
        {
            StartCoroutine(ShootRoutine());
            attackTimer = attackCooldown;
        }

        if (summonTimer <= 0f && !isSummoning && !isShooting)
        {
            StartCoroutine(SummonRoutine());
            summonTimer = summonCooldown;
        }
    }

    IEnumerator ShootRoutine()
    {
        isShooting = true;
        anim.SetBool("isShooting", true);

        yield return new WaitForSeconds(0.3f); // pequeno delay antes de atirar (ajuste conforme sua animação)
        SpawnArrow();

        yield return new WaitForSeconds(0.7f); // tempo total da animação (ajuste)
        anim.SetBool("isShooting", false);
        isShooting = false;
    }

    IEnumerator SummonRoutine()
    {
        isSummoning = true;
        anim.SetBool("isSummoning", true);

        yield return new WaitForSeconds(0.4f); // delay antes de spawnar
        SummonZombie();

        yield return new WaitForSeconds(1f); // duração total da animação
        anim.SetBool("isSummoning", false);
        isSummoning = false;
    }

    public void SummonZombie()
    {
        if (spawnPoints.Length == 0 || zombiePrefabs.Length == 0) return;

        int randomSpawn = Random.Range(0, spawnPoints.Length);
        int randomZombie = Random.Range(0, zombiePrefabs.Length);

        Transform spawn = spawnPoints[randomSpawn];
        GameObject chosenZombie = zombiePrefabs[randomZombie];

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
        if (bossCol && arrowCol)
            Physics2D.IgnoreCollision(bossCol, arrowCol);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
            Die();
    }

    private IEnumerator DamageFlash()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Movimenta_Personagem player = collision.gameObject.GetComponent<Movimenta_Personagem>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

    void Die()
    {
        Debug.Log("Boss morreu!");

        if(bossLifeUI != null)
        {
            bossLifeUI.Hide();
        }
        Destroy(gameObject);
        // animação / drop / abrir caminho etc.
    }
}
