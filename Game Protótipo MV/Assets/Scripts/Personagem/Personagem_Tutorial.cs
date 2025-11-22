using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Personagem_Tutorial : MonoBehaviour
{
    Animator animator;
    GameObject jogador;
    SpriteRenderer sprite;
    [SerializeField] GameObject arma;
    public GameObject Dialogo1;
    public TextMeshProUGUI Texto;
    bool inputmouse;
    bool inputdash;
    bool inputdireita;
    bool inputdireitaalt;
    enum EstadoJogador { estado0, estado1,estado2,estado3,estado4 }
    EstadoJogador estadoAtual = EstadoJogador.estado0;
    void Awake()
    {
        //determinando a variavel que será usada para:
        //fazer as transições entre as animações
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        jogador = this.gameObject;


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jogador.GetComponent<Movimenta_Personagem>().enabled = false;
        arma.SetActive(false);
        sprite.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        inputmouse = Input.GetMouseButton(0);
        inputdash = Input.GetKey(KeyCode.LeftShift);
        inputdireita = Input.GetKey(KeyCode.D);
        inputdireitaalt = Input.GetKey(KeyCode.RightArrow);

        switch (estadoAtual)
        {
            case EstadoJogador.estado0: Estado0(); break;
            case EstadoJogador.estado1: Estado1(); break;
            case EstadoJogador.estado2: Estado2(); break;
            case EstadoJogador.estado3: Estado3(); break;
            case EstadoJogador.estado4: Estado4(); break;
        }

    }

    private void FixedUpdate()
    {
      
    }

    void Estado0()
    {
        //comportamento do estado
        Time.timeScale = 0f;
        Texto.text = "Que estrondo foi esse? Será que minha poção deu certo?";
        Dialogo1.SetActive(true);
        //transição de estado
        if(inputmouse)
        {
          Dialogo1.SetActive(false);
          estadoAtual = EstadoJogador.estado1;
          Time.timeScale = 1f;
        }
        
    }
    void Estado1()
    {
        //comportamento do estado
        transform.position += Vector3.left * 2f * Time.deltaTime;
        animator.Play("Andando_Lado");
        //transição de estado
        if(transform.position.x <= 3.5f)
        {
            estadoAtual = EstadoJogador.estado2;
        }
    }

    void Estado2()
    {
        //comportamento do estado
        Texto.text = "Isso acabou de ficar mais interessante, vou mandar esses Zumbis de volta pra cova. (Aperte o botão esquerdo do mouse para Atirar!)";
        Debug.Log("Entrou estado 2");
        arma.SetActive(true);
        Time.timeScale = 0f;
        animator.Play("Idle_Lado");
        Dialogo1.SetActive(true);
        arma.GetComponent<Arma>().timertiro = arma.GetComponent<Arma>().cooldowntiro; 

        //transição de estado
        if (inputmouse)
        {
            Dialogo1.SetActive(false);
            estadoAtual = EstadoJogador.estado3;
            
        }

    }

    void Estado3()
    {
        //comportamento do estado
        Time.timeScale = 1f;
        arma.SetActive(false);
        animator.Play("Andando_Lado");
        transform.position += Vector3.left * 2f * Time.deltaTime;
        //transição de estado
        if (transform.position.x <= -6.5f)
        {
            estadoAtual = EstadoJogador.estado4;
        }
    }

    void Estado4()
    {
        //comportamento do estado
        Debug.Log("Entrou estado 2");
        Time.timeScale = 0f;
        animator.Play("Idle_Lado");
        Texto.text = " Estou em Perigo! (Aperte Shift + D ou Shift + → para dar dash)";
        Dialogo1.SetActive(true);
        
        //transição de estado
        if (inputdash && (inputdireita||inputdireitaalt))
        { 
            Dialogo1.SetActive(false);
            Time.timeScale = 1f;
            arma.SetActive(true);
            jogador.GetComponent<Movimenta_Personagem>().enabled = true;
            this.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zumbi"))
        {
            Destroy(jogador);
        }
    }
}
