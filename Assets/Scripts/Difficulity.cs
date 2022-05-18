using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulity
{
   // public float roadSpeed;
   // public float roadLength;
   // public int obstacleRatio;
    public Difficulity()
    {
        //SetDifficulties();
    }
    
    public void SetRoadLength()
    {
        int level = Gamemanager.GameManagerInstance.dataBase.currentLevel;
        RandomLevelGenerator.RandomLevelGeneratorInstance.roadLength = 20;
        for (int i = 1; i < level+1; i++)
        {
            if(i != 0 && i % 4 == 0)
            {
                RandomLevelGenerator.RandomLevelGeneratorInstance.roadLength += 1;//RANDOMLEVELDE ÇAGIR
            }
        }
        //Gamemanager.GameManagerInstance.data
    }

    public void SetRoadSpeed()
    {
        int level = Gamemanager.GameManagerInstance.dataBase.currentLevel;
        for (int i = 1; i < level+1; i++)
        {
            if(i != 0 && i % 4 == 0)
            {
               Gamemanager.GameManagerInstance.RoadSpeed += 0.25f;//RANDOMLEVELDE ÇAGIR
            }
        }
    }
}
