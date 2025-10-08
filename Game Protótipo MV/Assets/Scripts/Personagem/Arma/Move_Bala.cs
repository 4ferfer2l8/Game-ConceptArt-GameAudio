using UnityEngine;

public class Move_Bala : MonoBehaviour
{

    [SerializeField] float velocidade = 15f;
    [SerializeField] float lifeTime = 1f;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        Vector2 direcao = (mouseWorld - transform.position);
        // Move a bala em direção ao mouse
        rb.linearVelocity = direcao * velocidade;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {






    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zumbi" || collision.gameObject.tag == "Objetos")
        {
           Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Zumbi")
        {
            collision.gameObject.GetComponent<Damage>()?.TakeDamage(1);
        }
    }
}