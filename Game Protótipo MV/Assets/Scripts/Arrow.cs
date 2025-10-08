using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 1;          // dano que a flecha causa
    public float lifeTime = 3f;     // tempo at� sumir sozinha

    private void Start()
    {
        // destr�i a flecha depois de X segundos
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           // other.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Objetos")) 
        {
            Destroy(gameObject);
            return;
        }


    }

}
