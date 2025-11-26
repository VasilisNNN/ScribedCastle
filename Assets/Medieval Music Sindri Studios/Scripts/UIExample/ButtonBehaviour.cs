using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public Text m_QueueCount;
    public Text m_SongTime;
    public void playAudioWhenClicked()
    {
        //Call the Music manager instance and play a song loaded in the addressables
        MusicManager.Instance.PlayMusic(gameObject.name); 
    }

    public void addSongToQueue()
    {
        //Call the Music manager instance and add a song to the music queue
        MusicManager.Instance.AddSongToQueue(gameObject.name);

        //Access the Music manager instance and get the songs in queue
        m_QueueCount.text = "Clips in queue: " + MusicManager.Instance.MusicQueueCount;
    }

    public void removeSongFromQueue()
    {
        //Call the Music manager instance and add a song to the music queue
        MusicManager.Instance.RemoveSongFromQueue(gameObject.name);

        //Access the Music manager instance and get the songs in queue
        m_QueueCount.text = "Clips in queue: " + MusicManager.Instance.MusicQueueCount;
    }

    public void playMusic()
    {
        //Access the music manager and plays the music
        MusicManager.Instance.PlayMusic();
    }

    public void pauseMusic()
    {
        //Access the music manager and pauses the music
        MusicManager.Instance.PauseMusic();
    }

    public void changeLoop(bool value)
    {
        Toggle toggle = GetComponent<Toggle>();

        //Access the music manager and change if the music is looping
        MusicManager.Instance.MusicLoop = toggle.isOn;
    }

    public void fadeOutCurrent()
    {
        MusicManager.Instance.FadeOutSong(5);
    }
    public void fadeInCurrent()
    {
        MusicManager.Instance.FadeInSong(5);
    }
}
