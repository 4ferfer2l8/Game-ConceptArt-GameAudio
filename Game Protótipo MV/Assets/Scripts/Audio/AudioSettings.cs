using UnityEngine;
using FMODUnity;
using System.Collections;

public class AudioSettings : MonoBehaviour
{
    FMOD.Studio.Bus masterBus;
    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus sfxBus;
    FMOD.Studio.Bus voiceBus;
    FMOD.Studio.Bus ambientBus;
    FMOD.Studio.EventInstance SFXTest;
    FMOD.Studio.EventInstance MusicTest; 
    FMOD.Studio.EventInstance VOsTest; 
    FMOD.Studio.EventInstance AmbienceTest; 
    [SerializeField] GameObject PauseMenu;

    float masterVolume = 0.5f;
    float musicVolume = 0.5f;
    float sfxVolume = 0.5f;
    float voiceVolume = 0.5f;
    float ambientVolume = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFXs");
        voiceBus = FMODUnity.RuntimeManager.GetBus("bus:/VOs");
        ambientBus = FMODUnity.RuntimeManager.GetBus("bus:/Ambience");  
        SFXTest = FMODUnity.RuntimeManager.CreateInstance("event:/Hunter/Hunter Shoot Normal");
        MusicTest = FMODUnity.RuntimeManager.CreateInstance("event:/OST/Level Beta 2");
        VOsTest = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Player_Hit");
        AmbienceTest = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience/Ambience");
    }
    void OnEnable()
    {
      FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("GameState", "Paused");
    }
    void OnDisable()
    {
      FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("GameState", "Resumed");
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
        FMOD.Studio.PLAYBACK_STATE playbackState;
        SFXTest.getPlaybackState(out playbackState);
        if (playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXTest.start();
        }
    }
    public void SetVoiceVolume(float volume)
    {
        voiceVolume = volume;
        FMOD.Studio.PLAYBACK_STATE playbackState;
        VOsTest.getPlaybackState(out playbackState);
        if (playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            VOsTest.start();
        }
    }
    public void SetAmbientVolume(float volume)
    {
        ambientVolume = volume;
        FMOD.Studio.PLAYBACK_STATE playbackState;
        AmbienceTest.getPlaybackState(out playbackState);
        if (playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            AmbienceTest.start();
            IEnumerator StopAmbienceAfterDelay()
            {
                yield return new WaitForSeconds(1f);
                AmbienceTest.setPaused(true);
            }
            StartCoroutine(StopAmbienceAfterDelay());
        }
    }
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
    }
}