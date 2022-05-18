using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using TMPro;

public class War : MonoBehaviour
{
    [SerializeField] public GameObject AllocatedEnemies;
    [SerializeField] private GameObject EnemyCountTag;
    [SerializeField] private GameObject playerTagLoc;
    [SerializeField] private GameObject gameOverTag;
    [SerializeField] private GameObject levelloc;
    [SerializeField] private GameObject muteloc;
    public static War warInstance;
    public List<GameObject> Enemies = new List<GameObject>();

    private readonly int EnemyActiveCount = 11;

    public bool startWar;
    [SerializeField] private Camera addcam;
    private bool isThreadExecuting = false;

    private int maxcount;
    public bool youWillWin;

    private bool scoreShown;
    void Start()
    {
        warInstance = this;
        addcam.gameObject.SetActive(false);
        startWar = false;
        //maxcount = Math.Min(33,UnityEngine.Random.Range(7,30));//20,30 / 4,15
        int temp = (int) Math.Round(UnityEngine.Random.Range(5,10)*(1+ (0.05*Gamemanager.GameManagerInstance.dataBase.currentLevel)));
        maxcount = Math.Min(33,temp);//20,30 / 4,15
        DeActivateAllEnemies();
        PutEnemiesToList();
        ReOrganiseEnemies(); 
        //FormEnemies();
        gameOverTag.SetActive(false);
        scoreShown = false;
        //WaitAnimation();
    }

    private void WhoWins()
    {
        if(Enemies.Count <= Gamemanager.GameManagerInstance.Balls.Count)
        {
            youWillWin = true;
        }
        else
        {
            youWillWin = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //ReOrganiseEnemies();  
        UpdateCountTag();
        
        if(startWar == true)
        {
            StartElimination();
        } 
 
    }

    private void PutEnemiesToList()
    {
         for (int i = 0; i < maxcount; i++)
         {
             Enemies.Add(AllocatedEnemies.transform.GetChild(i).gameObject);
         }
    }
    private void AddEnemies()
    {
       // maxcount = maxcount - EnemyActiveCount;
         for (int i = 0; i < maxcount; i++)
         {
             Enemies.Add(AllocatedEnemies.transform.GetChild(i).gameObject);
         }
    }
    public void DeActivateAllEnemies()
    {
    for (int i = 0; i < AllocatedEnemies.transform.childCount; i++)
        {
            AllocatedEnemies.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void ReOrganiseEnemies()
    {
        int counter = Enemies.Count;//Enemies.Count;
        DeActivateAllEnemies();
        Enemies =  new List<GameObject>();
        for (int i = 0; i < AllocatedEnemies.transform.childCount; i++)
            {
                 if(counter <= 0)
                 {
                      break;
                 }
                GameObject currentChild = AllocatedEnemies.transform.GetChild(i).gameObject;
                if(currentChild.activeInHierarchy == false)
                {
                    currentChild.SetActive(true);
                    Enemies.Add(currentChild);
                    counter--;
                }
            }
        Debug.LogWarning("DÜŞMAN DESTEK KUVVETLERİ GELDİ");
        
    }
    private void FormEnemies()
    {
        for (int i = 0; i < AllocatedEnemies.transform.childCount; i++)
        {
            GameObject currentChild = AllocatedEnemies.transform.GetChild(i).gameObject;
            if(currentChild.activeInHierarchy == false)//currentChild.GetComponent<Renderer>().enabled==false
            {
                currentChild.SetActive(true);
                Gamemanager.GameManagerInstance.Balls.Add(currentChild);
            }
        }
    }
    private void ReLocateLevel()
    {
        Vector3 pos2 = levelloc.transform.position;
        Gamemanager.GameManagerInstance.levelObj.transform.position = pos2;
    }
    public void StartWar()
    {
        if(startWar == false)
        {
        MusicControl.musicInstance.CallSound("warmusic");
        ReLocateLevel();
        Gamemanager.GameManagerInstance.levelObj.transform.parent = levelloc.transform ;

       // Gamemanager.GameManagerInstance.musicObj.transform.position = new Vector3(0,0,0);
       // Gamemanager.GameManagerInstance.musicObj.transform.parent = muteloc.transform;
        WhoWins();
        FormAllies();
        ReLocatePlayerTag();
        startWar = true;
        Gamemanager.GameManagerInstance.organiserTime = 0;
        WaitAnimation();
        }     
    }
    public void FormAllies()
    {
        //Gamemanager.GameManagerInstance.mainCam.setactive;
        addcam.gameObject.SetActive(true);
        for (int i = 0; i < EnemyActiveCount; i++)
        {
            GameObject currentChild = Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(i).gameObject;
            if(currentChild.activeInHierarchy == false)
            {
                break;
            }
            else
            {
                Vector3 newPos =  AllocatedEnemies.transform.GetChild(i).gameObject.transform.position;
                newPos = new Vector3(newPos.x,newPos.y,newPos.z-2);
                currentChild.transform.position = newPos;
                var enemyAnimation = AllocatedEnemies.transform.GetChild(i).GetComponent<Animator>().runtimeAnimatorController;
                Animator allieAnimation = Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(i).GetComponent<Animator>();
                allieAnimation.runtimeAnimatorController = enemyAnimation as RuntimeAnimatorController;
            }
        }
        int dist = 2;
        int j = 0;
        for (int i = EnemyActiveCount; i < Gamemanager.GameManagerInstance.AllocatedBalls.transform.childCount; i++)
        {
            if(j == 11)
                j=0;
            if(i%11==0)
            {
                dist += 2;
            }
            GameObject currentChild = Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(i).gameObject;

            Vector3 newPos =  AllocatedEnemies.transform.GetChild(j).gameObject.transform.position;
            newPos = new Vector3(newPos.x,newPos.y,newPos.z-dist);
            //Vector3 newPos = new Vector3(20,0,20);

            currentChild.transform.position = newPos;
            j++;
        }
    }

    private void ReLocatePlayerTag()
    {
        Vector3 pos = playerTagLoc.transform.position;
        Gamemanager.GameManagerInstance.PlayerCountTag.transform.position = pos;
        Vector3 scale = new Vector3(6.5f,7.1f,5f);
        Gamemanager.GameManagerInstance.PlayerCountTag.transform.localScale = scale;
       // ReLocateLevel();

    }


    private void UpdateCountTag()
    {
        TextMeshPro textmeshPro = EnemyCountTag.GetComponent<TextMeshPro>();
        if(Enemies.Count != 0)
        {
            textmeshPro.SetText(Convert.ToString(Enemies.Count));
        }
        
    }

    private void StartElimination()
    {
        if(isThreadExecuting == false)
        {
        StartCoroutine(this.Organiser());
        }
    }
    public IEnumerator Organiser()
    {
        isThreadExecuting=true;
        print(Time.time);
        yield return new WaitForSeconds(2);

        print("method runs");
        WaitAnimation();//////////////////
        int loopCount = EnemyActiveCount;
        for (int i = 0; i < loopCount; i++)
        {
            if(i > Gamemanager.GameManagerInstance.Balls.Count || i > Enemies.Count)
            {
                break;
            }
            GameObject currentAllie = Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(i).gameObject;
            GameObject currentEnemy = AllocatedEnemies.transform.GetChild(i).gameObject;

            currentAllie.SetActive(false);
            currentEnemy.SetActive(false);
            Gamemanager.GameManagerInstance.Balls.Remove(currentAllie);
            Enemies.Remove(currentEnemy);
        }
        if(Enemies.Count<=0 || Gamemanager.GameManagerInstance.Balls.Count <= 0)
        {
           UpdateCountTag(); 
           StopAttackAnimations();
           isThreadExecuting = true;
           startWar = false;
           Debug.LogWarning("YOU Have Won : " + youWillWin);
           Gamemanager.GameManagerInstance.gameWon = true;
           Gamemanager.GameManagerInstance.StartTheGame = false;
           GameObject maincam = Gamemanager.GameManagerInstance.mainCam.transform.gameObject;
           //Vector3 pos = EnemyCountTag.transform.position;
           //pos = new Vector3(pos.x,pos.y+10,pos.z-5);
           Vector3 pos = addcam.transform.position;
           maincam.transform.position = pos;

           Gamemanager.GameManagerInstance.PlayerCountTag.SetActive(false);
           EnemyCountTag.SetActive(false);
          TextMeshPro textmeshPro = gameOverTag.GetComponent<TextMeshPro>();
           if(youWillWin == true)
           {
               if(scoreShown == false)
               {
               Gamemanager.GameManagerInstance.score.ShowScore();
               scoreShown = true;
               }
               MusicControl.musicInstance.CallSound("victorymusic");
               textmeshPro.SetText("<uppercase>VICTORY! \n Score: <uppercase>" + Gamemanager.GameManagerInstance.score.currentScorelast);
               Gamemanager.GameManagerInstance.restartButton.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().fontSize = 1.3f;
               Gamemanager.GameManagerInstance.restartButton.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().SetText("<uppercase>Next Level<uppercase>");
              // TextMeshPro rsChange = Gamemanager.GameManagerInstance.restartButton.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
           }
           else
           {
               textmeshPro.SetText("<uppercase><color=#FF0000>GAME OVER <uppercase></color>");
              // MusicControl.musicInstance.CallSound("gameovermusic");
               //MusicControl.musicInstance.StopSound();
               Debug.LogWarning("SOUND STOPED");
           }
           gameOverTag.SetActive(true);
           ReLocateLevel();
          // MusicControl.musicInstance.StopSound();
        }
       // else
      //  {
       // maxcount -= 10;
       // AddEnemies();
        UpdateCountTag(); 
        ReOrganiseEnemies();
        Gamemanager.GameManagerInstance.ReOrganiseBalls();
        //UpdateCountTag();
        isThreadExecuting = false;
      //  }
        WaitAnimation();

    }

    private void StopAttackAnimations()
    {
        var anim = Resources.Load("Infantry 4");//2
        for (int i = 0; i < AllocatedEnemies.transform.childCount; i++)
        {
            Animator EnemyAnimation = AllocatedEnemies.transform.GetChild(i).GetComponent<Animator>();
            EnemyAnimation.runtimeAnimatorController = anim as RuntimeAnimatorController;//Resources.Load("Infantary 2")
        }

        for (int i = 0; i < Gamemanager.GameManagerInstance.Balls.Count; i++)
        {
            Animator allieAnimation = Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(i).GetComponent<Animator>();
            allieAnimation.runtimeAnimatorController = anim as RuntimeAnimatorController;
        }
    }

    private void WaitAnimation()
    {
        var anim = Resources.Load("Infantry_4");//3
        //var animold = Resources.Load("Infantary 1");
        for (int i = EnemyActiveCount; i < AllocatedEnemies.transform.childCount; i++)
        {
            Animator EnemyAnimation = AllocatedEnemies.transform.GetChild(i).GetComponent<Animator>();
           // EnemyAnimation.runtimeAnimatorController = animold as RuntimeAnimatorController;//Resources.Load("Infantary 2")
            EnemyAnimation.runtimeAnimatorController = anim as RuntimeAnimatorController;//Resources.Load("Infantary 2")
        }

        for (int i = EnemyActiveCount; i < Gamemanager.GameManagerInstance.Balls.Count; i++)
        {
            Animator allieAnimation = Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(i).GetComponent<Animator>();
         //   allieAnimation.runtimeAnimatorController = animold as RuntimeAnimatorController;
            allieAnimation.runtimeAnimatorController = anim as RuntimeAnimatorController;
        }        
    }

}






    // private void DecideResult()
    // {
    //     if(Gamemanager.GameManagerInstance.Balls.Count <=0)
    //     {

    //     }
    //     if(Enemies.Count <=0)
    //     {
    //         Gamemanager.GameManagerInstance.gameWon = true;
    //         Gamemanager.GameManagerInstance.StartTheGame = false;
    //         print("YOU WON THE GAME!!!");            
    //     }
    // }