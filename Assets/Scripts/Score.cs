using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is used in order to calculate 
     and show the last score of the user
*/
public class Score
{
    public int currentScore; // Shows current score of the user
    public int currentScorelast; // Shows the user's score on game finish
    public Score()
    {

    }

    /*
        Calulates to current score of the user acording to the relevant formula
        by using current army (ball) count of the user
    */
    public void SetScore()
    {
        int count = Gamemanager.GameManagerInstance.Army.Count;
        int level = Gamemanager.GameManagerInstance.dataBase.currentLevel;
        currentScore = (int)(1+ (level/10.0)) * (count+1);
    }
    public void ShowScore()
    {
        int count = Gamemanager.GameManagerInstance.Army.Count;
        int level = Gamemanager.GameManagerInstance.dataBase.currentLevel;
        currentScorelast = (int)(1+ (level/10.0)) * (count+1);
    }
}
