using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bool muted = (PlayerPrefs.GetInt("Music", 1) == 0);
        GetComponent<AudioSource>().mute = muted;
        DontDestroyOnLoad(this);
    }

}
