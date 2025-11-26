using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : PersistentSingleton<MusicManager>
{
    //Atributes

    private Dictionary<string, AudioClip>   m_SFXDictionary         = null;
    private Dictionary<string, AudioClip>   m_musicDictionary       = null;
    private List<string>                    m_musicQueue            = null;

    private AudioSource                     m_backgroundMusic;
    private AudioSource                     m_sfx;

    private float                           m_musicTime             = 0;
    private float                           m_fadeTimer             = 0;
    private float                           m_timeToFade            = 0;
    private bool                            m_UpdateFadeTime        = false;
    private bool                            m_FadeOut               = false;

    private float                           m_musicVolume           = 0.5f;

    private float                           m_SFXVolume             = 0.5f;
    private bool                            m_musicLoop             = true;
    private bool                            m_SFXLoop               = true;

    //private string songs;
    //Properties
    
    /// <summary>
    /// Returns if the music is still fading
    /// </summary>
    public bool isMusicFading
    {
        get { return m_FadeOut; }
    }
    public bool MusicLoop
    {
        get { return m_musicLoop; }
        set {
            m_musicLoop = value;
            m_backgroundMusic.loop = value;  }
    }
    public bool SFXLoop
    {
        get { return m_SFXLoop; }
        set { m_SFXLoop = value;
            m_sfx.loop = value;
        }
    }
 
    /// <summary>
    /// Change the music volume 
    /// </summary>
    public float MusicVolume
    {
        get { return m_musicVolume; }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            m_musicVolume = value;
        }
    }
    /// <summary>
    /// Change the music volume and save it in player prefs
    /// </summary>
    public float MusicVolumeSave
    {
        get { return m_SFXVolume; }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            m_backgroundMusic.volume = m_musicVolume;
            PlayerPrefs.SetFloat(AppPlayerPrefKeys.SFX_VOLUME, value);
            m_musicVolume = value;
        }
    }

    /// <summary>
    /// Change the SFX volume 
    /// </summary>
    public float SFXVolume
    {
        get { return m_SFXVolume; }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            m_SFXVolume = value;
        }
    }
    /// <summary>
    /// Change the SFX volume and save it in player prefs
    /// </summary>
    public float SFXVolumeSave
    {
        get { return m_SFXVolume; }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            m_sfx.volume = m_SFXVolume;
            PlayerPrefs.SetFloat(AppPlayerPrefKeys.SFX_VOLUME, value);
            m_SFXVolume = value;
        }
    }
    public int MusicQueueCount { get { return m_musicQueue.Count; } }

    //Unity Methods

    public override void Awake()
    {
        //songs = "";
        base.Awake();

        m_backgroundMusic   = CreateAudioSource("Music", true);
        m_sfx               = CreateAudioSource("SFX", false);

        MusicVolume = PlayerPrefs.GetFloat(AppPlayerPrefKeys.MUSIC_VOLUME, 0.5f);
        SFXVolume   = PlayerPrefs.GetFloat(AppPlayerPrefKeys.SFX_VOLUME, 0.5f);

        m_SFXDictionary     = new Dictionary<string, AudioClip>();
        m_musicDictionary  = new Dictionary<string, AudioClip>();
        m_musicQueue            = new List<string>();

        //LOAD MUSIC
        var myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/Medieval Music Sindri Studios/AssetBundle/sindri_studios_music");
        if (myLoadedAssetBundle == null)
            Debug.Log("Failed to load AssetBundle");
        AudioClip[] audioArray = myLoadedAssetBundle.LoadAllAssets<AudioClip>();
        if (audioArray != null)
        {
            for (int i = 0; i < audioArray.Length; i++)
            {
                //songs += audioArray[i].name + "\n";
                m_musicDictionary.Add(audioArray[i].name, audioArray[i]);
            }
            //Debug.Log(songs);
        }

        //LOAD SFX
        myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/Medieval Music Sindri Studios/AssetBundle/sindri_studios_sfx");
        if (myLoadedAssetBundle == null)
            Debug.Log("Failed to load AssetBundle");
        audioArray = myLoadedAssetBundle.LoadAllAssets<AudioClip>();
        if (audioArray != null)
        {
            for (int i = 0; i < audioArray.Length; i++)
            {
                m_SFXDictionary.Add(audioArray[i].name, audioArray[i]);
            }
        }

    }
    private void FixedUpdate()
    {
        UpdateMusicTime();
        UpdateMusicVolumeGradually(m_timeToFade);
        CheckIfMusicClipEnded();
    }
    
    //MANAGER CONTROLLER

    /// <summary>
    /// Creates an audio source
    /// </summary>
    /// <param name="of the audio source"="name"></param>
    /// <param name="isLoop"></param>
    /// <returns></returns>
    private AudioSource CreateAudioSource (string name, bool isLoop)
    {
        GameObject temp_audio_host = new GameObject(name);
        AudioSource audioSource = temp_audio_host.AddComponent<AudioSource>() as AudioSource;
        audioSource.playOnAwake = false;
        audioSource.loop = isLoop;
        audioSource.spatialBlend = 0.0f;
        temp_audio_host.transform.SetParent(this.transform);
        return audioSource;
    }

    //  REPRODUCTION CONTROLLER
    /// <summary>
    /// Access the music dictionary and play an specific audio
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayMusic (string audioName)
    {
        if (m_musicDictionary.ContainsKey(audioName))
        {
            m_backgroundMusic.clip = m_musicDictionary[audioName];
            m_backgroundMusic.volume = m_musicVolume;
            m_backgroundMusic.Play();
            m_musicTime = 0;
        }
        else
            Debug.LogError("No audio called " + audioName + " was loaded into the gameplay.");
    }
    /// <summary>
    /// Unpauses the song in being played
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayMusic()
    {
        if (m_backgroundMusic != null)
        {
            if (m_backgroundMusic.isPlaying)
                m_musicTime = 0;
            m_backgroundMusic.Play();
        }
    }

    /// <summary>
    /// Access the SFX dictionary and play an specific audio
    /// </summary>
    /// <param name="audioName"></param>
    public void PlaySound(string audioName)
    {
        if (m_SFXDictionary.ContainsKey(audioName))
        {
            m_sfx.clip = m_musicDictionary[audioName];
            m_sfx.volume = m_musicVolume;
            m_sfx.Play();
        }
        else
            Debug.LogError("No audio called " + audioName + " with the label Sindri_Studios_Music_Pack in adressables.");
    }
    /// <summary>
    /// Stops the music immediatly
    /// </summary>
    public void StopMusic()
    {
        if(m_backgroundMusic != null)
            m_backgroundMusic.Stop();
    }
    /// <summary>
    /// Pauses music immediatly
    /// </summary>
    public void PauseMusic()
    {
        if (m_backgroundMusic != null)
            m_backgroundMusic.Pause();
    }

    /// <summary>
    /// Stops the music with a fade out
    /// </summary>
    /// <param name="time"></param>
    public void FadeOutSong(float time)
    {
        m_UpdateFadeTime    = true;
        m_FadeOut           = true;
        m_timeToFade        = time - 0.01f; ;
        m_fadeTimer         = time;
    }

    public void FadeInSong(float time)
    {
        m_UpdateFadeTime    = true;
        m_FadeOut           = false;
        m_timeToFade        = time;
        m_fadeTimer         = 0.1f;
    }

    /// <summary>
    /// Updates the volume of the music with a fade in or out 
    /// </summary>
    /// <param name="time"></param>
    private  void UpdateMusicVolumeGradually(float time)
    {
        if (m_UpdateFadeTime)
        {
            //Debug.Log(Mathf.InverseLerp(0f, time, m_fadeTimer) + " -- " + m_fadeTimer + " -- " + time);
            m_backgroundMusic.volume = Mathf.InverseLerp(0f, time, m_fadeTimer);

            if (m_FadeOut)
            {
                if (m_fadeTimer - Time.deltaTime < 0)
                    m_fadeTimer = 0;
                else
                    m_fadeTimer -= Time.deltaTime;
            }
            else
                if (m_fadeTimer + Time.deltaTime > m_timeToFade)
                    m_fadeTimer = m_timeToFade;
                else    
                    m_fadeTimer += Time.deltaTime;
            
            m_fadeTimer = Mathf.Clamp(m_fadeTimer, -1, time + 1);
            
            if (m_fadeTimer == 0)
            {
                m_fadeTimer = 0;
                m_UpdateFadeTime = false;
            }
            if (m_fadeTimer > time) { 
                m_fadeTimer = time;
                m_UpdateFadeTime = false;
            }
        }
    }

    //QUEUE CONTROLLER
    /// <summary>
    /// Adds a song to the music queue
    /// </summary>
    public void AddSongToQueue(string audioName)
    {
        if (m_musicDictionary.ContainsKey(audioName))
        {
            m_musicQueue.Add(audioName);
        }
        else
            Debug.LogError("No audio called " + audioName + " with the label Sindri_Studios_Music_Pack in adressables.");
    }
    /// <summary>
    /// Remove a song from the music queue
    /// </summary>
    /// <param name="audioName"></param>
    public void RemoveSongFromQueue(string audioName)
    {
        if (m_musicDictionary.ContainsKey(audioName))
        {
            m_musicQueue.Remove(audioName);
        }
        else
            Debug.LogError("No audio called " + audioName + " with the label Sindri_Studios_Music_Pack in adressables.");
    }
    /// <summary>
    /// Updates the music time it has being playing
    /// </summary>
    private void UpdateMusicTime()
    {
        if (m_backgroundMusic.isPlaying)
        {
            m_musicTime += Time.deltaTime;
            m_musicTime = Mathf.Clamp(m_musicTime, 0, m_backgroundMusic.clip.length + 1);
            if (m_musicTime == m_backgroundMusic.clip.length)
            {
                m_musicTime = 0;
            }
        }
    }

    /// <summary>
    /// Checks if the current clip has ended.
    /// </summary>
    private void CheckIfMusicClipEnded()
    {
        //Debug.Log("Is playing: " + m_backgroundMusic.isPlaying + "|  Time left: " + GetMusicTimeLeft + "|  Queue count: " + m_musicQueue.Count);
        if(!m_backgroundMusic.isPlaying && GetMusicTimeLeft <= 0 && m_musicQueue.Count > 0)
        {
            PlayNextSong();
        }
    }

    /// <summary>
    /// Plays the next song in the queue
    /// </summary>
    public void PlayNextSong()
    {
        PlayMusic(m_musicQueue[0]);
        m_musicQueue.RemoveAt(0);
    }
    public float GetMusicTimeLeft
    {
        get {
            if (m_backgroundMusic.clip != null)
            {
                float timeLeft = m_backgroundMusic.clip.length - m_musicTime;
                if(timeLeft <= 0.15f)
                {
                    timeLeft = 0;
                }
                return timeLeft;
            }
            else
                return 0;
        }
    }
}

[SerializeField]
public struct songLoad
{
    bool loadInGame;
    string tag;
}