using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/* 
    This class is used for saving and 
     loading the preferences, levels, scores of the player
*/
public class DataSaveLoad
{
    // Scores of user
    public int totalScore;
    public int HighestScore;
    public int currentLevel;

    // Resulotion preferences
    public int resulotionWidth;
    
    public int resulotionHeight;

    public string mute; // Sound mute preference

    /*
        Constructor method works on a DataSaveLoad class created that allows
         data read and write operations. Also reads stored data on creation
    */
    public DataSaveLoad()
    {
        InitiliazeKeys();
        ReadFile();
    }

    /*
        Playerprefs is a built-in class in the Unity that allows storing user
        information on the user's device. Stored data can be acessed by using
        saved the key value which is used for saving relevant data.
    */
    private void ReadFile()
    {

        this.totalScore = PlayerPrefs.GetInt("total_score");
        this.HighestScore = PlayerPrefs.GetInt("highest_score");
        this.currentLevel = PlayerPrefs.GetInt("current_level");

        this.resulotionWidth = PlayerPrefs.GetInt("resulotionWidth");
        this.resulotionHeight = PlayerPrefs.GetInt("resulotionHeight");
        this.mute =  PlayerPrefs.GetString("mute");
    }
    
    /*
        Similiar to read however this time Playerprefs is used in a key-pair
        relation in order to store relevant data of the user
    */
    public void WriteFile()
    {
        PlayerPrefs.SetInt("total_score",this.totalScore);
        PlayerPrefs.SetInt("highest_score",this.HighestScore);
        PlayerPrefs.SetInt("current_level",this.currentLevel);
        PlayerPrefs.SetString("mute",this.mute);
    }
    /*
       If user is playing the game first time this method 
        initializes user's data by assigning default values
    */
    private void InitiliazeKeys()
    {
        // Default values are set if the relevant data is not exists in the user's device
        if(!PlayerPrefs.HasKey("total_score"))
        {
            PlayerPrefs.SetInt("total_score",5);
        }
        if(!PlayerPrefs.HasKey("highest_score"))
        {
            PlayerPrefs.SetInt("highest_score",4);
        }
        if(!PlayerPrefs.HasKey("current_level"))
        {
            PlayerPrefs.SetInt("current_level",1);
        }

        if(!PlayerPrefs.HasKey("resulotionHeight") || !PlayerPrefs.HasKey("resulotionWidth"))
        {
            var resolution = Screen.currentResolution;
            PlayerPrefs.SetInt("resulotionHeight",resolution.height);
            PlayerPrefs.SetInt("resulotionWidth",resolution.width);
        }

        if(!PlayerPrefs.HasKey("mute"))
        {
            PlayerPrefs.SetString("mute","false");
        }

    }
}