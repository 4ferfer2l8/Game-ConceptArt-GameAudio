using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Movimento_JOgador2 : MonoBehaviour
{

    //componentes
    Animator animator;
    Rigidbody2D rb;
    Collider2D coll;
    SpriteRenderer sprite;
    //audio sources

    //varivaveis movimento base
    [Header("Movimenta��o Base")]
    [SerializeField] float velocidadejogador = 5f;
    Vector3 movimento = new Vector3();
    [SerializeField] AudioSource playersource;
    [SerializeField] AudioClip[] passosclip;
    float timerpassos = 0f;
    float tempopassos = 0.4f;

    //estados do jogador
    enum EstadoJogador { idle, correndo, dash }
    EstadoJogador estadoAtual = EstadoJogador.idle;

    [Header("Dash")]
    [SerializeField] float velocidadedash = 8f;
    float velocidadeatual;
    float timer;
    float tempodash = 0.5f;
    [SerializeField] AudioClip dashclip;
    //Inputs
    bool inputdash;
    float movehorizontalInput;
    float moveverticalInput;

    //variaveis sistema de dano
    float currentHealth;
    bool isInvulnerable = false;
    float invulnerabilityTime = 1f; // tempo de invulnerabilidade em segundos

    void Awake()
    {
        //determinando a variavel que ser� usada para:
        //fazer as transi��es entre as anima��es
        animator = GetComponent<Animator>();
        //aplicar a f�sica
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        sprite = GetComponent<SpriteRenderer>();

        // debug se faltou algo na cena (ajuda a encontrar NRE cedo)
        if (animator == null) Debug.LogWarning("Animator n�o encontrado em " + name);
        if (rb == null) Debug.LogWarning("Rigidbody2D n�o encontrado em " + name);
        if (sprite == null) Debug.LogWarning("SpriteRenderer n�o encontrado em " + name);
        if (playersource == null) Debug.LogWarning("playersource n�o atribu�do em " + name);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocidadeatual = velocidadejogador;
        currentHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //coleta inputs do jogador
        inputdash = Input.GetKey(KeyCode.LeftShift);
        if (estadoAtual != EstadoJogador.dash)
        {
            moveverticalInput = Input.GetAxisRaw("Vertical");
            movehorizontalInput = Input.GetAxisRaw("Horizontal");
        }
    }

    void FixedUpdate()
    {
        movimento = new Vector3(movehorizontalInput, moveverticalInput, 0);
        movimento.Normalize();
        // melhor usar rb.MovePosition para f�sica, mas mantendo comportamento atual:
        transform.position += movimento * velocidadeatual * Time.deltaTime;

        if (movimento.x > 0f)
        {
            sprite.flipX = false;
        }
        else if (movimento.x < 0f)
        {
            sprite.flipX = true;
        }

        switch (estadoAtual)
        {
            case EstadoJogador.idle: Idle(); break;
            case EstadoJogador.correndo: Correndo(); break;
            case EstadoJogador.dash: Dash(); break;
        }
    }

    void Idle()
    {
        //comportamento do estado
        animator.Play("Idle_Frente");

        //transi��es de estado
        if (movimento != Vector3.zero)
        {
            estadoAtual = EstadoJogador.correndo;
        }
    }

    void Correndo()
    {
        //comportamento do estado
        TocaPassos();
        if (movimento.x != 0)
        {
            animator.Play("Andando_Lado");
        }
        else
        {
            if (movimento.y < 0)
            {
                animator.Play("Andando_Frente");
            }
            else if (movimento.y > 0)
            {
                animator.Play("Andando_Costas");
            }
        }

        //transi��es de estado
        if (movimento == Vector3.zero)
        {
            estadoAtual = EstadoJogador.idle;
        }
        else if (inputdash && movimento != Vector3.zero)
        {
            timer = 0f;
            estadoAtual = EstadoJogador.dash;
        }
    }

    void Dash()
    {
        if (playersource != null && dashclip != null)
        {
            playersource.PlayOneShot(dashclip);
        }

        //comportamento do estado
        if (movimento.x != 0)
        {
            animator.Play("Dash_Lateral");
        }
        else
        {
            if (movimento.y < 0)
            {
                animator.Play("Dash_Frente");
            }
            else if (movimento.y > 0)
            {
                animator.Play("Dash_Costas");
            }
        }

        velocidadeatual = velocidadedash;
        timer += Time.fixedDeltaTime;

        if (timer >= tempodash)
        {
            //transi��o do estado
            velocidadeatual = velocidadejogador;
            if (movimento.x != 0)
            {
                estadoAtual = EstadoJogador.correndo;
            }
            else
            {
                estadoAtual = EstadoJogador.idle;
            }
        }
    }

    void TocaPassos()
    {
        if (passosclip == null || passosclip.Length == 0)
            return;

        if (playersource == null)
            return;

        if (timerpassos < tempopassos)
        {
            timerpassos += Time.deltaTime;
        }
        else
        {
            playersource.PlayOneShot(passosclip[Random.Range(0, passosclip.Length)]);
            timerpassos = 0f;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable) return; // ignora se est� no cooldown

        currentHealth -= amount;
        Debug.Log("Player tomou dano! Vida atual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invulnerability());
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    private IEnumerator Invulnerability()
    {
        if (sprite == null)
        {
            // nada a piscar, mas garante reset do estado
            isInvulnerable = true;
            yield return new WaitForSeconds(invulnerabilityTime);
            isInvulnerable = false;
            yield break;
        }

        isInvulnerable = true;

        // piscar vermelho
        float elapsed = 0f;
        while (elapsed < invulnerabilityTime)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }

        sprite.color = Color.white;
        isInvulnerable = false;
    }
}
