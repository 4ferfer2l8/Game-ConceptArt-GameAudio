using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    FMOD.Studio.Bus masterBus;
    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus sfxBus;
    FMOD.Studio.Bus voiceBus;
    FMOD.Studio.Bus ambientBus;
    float masterVolume = 1.0f;
    float musicVolume = 1.0f;
    float sfxVolume = 1.0f;
    float voiceVolume = 1.0f;
    float ambientVolume = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFXs");
        voiceBus = FMODUnity.RuntimeManager.GetBus("bus:/VOs");
        ambientBus = FMODUnity.RuntimeManager.GetBus("bus:/Ambience");  
    }

    // Update is called once per frame
    void Update()
    {
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
        voiceBus.setVolume(voiceVolume);
        ambientBus.setVolume(ambientVolume);
        masterBus.setVolume(masterVolume);
    }
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }
    public void SetVoiceVolume(float volume)
    {
        voiceVolume = volume;
    }
    public void SetAmbientVolume(float volume)
    {
        ambientVolume = volume;
    }
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
    }
}