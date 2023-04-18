using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using TMPro;

/*
    Manages the war process which is triggering at the end of the game road. 
    War happens between the player's army and an enemy skeleton army. 
    The army which has a higher soldier count wins.
*/
public class War : MonoBehaviour
{
    [SerializeField] public GameObject AllocatedEnemies; // Max enemy count
    [SerializeField] private GameObject EnemyCountTag; // Holds the tag which shows enemy count
    [SerializeField] private GameObject playerTagLoc; // Holds the location which will show player's army count on the war
    [SerializeField] private GameObject gameOverTag; // Location which gameover screen will be put
    [SerializeField] private GameObject levelloc; // Hold the location which will show player's level on the war 
    [SerializeField] private GameObject muteloc; // Hold the location which will show mute button on the war
    public static War warInstance; // Instance of itself
    public List<GameObject> Enemies = new List<GameObject>(); // Holds enemy list

    private readonly int EnemyActiveCount = 11; // Count of enemies which will be active on each process of the war

    public bool startWar; // Indicates if war is started or not
    [SerializeField] private Camera addcam; // Special camera for the war
    private bool isThreadExecuting = false; // Indicates if Army organiser thread is working or not

    private int maxcount; // Max enemy count
    public bool youWillWin; // Before war finishes, calculates that if you will win or lose the game

    private bool scoreShown; // Shows score when you win the game

    /*
        On the game starts, creates an enemy skeleton army at the end of the game road.
    */
    void Start()
    {
        warInstance = this;
        addcam.gameObject.SetActive(false);
        startWar = false;

        // Calculates total enemy count
        int temp = (int) Math.Round(UnityEngine.Random.Range(5,10)*(1+ (0.05*Gamemanager.GameManagerInstance.dataBase.currentLevel)));
        maxcount = Math.Min(33,temp);

        DeActivateAllEnemies(); // Deactivates all enemy allocations
        PutEnemiesToList(); // Creates enemies
        ReOrganiseEnemies(); // Organises the enemy formation

        gameOverTag.SetActive(false);
        scoreShown = false;

    }

    /*
        Calculates who will be winner by comparing army counts
    */
    private void WhoWins()
    {
        if(Enemies.Count <= Gamemanager.GameManagerInstance.Army.Count)
        {
            youWillWin = true;
        }
        else
        {
            youWillWin = false;
        }
    }
    /*
        While war continues, it decreases the soldiers of each army.
    */
    void Update()
    {
        UpdateCountTag();
        
        if(startWar == true)
        {
            StartElimination(); // Eliminates soldiers
        } 
 
    }
    /*
        Creates enemies
    */
    private void PutEnemiesToList()
    {
         for (int i = 0; i < maxcount; i++)
         {
             Enemies.Add(AllocatedEnemies.transform.GetChild(i).gameObject);
         }
    }
    /*
        Deactivates all enemies on start
    */
    public void DeActivateAllEnemies()
    {
    for (int i = 0; i < AllocatedEnemies.transform.childCount; i++)
        {
            AllocatedEnemies.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /*
        When eliminations happen in an army, it reorganises the formation of the relevant army.
    */
    private void ReOrganiseEnemies()
    {
        int counter = Enemies.Count;
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
        Debug.LogWarning("Enemy support forces come");
        
    }
    /*
        Changes location of level indicator
    */
    private void ReLocateLevel()
    {
        Vector3 pos2 = levelloc.transform.position;
        Gamemanager.GameManagerInstance.levelObj.transform.position = pos2;
    }
    /*
        Starts the war between the player's army and the enemy's army. 
        Overall war loops such as army eliminations and animations are called in this method.
    */
    public void StartWar()
    {
        // Starts the war if it is not already started
        if(startWar == false)
        {
        MusicControl.musicInstance.CallSound("warmusic"); // War music starts
        // Level indicator location changes
        ReLocateLevel(); 
        Gamemanager.GameManagerInstance.levelObj.transform.parent = levelloc.transform ;

        WhoWins(); // Calculates who will be winner
        InteractArmies(); // Forms the armies
        ReLocatePlayerTag(); // Changes location of player army count indicator
        startWar = true;
        Gamemanager.GameManagerInstance.organiserTime = 0;
        WaitAnimation(); // Idle animation
        }     
    }

    /*
        Interacts armies with each other by performing their war animations
    */
    public void InteractArmies()
    {
        addcam.gameObject.SetActive(true);
        for (int i = 0; i < EnemyActiveCount; i++)
        {
            GameObject currentChild = Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(i).gameObject;
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
                Animator allieAnimation = Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(i).GetComponent<Animator>();
                allieAnimation.runtimeAnimatorController = enemyAnimation as RuntimeAnimatorController;
            }
        }
        int dist = 2;
        int j = 0;
        for (int i = EnemyActiveCount; i < Gamemanager.GameManagerInstance.AllocatedArmy.transform.childCount; i++)
        {
            if(j == 11)
                j=0;
            if(i%11==0)
            {
                dist += 2;
            }
            GameObject currentChild = Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(i).gameObject;

            Vector3 newPos =  AllocatedEnemies.transform.GetChild(j).gameObject.transform.position;
            newPos = new Vector3(newPos.x,newPos.y,newPos.z-dist);

            currentChild.transform.position = newPos;
            j++;
        }
    }

    /*
        Changes location of player count
    */
    private void ReLocatePlayerTag()
    {
        Vector3 pos = playerTagLoc.transform.position;
        Gamemanager.GameManagerInstance.PlayerCountTag.transform.position = pos;
        Vector3 scale = new Vector3(6.5f,7.1f,5f);
        Gamemanager.GameManagerInstance.PlayerCountTag.transform.localScale = scale;

    }

    /*
        Updates enemy count
    */
    private void UpdateCountTag()
    {
        TextMeshPro textmeshPro = EnemyCountTag.GetComponent<TextMeshPro>();
        if(Enemies.Count != 0)
        {
            textmeshPro.SetText(Convert.ToString(Enemies.Count));
        }
        
    }
    /*
        Army elimination is working on a thread coroutine and this method calls relevant coroutine.
    */
    private void StartElimination()
    {
        if(isThreadExecuting == false)
        {
        StartCoroutine(this.Organiser());
        }
    }

    /*
        After every elimination of the armies, this threading method reorganizes the formation of the armies.
        It also controls if the soldier counts reached 0 on one of the armies, if so triggers game-over conditions.
    */
    public IEnumerator Organiser()
    {
        isThreadExecuting=true;
        print(Time.time);
        yield return new WaitForSeconds(2);

        print("method runs");
        WaitAnimation();
        int loopCount = EnemyActiveCount;
        for (int i = 0; i < loopCount; i++)
        {
            if(i > Gamemanager.GameManagerInstance.Army.Count || i > Enemies.Count)
            {
                break;
            }
            GameObject currentAllie = Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(i).gameObject;
            GameObject currentEnemy = AllocatedEnemies.transform.GetChild(i).gameObject;

            currentAllie.SetActive(false);
            currentEnemy.SetActive(false);
            Gamemanager.GameManagerInstance.Army.Remove(currentAllie);
            Enemies.Remove(currentEnemy);
        }
        // Triggers when one of the army count is reached to 0
        if(Enemies.Count<=0 || Gamemanager.GameManagerInstance.Army.Count <= 0)
        {
           UpdateCountTag(); 
           StopAttackAnimations();
           isThreadExecuting = true;
           startWar = false;
           Debug.LogWarning("YOU Have Won : " + youWillWin);
           Gamemanager.GameManagerInstance.gameWon = true;
           Gamemanager.GameManagerInstance.StartTheGame = false;
           GameObject maincam = Gamemanager.GameManagerInstance.mainCam.transform.gameObject;
           Vector3 pos = addcam.transform.position;
           maincam.transform.position = pos;

           Gamemanager.GameManagerInstance.PlayerCountTag.SetActive(false);
           EnemyCountTag.SetActive(false);
          TextMeshPro textmeshPro = gameOverTag.GetComponent<TextMeshPro>();
          // Triggers when enemy count reaches 0 which also indicates player won the war
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
           }
           else // If not it indicates that player's army is lose
           {
               textmeshPro.SetText("<uppercase><color=#FF0000>GAME OVER <uppercase></color>");
               Debug.LogWarning("SOUND STOPED");
           }
           gameOverTag.SetActive(true);
           ReLocateLevel();
        }

        // Continues to organize armies while war is proceeding.
        UpdateCountTag(); 
        ReOrganiseEnemies();
        Gamemanager.GameManagerInstance.ReOrganiseArmy();
        isThreadExecuting = false;
        WaitAnimation();

    }

    /*
        Stops attack operations of the armies when game is finished
    */
    private void StopAttackAnimations()
    {
        var anim = Resources.Load("Infantry 4");//2
        for (int i = 0; i < AllocatedEnemies.transform.childCount; i++)
        {
            Animator EnemyAnimation = AllocatedEnemies.transform.GetChild(i).GetComponent<Animator>();
            EnemyAnimation.runtimeAnimatorController = anim as RuntimeAnimatorController;
        }

        for (int i = 0; i < Gamemanager.GameManagerInstance.Army.Count; i++)
        {
            Animator allieAnimation = Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(i).GetComponent<Animator>();
            allieAnimation.runtimeAnimatorController = anim as RuntimeAnimatorController;
        }
    }

    /*
        Performs idle animation of soldiers
    */
    private void WaitAnimation()
    {
        var anim = Resources.Load("Infantry_4");//3
        for (int i = EnemyActiveCount; i < AllocatedEnemies.transform.childCount; i++)
        {
            Animator EnemyAnimation = AllocatedEnemies.transform.GetChild(i).GetComponent<Animator>();
            EnemyAnimation.runtimeAnimatorController = anim as RuntimeAnimatorController;
        }

        for (int i = EnemyActiveCount; i < Gamemanager.GameManagerInstance.Army.Count; i++)
        {
            Animator allieAnimation = Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(i).GetComponent<Animator>();
            allieAnimation.runtimeAnimatorController = anim as RuntimeAnimatorController;
        }        
    }

}