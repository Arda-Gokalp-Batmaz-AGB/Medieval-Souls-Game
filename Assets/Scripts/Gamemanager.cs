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
    [SerializeField] public GameObject AllocatedArmy;
    [SerializeField] public GameObject PlayerCountTag;

    [SerializeField] public GameObject restartButton;

    [SerializeField] public GameObject levelTag;
    [SerializeField] public GameObject levelObj;
    [SerializeField] public GameObject totalScoreTag;
    [SerializeField] public GameObject highestScoreTag;
    [SerializeField] public GameObject musicObj;
    public static Gamemanager GameManagerInstance;
    public Camera mainCam;
    public List<GameObject> Army = new List<GameObject>();
    public GameObject NewSoldier;
    public ParticleSystem Explosion;

    public bool gameWon;
    private bool isThreadExecuting = false;

    public DataSaveLoad dataBase;
    private bool writeDataBase;

    public Difficulity difficulity;

    public Score score;

    public bool stopMovement;

    public int organiserTime;

    private bool musicStarted; 

    private bool gameovermusiccalled;

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
        DeActivateAllocations(); //
        Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(0).gameObject.SetActive(true);
        Army.Add(Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(0).gameObject);
        restartButton.SetActive(false);
        gameWon = false;
        stopMovement = false;
        organiserTime = 1;
        SetTables();
        UpdateLevelTag();
        musicStarted = false;
        gameovermusiccalled = false;
    }
    
     private void SetResolution() {
        // var height = (int)(dataBase.resulotionHeight * 0.8f);
        // var width = (int)(dataBase.resulotionWidth * 0.8f); 
        // Screen.SetResolution(width, height, true);
        // Debug.LogWarning("Height:"+height + " Width:" + width);
      }
      
    void Update()
    {
        GameContinues();
        UpdateCountTag();
        HideTables();

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
       
        if (StartTheGame) //Oyun devam ederken yol hareket ediyor
            Road.transform.Translate(Vector3.forward * (RoadSpeed * -1 * Time.deltaTime));

        
    }

    private void LateUpdate()
    {
        if (StartTheGame)
            mainCam.transform.position = new Vector3(Mathf.Lerp(mainCam.transform.position.x, transform.position.x, (SwipeSpeed - 5f) * Time.deltaTime),
                    mainCam.transform.position.y, mainCam.transform.position.z);
    }


/* 

*/
 public void DeActivateAllocations()
 {
   for (int i = 0; i < Gamemanager.GameManagerInstance.AllocatedArmy.transform.childCount; i++)
    {
        Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(i).gameObject.SetActive(false);
    }

 }

private void HideTables()
{
    if(StartTheGame == true)
    {
        totalScoreTag.transform.parent.gameObject.SetActive(false);
        highestScoreTag.transform.parent.gameObject.SetActive(false);            
    }
}

private void SetTables()
{
    totalScoreTag.transform.parent.gameObject.SetActive(true);
    highestScoreTag.transform.parent.gameObject.SetActive(true);
    highestScoreTag.GetComponent<TextMeshPro>().SetText(Convert.ToString(dataBase.HighestScore));

    totalScoreTag.GetComponent<TextMeshPro>().SetText(Convert.ToString(dataBase.totalScore));
}
public void ReOrganiseBalls()
{
    if(isThreadExecuting == false)
    {
    StartCoroutine(Gamemanager.GameManagerInstance.Organiser());
    
    }
}
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

public void UpdateCountTag()
{
    TextMeshPro textmeshPro = PlayerCountTag.GetComponent<TextMeshPro>();
    if(Army.Count != 0)
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
    else
    {
    if(gameovermusiccalled == false)
    {
        MusicControl.musicInstance.CallSound("gameovermusic");
        gameovermusiccalled = true;
    }
    Debug.LogWarning("ttttt");
    textmeshPro.SetText("<color=#FF0000><uppercase>GAME OVER<uppercase></color>");
    Vector3 loc = new Vector3(restartButton.transform.position.x,restartButton.transform.position.y+0.6f,restartButton.transform.position.z);

    PlayerCountTag.transform.SetParent(mainCam.gameObject.transform);
    PlayerCountTag.transform.position = loc;
    PlayerCountTag.transform.localScale = new Vector3(0.62f,0.62f,0.62f);
    stopMovement = true;

    }
    if((gameWon == true || Army.Count <= 0) && writeDataBase == true)
    {
        writeDataBase = false;
        score.SetScore();
        dataBase.totalScore = dataBase.totalScore + score.currentScore;
        if(score.currentScore > dataBase.HighestScore)
        {
            dataBase.HighestScore = score.currentScore;
        }
        dataBase.WriteFile();
    }
}

private void UpdateLevelTag()
{
     TextMeshPro textmeshPro = levelTag.GetComponent<TextMeshPro>();
     textmeshPro.SetText("<uppercase>"+dataBase.currentLevel+"<uppercase>");
}
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        PlayerPrefs.SetString("mute",dataBase.mute);
    }
}