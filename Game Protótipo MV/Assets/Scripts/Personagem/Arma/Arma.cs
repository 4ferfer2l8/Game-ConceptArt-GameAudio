using System.Collections;
using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField] GameObject Bala;
    [SerializeField] GameObject canodaarma;
    [SerializeField] AudioSource armasource;
    [SerializeField] float shootDelay = 0.04f;
    [SerializeField] AudioClip[] atiraclip;
    public bool atirou = false;


    //codigo pra arma girar em direção ao mouse
    [SerializeField] GameObject jogador;
    [SerializeField] float raio = 1.5f;

    //cooldown do tiro

    [SerializeField] public float cooldowntiro;
    [SerializeField] public float timertiro;
    [SerializeField] bool podeatirar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 1. Posição do mouse no mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // z fixo para 2D

        // 2. Direção entre jogador e mouse
        Vector3 direcao = (mousePos - jogador.transform.position).normalized;

        // 3. Posição da arma em órbita
        Vector3 posicaoArma = jogador.transform.position + direcao * raio;
        transform.position = posicaoArma;

        
        Vector3 PosMouse = Input.mousePosition;

        PosMouse = Camera.main.ScreenToWorldPoint(PosMouse);

        
        Vector2 diferenca = new Vector2(PosMouse.x - transform.position.x, PosMouse.y - transform.position.y);

        // Mantï¿½m a rotaï¿½ï¿½o calculada e aplica um ajuste de +90 graus no eixo Z
        Quaternion rotBase = Quaternion.LookRotation(Vector3.forward, diferenca);
        float zCorrigido = rotBase.eulerAngles.z + 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, zCorrigido);
       
        if(timertiro>=cooldowntiro)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Atira();
               timertiro = 0f;
            }
        }
        
        timertiro += Time.deltaTime;
    }

    IEnumerator ShootTime()
    {
        yield return new WaitForSeconds(shootDelay);
        atirou = false;
        
    }

    void Atira()
    {
        //armasource.PlayOneShot(atiraclip[Random.Range(0, atiraclip.Length)]);
        Vector3 PosMouse = Input.mousePosition;

        PosMouse = Camera.main.ScreenToWorldPoint(PosMouse);

        Vector2 diferenca = new Vector2(PosMouse.x - transform.position.x, PosMouse.y - transform.position.y);

        // Mantï¿½m a rotaï¿½ï¿½o calculada e aplica um ajuste de +90 graus no eixo Z
        Quaternion rotBase = Quaternion.LookRotation(Vector3.forward, diferenca);
        float zCorrigido = rotBase.eulerAngles.z + 90f;
        Instantiate(Bala, canodaarma.transform.position, this.gameObject.transform.rotation);//Quaternion.Euler(0f, 0f, zCorrigido));
        atirou = true;
    }
}
