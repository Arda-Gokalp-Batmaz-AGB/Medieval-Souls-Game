using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour
{
    public void SwitchMuteGame()
    {
        MusicControl.musicInstance.SwitchMute();
       // ChangeIcon();
        Debug.LogWarning("MUTE HAS BEEN SWITCHED");
    }

     void Update()
    {
       // Debug.LogWarning(Gamemanager.GameManagerInstance.dataBase.mute);
        if(Gamemanager.GameManagerInstance.dataBase.mute.Equals("false"))
        {
            //transform.Find("unmute").gameObject.m(false);
            transform.Find("unmute").gameObject.GetComponent<SpriteRenderer>().enabled = false;
            transform.Find("mute").gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            transform.Find("mute").gameObject.GetComponent<SpriteRenderer>().enabled = false;
            transform.Find("unmute").gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
