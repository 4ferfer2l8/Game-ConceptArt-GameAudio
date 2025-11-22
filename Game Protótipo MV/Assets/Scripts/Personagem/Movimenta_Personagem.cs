using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using System.Collections.Generic;

public class Movimenta_Personagem : MonoBehaviour
{

    //componentes
    Animator animator;
    Rigidbody2D rb;
    Collider2D coll;
    SpriteRenderer sprite;
    //audio sources

    //varivaveis movimento base
    [Header("Movimentação Base")]
    [SerializeField] float velocidadejogador = 5f;
    Vector3 movimento = new Vector3();
    //Old Sound Source
    /*[SerializeField] AudioSource playersource;
    [SerializeField] AudioClip[] passosclip;
    */
    float timerpassos = 0f;
    float tempopassos = 0.4f;

    //Fmod Parameters
    public bool isDashing;
    public bool isHurt;

    //estados do jogador
    public enum EstadoJogador  { idle, correndo, dash } 
    public EstadoJogador estadoAtual { get; private set; } = EstadoJogador.idle ;

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
    public float currentHealth { get; private set; }
    bool isInvulnerable = false;
    float invulnerabilityTime = 1f; // tempo de invulnerabilidade em segundos
    [SerializeField] AudioClip[] machucadoclip;

    void Awake()
    {
        //determinando a variavel que será usada para:
        //fazer as transições entre as animações
        animator = GetComponent<Animator>();
        //aplicar a física
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        sprite = GetComponent<SpriteRenderer>();

       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocidadeatual = velocidadejogador;
        currentHealth = 20;
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
        // melhor usar rb.MovePosition para física, mas mantendo comportamento atual:
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

        //transições de estado
        if (movimento != Vector3.zero)
        {
            estadoAtual = EstadoJogador.correndo;
        }
    }

    void Correndo()
    {
        //comportamento do estado
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

        //transições de estado
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
        //Old Dash sound
        /*
        if (playersource != null && dashclip != null)
        {
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }
        */
        

        //comportamento do estado
        if (movimento.x != 0)
        {
            animator.Play("Dash_Lateral");
            isDashing = true;
        }
        else
        {
            if (movimento.y < 0)
            {
                animator.Play("Dash_Frente");
                isDashing = true;
            }
            else if (movimento.y > 0)
            {
                animator.Play("Dash_Costas");
                isDashing = true;
            }
        }

        velocidadeatual = velocidadedash;
        timer += Time.fixedDeltaTime;

        if (timer >= tempodash)
        {
            //transição do estado
            velocidadeatual = velocidadejogador;
            if (movimento.x != 0)
            {
                isDashing = false;
                estadoAtual = EstadoJogador.correndo;
            }
            else
            {
                isDashing = false;
                estadoAtual = EstadoJogador.idle;
                
            }
        }
    }
    // som de passos antigo
    /*void TocaPassos()
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
    */

    public void TakeDamage(int amount)
    {
        if (isInvulnerable) return; // ignora se está no cooldown

        currentHealth -= amount;
        isHurt = true;
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
            
            //old Hit sound
            /*playersource.clip = machucadoclip[Random.Range(0, machucadoclip.Length)];
            playersource.Play();
            */
        }

        sprite.color = Color.white;
        isInvulnerable = false;
    }
}
