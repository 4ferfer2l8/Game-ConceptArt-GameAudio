using UnityEngine;

public class Move_Bala : MonoBehaviour
{
    
    [SerializeField]float velocidade = 15f;
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

    }

    // Update is called once per frame
    void Update()
    {
        
        

        


    }
}
