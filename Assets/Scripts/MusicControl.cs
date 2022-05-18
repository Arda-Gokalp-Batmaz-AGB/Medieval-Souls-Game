using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
   // [SerializeField] GameObject backGroundMusic;
   // [SerializeField] GameObject warMusic;

    public static MusicControl musicInstance;

    //private AudioSource currrentAudio;
    [SerializeField] private GameObject backGroundMusic;
    [SerializeField] private GameObject warMusic;
    [SerializeField] private GameObject victoryMusic;
    [SerializeField] private GameObject gameOverMusic;
    private AudioSource currentAudio;

    //private bool mute;
    void Start()
    {
        //mute = false;
        musicInstance = this;
    }

    public void CallSound(string type)
    {
        if(currentAudio != null)
        {
            currentAudio.Stop();
        }
        if(type.Equals("backgroundmusic"))
        {
        currentAudio =  backGroundMusic.GetComponent<AudioSource>();
        Debug.LogWarning("MUSIC SOUND STARTED");
        }
        if(type.Equals("warmusic"))
        {
            currentAudio =  warMusic.GetComponent<AudioSource>();
        }
        if(type.Equals("victorymusic"))
        {
            currentAudio =  victoryMusic.GetComponent<AudioSource>();
        }
        if(type.Equals("gameovermusic"))
        {
            currentAudio =  gameOverMusic.GetComponent<AudioSource>();
        }

        if(Gamemanager.GameManagerInstance.dataBase.mute.Equals("false"))
        {
            currentAudio.Play();
        }
    }

     public void StopSound()
     {
         currentAudio.Stop();
         PlayerPrefs.SetString("mute",Gamemanager.GameManagerInstance.dataBase.mute);
     }

    public void SwitchMute()
    {
        if(Gamemanager.GameManagerInstance.dataBase.mute.Equals("true"))
        {
            currentAudio.Play();
            Gamemanager.GameManagerInstance.dataBase.mute = "false";
        }
        else if(Gamemanager.GameManagerInstance.dataBase.mute.Equals("false"))
        {
            currentAudio.Stop();
            Gamemanager.GameManagerInstance.dataBase.mute = "true";
        }
    }
}





    // void Start()
    // {
    //     backGroundMusic = Resources.Load("Sounds/BackGroundMusic") as AudioSource;
    //     warMusic = Resources.Load("Sounds/WarMusic") as AudioSource;
    //     currentAudio = null;
    //     musicInstance = this;
    // }

    // public void CallSound(string type)
    // {
    //     if(currentAudio != null)
    //     {
    //         currentAudio.Stop();
    //     }
    //     if(type.Equals("backgroundmusic"))
    //     {
    //        currentAudio =  backGroundMusic;
    //        Debug.LogWarning("MUSIC SOUND STARTED");
    //     }
    //     if(type.Equals("warmusic"))
    //     {
    //        currentAudio =  warMusic;
    //     }
    //     warMusic.Play(0);
    // }