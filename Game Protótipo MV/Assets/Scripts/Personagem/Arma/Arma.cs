using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField]GameObject Bala;
    [SerializeField]GameObject canodaarma;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PosMouse = Input.mousePosition;

        PosMouse = Camera.main.ScreenToWorldPoint(PosMouse);

        Vector2 diferenca = new Vector2(PosMouse.x - transform.position.x, PosMouse.y - transform.position.y);

        // Mantém a rotação calculada e aplica um ajuste de +90 graus no eixo Z
        Quaternion rotBase = Quaternion.LookRotation(Vector3.forward, diferenca);
        float zCorrigido = rotBase.eulerAngles.z + 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, zCorrigido);

        if (Input.GetMouseButtonDown(0))
        {
            Atira();
        }
    }


    void Atira()
    {
        Vector3 PosMouse = Input.mousePosition;

        PosMouse = Camera.main.ScreenToWorldPoint(PosMouse);

        Vector2 diferenca = new Vector2(PosMouse.x - transform.position.x, PosMouse.y - transform.position.y);

        // Mantém a rotação calculada e aplica um ajuste de +90 graus no eixo Z
        Quaternion rotBase = Quaternion.LookRotation(Vector3.forward, diferenca);
        float zCorrigido = rotBase.eulerAngles.z + 90f;
        Instantiate(Bala, canodaarma.transform.position, Quaternion.Euler(0f, 0f, zCorrigido));
    }
}
