using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using TMPro;

/*
The main class of all programs. It controls the game loop, wins, losses conditions, 
 and the army of the user. 
 All the classes are depends on the management of this class.
*/
public class Gamemanager : MonoBehaviour
{
    [HideInInspector] public bool MoveByTouch, StartTheGame;
    private Vector3 _mouseStartPos, PlayerStartPos;
    [SerializeField] public float RoadSpeed, SwipeSpeed,Distance;
    [SerializeField] private GameObject Road;
    [SerializeField] public GameObject AllocatedArmy; // Max army size
    [SerializeField] public GameObject PlayerCountTag; //Holds the location which shows army count

    [SerializeField] public GameObject restartButton; // Restart button object

    [SerializeField] public GameObject levelTag;  //Holds the location which shows current level
    [SerializeField] public GameObject levelObj; //Holds the level object
    [SerializeField] public GameObject totalScoreTag; //Holds the location which shows total score
    [SerializeField] public GameObject highestScoreTag;  //Holds the location which shows highest score
    [SerializeField] public GameObject musicObj; // Music open/close button object
    public static Gamemanager GameManagerInstance; // Instance of object itself
    public Camera mainCam; // Maincam of the game
    public List<GameObject> Army = new List<GameObject>(); // Current army of the user
    public GameObject NewSoldier; // New soldier which will be added on the army
    public ParticleSystem Explosion; // Explosion particle effect object

    public bool gameWon; // States if game is won or not
    private bool isThreadExecuting = false; // Checks if army organizing works

    public DataSaveLoad dataBase; // Database of the game
    private bool writeDataBase; // Is database write is allowed

    public Difficulity difficulity; // Difficulty object

    public Score score; // Score object

    public bool stopMovement; // States if movement allowed

    public int organiserTime; // Indicates timer of army organiser

    private bool musicStarted; //Indicates if game music is started

    private bool gameovermusiccalled; // States if gameover music is called

    /* 
        This function works on the game starts
    */
    void Start()
    {
        Application.targetFrameRate = 60; 
        GameManagerInstance = this;
        Debug.LogWarning("game manager is working");
        mainCam = Camera.main; // Sets the camera
        dataBase = new DataSaveLoad(); // Creates database object
        writeDataBase = true;
        SetResolution();
        difficulity = new Difficulity(); // Creates a difficulity object to control difficulity
        score = new Score(); // Score object
        difficulity.SetRoadSpeed(); // Sets road speed according to difficulty
        Debug.LogWarning("CUrrent level : " + dataBase.currentLevel);
        Debug.LogWarning("Set road speed : " + RoadSpeed);
        DeActivateAllocations(); // Deactivates all army on the start

        // Activates first soldier in the army
        Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(0).gameObject.SetActive(true); 
        Army.Add(Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(0).gameObject);

        restartButton.SetActive(false); //Disables restart button on start

        // Initialises relevant variables
        gameWon = false;
        stopMovement = false;
        organiserTime = 1;


        SetTables(); // Activates objects which shows current and highest
        UpdateLevelTag(); // Activates the object which shows user level
        musicStarted = false;
        gameovermusiccalled = false;
    }
    
    /*
        Sets the resolution of the game, will be used in the future
    */
     private void SetResolution() {
        // var height = (int)(dataBase.resulotionHeight * 0.8f);
        // var width = (int)(dataBase.resulotionWidth * 0.8f); 
        // Screen.SetResolution(width, height, true);
        // Debug.LogWarning("Height:"+height + " Width:" + width);
      }
      
    /*
      This method works on every frame 
      in order to update the game and manage the game loop
    */
    void Update()
    {
        GameContinues(); // Checks if game still going on
        UpdateCountTag(); // Updates the indicator that shows army count of user
        HideTables(); // Hides total and highest score

        // Starts the background music on game starts
        if (Input.GetMouseButtonDown(0) && stopMovement == false)
        {
            StartTheGame = MoveByTouch = true;
            if(musicStarted == false)
            {
            MusicControl.musicInstance.CallSound("backgroundmusic");
            musicStarted = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && stopMovement == false) 
        {
            MoveByTouch = false;
        }
        // Allows the user to move the army by swiping the mouse left and right
        // Some physical calculations are used in order to provide this functionality
        if (MoveByTouch)
        {
            var plane = new Plane(Vector3.up, 0f);

            float distance;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out distance))
            {
                Vector3 mousePos = ray.GetPoint(distance);
                Vector3 desirePso = mousePos - _mouseStartPos;
                Vector3 move = PlayerStartPos + desirePso;

                move.x = Mathf.Clamp(move.x, -5f, 4.5f);
                move.z = -7f;

                var player = transform.position;
                var oldpos = transform.position;
                player = new Vector3(Mathf.Lerp(player.x, move.x, Time.deltaTime * (SwipeSpeed + 10f)), player.y, player.z);
                transform.position = player;

            }
        }

        // In the overall game loop, actually player is not progressing but the road is progressing 
        // and this code block allows the road to progress in the direction of the player
        if (StartTheGame) 
            Road.transform.Translate(Vector3.forward * (RoadSpeed * -1 * Time.deltaTime));

    }

    /*
    It is a special frame update method that works after the "update" method is called. 
    It is preferable, especially for rendering the main camera of the game
    */
    private void LateUpdate()
    {
        if (StartTheGame)
            mainCam.transform.position = new Vector3(Mathf.Lerp(mainCam.transform.position.x, transform.position.x, (SwipeSpeed - 5f) * Time.deltaTime),
                    mainCam.transform.position.y, mainCam.transform.position.z);
    }


/* 
    Deactivates all the army on game start
*/
 public void DeActivateAllocations()
 {
   for (int i = 0; i < Gamemanager.GameManagerInstance.AllocatedArmy.transform.childCount; i++)
    {
        Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(i).gameObject.SetActive(false);
    }

 }

/*
    Deactivates the score objects
*/
private void HideTables()
{
    if(StartTheGame == true)
    {
        totalScoreTag.transform.parent.gameObject.SetActive(false);
        highestScoreTag.transform.parent.gameObject.SetActive(false);            
    }
}

/*
    Activates and updates the score values on the total and highest score objects
*/
private void SetTables()
{
    totalScoreTag.transform.parent.gameObject.SetActive(true);
    highestScoreTag.transform.parent.gameObject.SetActive(true);
    highestScoreTag.GetComponent<TextMeshPro>().SetText(Convert.ToString(dataBase.HighestScore));

    totalScoreTag.GetComponent<TextMeshPro>().SetText(Convert.ToString(dataBase.totalScore));
}

/*
    When the army is damaged by obstacles on the road, 
    the game reforms the remaining army in order to preserve the army formation.
    This operation is a timed operation that works on an alternative thread.
*/
public void ReOrganiseArmy()
{
    if(isThreadExecuting == false)
    {
    StartCoroutine(Gamemanager.GameManagerInstance.Organiser());
    
    }
}
    /*
        It is a coroutine thread that reorganizes the formation of the army after some time is passed.
    */
    public IEnumerator Organiser()
    {
        isThreadExecuting=true;
        print(Time.time);
        yield return new WaitForSeconds(organiserTime);

        print("method runs");
        int counter = Army.Count;
        DeActivateAllocations();
        Army =  new List<GameObject>();
        for (int i = 0; i < Gamemanager.GameManagerInstance.AllocatedArmy.transform.childCount; i++)
            {
                if(counter <= 0)
                {
                    break;
                }
                GameObject currentChild = Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(i).gameObject;
                if(currentChild.activeInHierarchy == false)
                {
                    currentChild.SetActive(true);

                    Gamemanager.GameManagerInstance.Army.Add(currentChild);
                    counter-- ;
                }
            }
        isThreadExecuting = false;

    }

/*
    Checks, if the game is finished by the loss which is triggered when the army count drops to 0,
     or if the win condition is triggered after you successfully defeat the enemy army.
*/
public void GameContinues()
{
    if (Army.Count <= 0 || gameWon == true)
    {
        StartTheGame = false;
        restartButton.SetActive(true);
        MoveByTouch = false;
        RoadSpeed = 0;
        stopMovement = true;

    } 
}

/*
    Updates the army count of the player and 
    when win condition occurs sets player count to text "Victory Score: ..."
*/
public void UpdateCountTag()
{
    TextMeshPro textmeshPro = PlayerCountTag.GetComponent<TextMeshPro>();
    if(Army.Count != 0) // WIN Condition
    {
        if(gameWon == true)
        {
            textmeshPro.SetText("VICTORY! \n Score:" + score.currentScore);
            dataBase.currentLevel++;
        }
        else
        {
            textmeshPro.SetText(Convert.ToString(Army.Count));
        }
    }
    else // Lose Condition
    {
    // Calls game over music when you lose the game
    if(gameovermusiccalled == false)
    {
        MusicControl.musicInstance.CallSound("gameovermusic");
        gameovermusiccalled = true;
    }
    textmeshPro.SetText("<color=#FF0000><uppercase>GAME OVER<uppercase></color>");
    Vector3 loc = new Vector3(restartButton.transform.position.x,restartButton.transform.position.y+0.6f,restartButton.transform.position.z);

    PlayerCountTag.transform.SetParent(mainCam.gameObject.transform);
    PlayerCountTag.transform.position = loc;
    PlayerCountTag.transform.localScale = new Vector3(0.62f,0.62f,0.62f);
    stopMovement = true; // Stops movement after game over

    }
    // Updates the database by your new score when you won the game 
    if((gameWon == true || Army.Count <= 0) && writeDataBase == true)
    {
        writeDataBase = false;
        score.SetScore();
        dataBase.totalScore = dataBase.totalScore + score.currentScore;
        if(score.currentScore > dataBase.HighestScore) // Updates highest score
        {
            dataBase.HighestScore = score.currentScore;
        }
        dataBase.WriteFile();
    }
}

/*
    Updates the current level of the user
*/
private void UpdateLevelTag()
{
     TextMeshPro textmeshPro = levelTag.GetComponent<TextMeshPro>();
     textmeshPro.SetText("<uppercase>"+dataBase.currentLevel+"<uppercase>");
}
    /*
        Triggers when user quit from the application. 
        It is also saving mute preference before quitting from the game    
    */
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        PlayerPrefs.SetString("mute",dataBase.mute);
    }
}