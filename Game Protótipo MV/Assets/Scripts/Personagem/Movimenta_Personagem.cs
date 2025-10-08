using UnityEngine;
using UnityEngine.Playables;

public class Movimenta_Personagem : MonoBehaviour
{

    //componentes
    
    Animator animator;
    Rigidbody2D rb;
    Collider2D coll;
    SpriteRenderer sprite;

    //varivaveis movimento base
    [Header("Movimentação Base")]
    [SerializeField] float velocidadejogador = 5f;
    Vector3 movimento = new Vector3();
    enum EstadoJogador { idle, correndo, dash }
    EstadoJogador estadoAtual = EstadoJogador.idle;

    [Header("Dash")]
    [SerializeField] float velocidadedash = 8f;
    float velocidadeatual;
    float timer;
    float tempodash = 0.5f;
    //Inputs
    bool inputdash;
    float movehorizontalInput;
    float moveverticalInput;

    void Awake()
    {
        //determinando a variavel que ser� usada para:
        //fazer as transições entre as anima��es
        animator = GetComponent<Animator>();
        //aplicar a física
        rb = GetComponent<Rigidbody2D>();

        sprite = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocidadeatual = velocidadejogador;
    }

    // Update is called once per frame
    void Update()
    {
        //coleta inputs do jogador
        inputdash = Input.GetKey(KeyCode.LeftShift);
       if(estadoAtual != EstadoJogador.dash)
       {
            moveverticalInput = Input.GetAxisRaw("Vertical");
            movehorizontalInput = Input.GetAxisRaw("Horizontal");
       }

        
       
    }

    void FixedUpdate()
    {
        movimento = new Vector3(movehorizontalInput, moveverticalInput, 0);
        movimento.Normalize();
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
            if(movimento.y < 0)
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
            //transição do estado
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
}
