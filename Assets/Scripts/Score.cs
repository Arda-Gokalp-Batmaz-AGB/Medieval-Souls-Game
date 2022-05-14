using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    public int currentScore;
    public int currentScorelast;
    public Score()
    {

    }

    public void SetScore()
    {
        int count = Gamemanager.GameManagerInstance.Balls.Count;
        int level = Gamemanager.GameManagerInstance.dataBase.currentLevel;
        currentScore = (int)(1+ (level/10.0)) * (count+1);
        // switch (type)
        // {
        //     case "Gates":
        //     currentScore += 5;
        //     break;


        //     default:
        //     break;
        // }
    }
    public void ShowScore()
    {
        int count = Gamemanager.GameManagerInstance.Balls.Count;
        int level = Gamemanager.GameManagerInstance.dataBase.currentLevel;
        currentScorelast = (int)(1+ (level/10.0)) * (count+1);
    }
}
