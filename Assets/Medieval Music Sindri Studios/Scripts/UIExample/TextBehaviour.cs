using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBehaviour : MonoBehaviour
{
    private Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        //Access the music manager and get how much time the current song has left
        int timeleft = (int)MusicManager.Instance.GetMusicTimeLeft;
        text.text = "Time remaining: -" + timeleft + " s";
    }
}
