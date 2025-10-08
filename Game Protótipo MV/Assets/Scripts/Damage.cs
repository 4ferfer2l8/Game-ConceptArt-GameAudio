using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int hit)
    {
        currentHealth -= hit;
        Debug.Log(gameObject.name + " tomou dano! Vida: " + currentHealth);

        StartCoroutine(BlinkRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " morreu!");
        Destroy(gameObject); // ou animação de morte
    }
}
