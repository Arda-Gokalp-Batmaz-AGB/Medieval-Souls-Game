using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Controls the background music and sounds of the game. 
     There are 4 types of sounds that are used in different types of game events. 
     These are triggered depending on the current situation of the game.
*/
public class MusicControl : MonoBehaviour
{
    public static MusicControl musicInstance; // Instance of itself
    // Every type of sound and music is an object every sound object is managed by 
    // the musiccontrol class instance
    [SerializeField] private GameObject backGroundMusic;
    [SerializeField] private GameObject warMusic;
    [SerializeField] private GameObject victoryMusic;
    [SerializeField] private GameObject gameOverMusic;
    private AudioSource currentAudio;

    /*
        Works on sound is created
    */
    void Start()
    {
        musicInstance = this;
    }
    /*
        Function starts to play the relevant sound effect.
        Before starting the new sound, 
        it silences the current playing sound and calls a new sound according to the current event music.
    */
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

    /*
        Stops the current sound of the game
    */
     public void StopSound()
     {
         currentAudio.Stop();
         PlayerPrefs.SetString("mute",Gamemanager.GameManagerInstance.dataBase.mute);
     }
    /*
        Controls the mute preference of the user.
        When user switches mute button, it opens or closes all sound effects.
    */
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