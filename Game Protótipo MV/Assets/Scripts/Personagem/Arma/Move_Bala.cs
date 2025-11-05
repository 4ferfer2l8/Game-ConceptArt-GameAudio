using UnityEngine;

public class Move_Bala : MonoBehaviour
{

    [SerializeField] float velocidade = 3000f;
    [SerializeField] float lifeTime = 1f;
    Rigidbody2D rb;
    [SerializeField] int damage = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = transform.position.z;
        Vector2 direcao = (mouseWorld - transform.position);
        // Move a bala em direção ao mouse
        rb.linearVelocity = transform.right * velocidade;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Objetos")
        {
           Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Zumbi")
        {
            collision.gameObject.GetComponent<Damage>()?.TakeDamage(1);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            BossController boss = collision.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
                Debug.Log("Boss levou dano de projétil!");
            }

            Destroy(gameObject);
        }
    }
}