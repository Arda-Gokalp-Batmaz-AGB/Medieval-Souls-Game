using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Difficulty class is used by GameManager and Random Level Generator 
    classes to change road speed and road length of the overall 
    game depending on the current level of user
*/
public class Difficulity
{
    public Difficulity()
    {
    }
    
    /*
        This function sets a roadlength which is used while 
        RandomLevelGenerator Class is building the game road.
        At the beginning, the road length is 20 
        however on every 4 levels it is increased by 1.
    */
    public void SetRoadLength()
    {
        int level = Gamemanager.GameManagerInstance.dataBase.currentLevel;
        RandomLevelGenerator.RandomLevelGeneratorInstance.roadLength = 20;
        for (int i = 1; i < level+1; i++)
        {
            if(i != 0 && i % 4 == 0)
            {
                RandomLevelGenerator.RandomLevelGeneratorInstance.roadLength += 1;
            }
        }
    }

    /*
        This function sets roadspeed which increases pace of the game.
         On every 4 levels roadspeed is increased by 0.25f
    */
    public void SetRoadSpeed()
    {
        int level = Gamemanager.GameManagerInstance.dataBase.currentLevel;
        for (int i = 1; i < level+1; i++)
        {
            if(i != 0 && i % 4 == 0)
            {
               Gamemanager.GameManagerInstance.RoadSpeed += 0.25f;
            }
        }
    }
}
