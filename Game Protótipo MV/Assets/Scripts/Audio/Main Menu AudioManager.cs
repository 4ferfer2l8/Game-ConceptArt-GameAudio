using UnityEngine;
using FMODUnity;

public class MainMenuAudioManager : MonoBehaviour
{
   [SerializeField] EventReference ClickEvent;
    [SerializeField] EventReference UnClickEvent;
    
    [SerializeField] GameObject menu;
    [SerializeField] Funções_Menu controlador;

    public void Click()
    {
        RuntimeManager.PlayOneShotAttached(ClickEvent, menu);
    }
    public void UnClick()
    {
        RuntimeManager.PlayOneShotAttached(UnClickEvent, menu);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controlador.isClicking)
        {
            Click();
        }
        if (controlador.Unclicked)
        {
            UnClick();
        }
    }
}
