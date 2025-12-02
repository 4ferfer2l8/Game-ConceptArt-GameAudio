using UnityEngine;

public class ContaZumbis : MonoBehaviour
{
    public int contadorZumbis = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contadorZumbis = 0;
    }

    // Update is called once per frame
    void Update()
    {
        contadorZumbis = GameObject.FindGameObjectsWithTag("Zumbi").Length;
    }
}
