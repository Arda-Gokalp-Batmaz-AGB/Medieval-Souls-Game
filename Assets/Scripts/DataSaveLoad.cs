using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataSaveLoad
{
    public int totalScore;
    public int HighestScore;
    public int currentLevel;

    public int resulotionWidth;
    
    public int resulotionHeight;

    public string mute;
    public DataSaveLoad()
    {
        InitiliazeKeys();
        ReadFile();
    }

    private void ReadFile()
    {

        this.totalScore = PlayerPrefs.GetInt("total_score");
        this.HighestScore = PlayerPrefs.GetInt("highest_score");
        this.currentLevel = PlayerPrefs.GetInt("current_level");

        this.resulotionWidth = PlayerPrefs.GetInt("resulotionWidth");
        this.resulotionHeight = PlayerPrefs.GetInt("resulotionHeight");
        this.mute =  PlayerPrefs.GetString("mute");
            // try
            // {
            //     StreamReader sr = new StreamReader(Application.dataPath+"/database.txt");//Application.dataPath
            //     //StreamReader sr = new StreamReader("Assets/Scripts/Saves/database.txt");
            //    // Debug.LogWarning(Application.dataPath);

            //     // this.totalScore = long.Parse(sr.ReadLine());
            //     // this.HighestScore = long.Parse(sr.ReadLine());
            //     // this.currentLevel = int.Parse(sr.ReadLine());


            //     sr.Close();
            // }
            // catch(System.Exception e)
            // {
            //     Debug.LogWarning("Exception: " + e.Message);
            // }
            // finally
            // {
            //     Debug.LogWarning("Executing finally block.");
            // }
    }
    

    public void WriteFile()
    {
        PlayerPrefs.SetInt("total_score",this.totalScore);
        PlayerPrefs.SetInt("highest_score",this.HighestScore);
        PlayerPrefs.SetInt("current_level",this.currentLevel);
        PlayerPrefs.SetString("mute",this.mute);
        // try
        // {
        //     StreamWriter sw = new StreamWriter(Application.dataPath+"/database.txt");//"/Scripts/Saves/database.txt"

        //     // sw.WriteLine(this.totalScore);
        //     // sw.WriteLine(this.HighestScore);
        //     // sw.WriteLine(this.currentLevel);


        //     sw.Close();
        // }
        // catch(System.Exception e)
        // {
        //     Debug.LogWarning("Exception: " + e.Message);
        // }
        // finally
        // {
        //     Debug.LogWarning("Executing finally block.");
        // }
    }
    private void InitiliazeKeys()
    {
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

















// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// public class DataSaveLoad
// {
//     public long totalScore;
//     public long HighestScore;
//     public int currentLevel;

//     public DataSaveLoad()
//     {
//         ReadFile();
//     }

//     private void ReadFile()
//     {
//             try
//             {
//                 StreamReader sr = new StreamReader(Application.dataPath+"/database.txt");
//                 //StreamReader sr = new StreamReader("Assets/Scripts/Saves/database.txt");
//                 Debug.LogWarning(Application.dataPath);
//                 this.totalScore = long.Parse(sr.ReadLine());
//                 this.HighestScore = long.Parse(sr.ReadLine());
//                 this.currentLevel = int.Parse(sr.ReadLine());
//                 sr.Close();
//             }
//             catch(System.Exception e)
//             {
//                 Debug.LogWarning("Exception: " + e.Message);
//             }
//             finally
//             {
//                 Debug.LogWarning("Executing finally block.");
//             }
//     }
    

//     public void WriteFile()
//     {
//         try
//         {
//             StreamWriter sw = new StreamWriter(Application.dataPath+"/database.txt");//"/Scripts/Saves/database.txt"
//             sw.WriteLine(this.totalScore);
//             sw.WriteLine(this.HighestScore);
//             sw.WriteLine(this.currentLevel);
//             sw.Close();
//         }
//         catch(System.Exception e)
//         {
//             Debug.LogWarning("Exception: " + e.Message);
//         }
//         finally
//         {
//             Debug.LogWarning("Executing finally block.");
//         }
//     }
// }

