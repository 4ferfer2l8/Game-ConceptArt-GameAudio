using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField]GameObject boss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Outro");
        }
    }
}
