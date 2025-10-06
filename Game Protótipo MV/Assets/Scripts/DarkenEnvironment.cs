using UnityEngine;
using UnityEngine.Tilemaps;

public class DarkenEnvironment : MonoBehaviour
{
    [Header("Cor do cenário noturno")]
    public Color nightColor = new Color(0.3f, 0.3f, 0.4f, 1f); // cinza azulado

    void Start()
    {
        // Pega todos os objetos com tag "Cenario"
        GameObject[] environmentObjects = GameObject.FindGameObjectsWithTag("Cenario");

        foreach (GameObject obj in environmentObjects)
        {
            // Se for SpriteRenderer
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = nightColor;
            }

            // Se for Tilemap
            Tilemap tm = obj.GetComponent<Tilemap>();
            if (tm != null)
            {
                tm.color = nightColor;
            }
        }
    }
}
