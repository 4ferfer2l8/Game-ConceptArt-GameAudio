using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionDebug : MonoBehaviour
{
    [Tooltip("Nome exato da cena (ou use �ndice). Certifique-se que a cena est� em Build Settings.")]
    public string nomeDaCena;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[SceneTransition] Trigger: entrou {other.name} (tag={other.tag})");

        // aceitando 'Player' ou 'player' para evitar problema de case
        if (other.CompareTag("Player") || other.CompareTag("player"))
        {
            Debug.Log("[SceneTransition] Player detectado. Carregando cena: " + nomeDaCena);
            if (string.IsNullOrEmpty(nomeDaCena))
            {
                Debug.LogError("[SceneTransition] nomeDaCena est� vazio!");
                return;
            }
            SceneManager.LoadScene(nomeDaCena);
        }
    }
}
