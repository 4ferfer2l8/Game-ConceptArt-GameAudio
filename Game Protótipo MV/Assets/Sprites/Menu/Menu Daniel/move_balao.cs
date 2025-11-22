using UnityEngine;

public class move_balao : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
      this.transform.position += new Vector3(1f * Time.deltaTime, 0, 0);
    }
}
