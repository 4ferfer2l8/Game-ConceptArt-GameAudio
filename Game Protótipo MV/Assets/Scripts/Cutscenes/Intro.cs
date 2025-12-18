using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] float videoDuration = 5f;
    [SerializeField] float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= videoDuration)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
}
