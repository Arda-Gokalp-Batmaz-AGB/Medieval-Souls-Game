using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using TMPro;
public class Gamemanager : MonoBehaviour
{
    [HideInInspector] public bool MoveByTouch, StartTheGame;
    private Vector3 _mouseStartPos, PlayerStartPos;
    [SerializeField] public float RoadSpeed, SwipeSpeed,Distance;
    [SerializeField] private GameObject Road;
    [SerializeField] public GameObject AllocatedBalls;
    [SerializeField] public GameObject PlayerCountTag;

    [SerializeField] public GameObject restartButton;

    [SerializeField] public GameObject levelTag;
    [SerializeField] public GameObject levelObj;
    [SerializeField] public GameObject totalScoreTag;
    [SerializeField] public GameObject highestScoreTag;
    [SerializeField] public GameObject musicObj;
    //[SerializeField] public GameObject MusicController;
    //[SerializeField] public TextAsset txtobj;
   // [SerializeField] public GameObject Edges;
    public static Gamemanager GameManagerInstance;
    public Camera mainCam;//0, 2.5, -10, 25,0,0
    public List<GameObject> Balls = new List<GameObject>();
    public GameObject Newball;
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
    void Start()
    {
       // Debug.LogWarning(test.text);
        Application.targetFrameRate = 60;
        GameManagerInstance = this;
        Debug.LogWarning("game manager çalıişıyor");
        mainCam = Camera.main;
        dataBase = new DataSaveLoad();
        writeDataBase = true;
        SetResolution();
        difficulity = new Difficulity();
        score = new Score();
        difficulity.SetRoadSpeed();
        Debug.LogWarning("CUrrent level : " + dataBase.currentLevel);
        Debug.LogWarning("Set road speed : " + RoadSpeed);
        //Balls.Add(gameObject); // İLK ANA TOPU LİSTEYE EKLER
        DeActivateAllocations();
        Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(0).gameObject.SetActive(true);
        Balls.Add(Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(0).gameObject);
        restartButton.SetActive(false);
        gameWon = false;
        stopMovement = false;
        organiserTime = 1;
        SetTables();
        UpdateLevelTag();
        musicStarted = false;
    }
    
     private void SetResolution() {
        // var height = (int)(dataBase.resulotionHeight * 0.8f);
        // var width = (int)(dataBase.resulotionWidth * 0.8f); 
        // Screen.SetResolution(width, height, true);
        // Debug.LogWarning("Height:"+height + " Width:" + width);
      }
      
    void Update()
    {
       // print(StartTheGame);
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

                //float[] edges = DetermineEdges();

                move.x = Mathf.Clamp(move.x, -5f, 4.5f);// -3f,3f   / -2.2f, 2.2f
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


 public void DeActivateAllocations()
 {
   for (int i = 0; i < Gamemanager.GameManagerInstance.AllocatedBalls.transform.childCount; i++)
    {
        Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(i).gameObject.SetActive(false);
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
        int counter = Balls.Count;
        DeActivateAllocations();
        Balls =  new List<GameObject>();
        for (int i = 0; i < Gamemanager.GameManagerInstance.AllocatedBalls.transform.childCount; i++)
            {
                if(counter <= 0)
                {
                    break;
                }
                GameObject currentChild = Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(i).gameObject;
                if(currentChild.activeInHierarchy == false)//currentChild.GetComponent<Renderer>().enabled==false
                {
                    //print("nomash");
                    currentChild.SetActive(true);
                   // currentChild.GetComponent<Renderer>().enabled=true;
                    Gamemanager.GameManagerInstance.Balls.Add(currentChild);
                    counter-- ;
                }
            }
        isThreadExecuting = false;

    }
public void GameContinues()
{
    if (Balls.Count <= 0 || gameWon == true)
    {
        StartTheGame = false;
        restartButton.SetActive(true);
        MoveByTouch = false;
        RoadSpeed = 0;
        stopMovement = true;
        //print("GAME OVER");
    } 
}

public void UpdateCountTag()
{
    TextMeshPro textmeshPro = PlayerCountTag.GetComponent<TextMeshPro>();
    if(Balls.Count != 0)
    {
        if(gameWon == true)
        {
            textmeshPro.SetText("VICTORY! \n Score:" + score.currentScore);
            dataBase.currentLevel++;
        }
        else
        {
            textmeshPro.SetText(Convert.ToString(Balls.Count));
        }
    }
    else
    {
    textmeshPro.SetText("<color=#FF0000><uppercase>GAME OVER<uppercase></color>");
    Vector3 loc = new Vector3(restartButton.transform.position.x,restartButton.transform.position.y+0.6f,restartButton.transform.position.z);
    //
   // Vector3 loc = new Vector3(PlayerCountTag.transform.position.x,5,PlayerCountTag.transform.position.z);
    PlayerCountTag.transform.SetParent(mainCam.gameObject.transform);
    PlayerCountTag.transform.position = loc;
    PlayerCountTag.transform.localScale = new Vector3(0.62f,0.62f,0.62f);
    stopMovement = true;
    MusicControl.musicInstance.StopSound();
    }
    if((gameWon == true || Balls.Count <= 0) && writeDataBase == true)
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
    //PlayerCountTag.GetComponent<TMPro.TextMeshProUGUI>().text = "as";//Convert.ToString(Balls.Count);
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













// public float[] DetermineEdges()
// {
//     float maxDiff_right = 0;
//     float diff_right = 0;
//     float mindiff_left = 1000;
//     float diff_left = 0;
//     for (int i = 0; i < Balls.Count; i++)
//     {
//         GameObject currentBall = Balls.ElementAt(i);
//         float radius = currentBall.transform.parent.gameObject.transform.localScale.x;


//         diff_right = transform.position.x+currentBall.transform.position.x+radius - 4.35f;
//         diff_left = transform.position.x+currentBall.transform.position.x + 4.35f;

//         if(diff_right > maxDiff_right)//diff > maxDiff
//         {
//             maxDiff_right = diff_right;
//         }
//         if(diff_left < mindiff_left)//diff > maxDiff
//         {
//             mindiff_left = diff_left;
//         }
//     }
//     float newEdge = 2.2f;
//     float[] edgeArray = new float[2];
//     edgeArray[0]=(newEdge);//2.2f - maxDiff_right
//     edgeArray[1]=(-2.2f - mindiff_left*2);

//     return edgeArray;
//     // Önce bütün toplara bak,
//     // Sağ:eğer herhangi bir top un x+radius'u road.x'ten büyük ise aralıgı fark kadar sağdan küçült
//     // Sol:eğer herhangi bir top un x-radius'u road.x'ten küçük ise aralıgı fark kadar soldan küçült
//     // Eğer böyle bir top yoksa aralığı default değer olarak ayarla [2.2f,-2.2f]
//      //   float radius = currentBall.transform.parent.gameObject.transform.localScale.x;
    
// }


    // private void OnTriggerEnter(Collider other)
    // {
    //     print("qwe");
    //     // MathOperations.PerformOperation(other);
    //     //  if (Balls.Count <= 0)
    //     //  {
    //     //      StartTheGame = false;
    //     //  }

    // }

    // private void moveActiveBalls(Ray ray,float distance)
    // {
    //     print(Balls.Count);
    //     if (Balls.Count > 1)
    //     {
    //         for (int i = 1; i < Balls.Count; i++)
    //         {
    //             Vector3 mousePos = ray.GetPoint(distance);
    //             Vector3 move = Balls.ElementAt(i).transform.position;
    //             move.x = mousePos.x+move.x;
    //             //move.x = Mathf.Clamp(move.x, -2.2f, 2.2f);
    //             move.z = -7f;

    //             var player = Balls.ElementAt(i).transform.position;
    //             player = new Vector3(Mathf.Lerp(player.x, move.x, Time.deltaTime * (SwipeSpeed + 10f)), player.y, player.z);

    //             //Balls.ElementAt(i).transform.position = player;
    //             // move.x =  Balls.ElementAt(i).transform.position.x + move.x;
    //             // var newLocation = new Vector3(Mathf.Lerp(Balls.ElementAt(i).transform.position.x, move.x, Time.deltaTime * (SwipeSpeed + 10f)), Balls.ElementAt(i).transform.position.y, Balls.ElementAt(i).transform.position.z);
    //             // Balls.ElementAt(i).transform.position = newLocation;
    //         }
    //         //circleFormation(Balls.Count);
    //     }
    // }
//  public void circleFormation(int howMany)
//  {
//      float value = 0.7f;
//      for (int i = 1; i < howMany; i++)
//      {
//         float radius = howMany;
//         float angle = i * Mathf.PI * 2f / radius;
//         Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(angle) * value, 0, Mathf.Sin(angle) * value));
//         Balls.ElementAt(i).transform.position = newPos;
//      }
//  }


        // if (other.CompareTag("add"))
        // {
        //     var NoAdd = Int16.Parse(other.transform.GetChild(0).name);

        //     for (int i = 0; i < NoAdd; i++)
        //     {
        //       GameObject Ball =  Instantiate(Newball, Balls.ElementAt(Gamemanager.GameManagerInstance.Balls.Count - 1).position + new Vector3(0f, 0f, 0.5f),
        //             Quaternion.identity);//deiş
              
        //       Balls.Add(Ball.transform);
        //     }
        //     other.GetComponent<Collider>().enabled = false;
        // }

        // if ((other.CompareTag("sub") || other.CompareTag("div")) && Balls.Count > 0)
        // {
        //   var NoSub = 0;

        //     if(other.CompareTag("sub"))
        //     NoSub = Int16.Parse(other.transform.GetChild(0).name);
        //     else
        //     {
        //        NoSub = (Balls.Count/ (Int16.Parse(other.transform.GetChild(0).name)));
        //     }

        //     if(NoSub > Balls.Count)
        //     {
        //         NoSub=Balls.Count;
        //     }
        //   //  if (Gamemanager.GameManagerInstance.Balls.Count > NoSub)
        //  //   {
        //      print(NoSub);
        //         for (int i = 0; i < NoSub; i++)
        //         {
        //            Balls.ElementAt(Balls.Count - 1).gameObject.SetActive(false);
        //            Balls.RemoveAt(Balls.Count - 1);
        //         }
        //         Instantiate(Explosion,   Gamemanager.GameManagerInstance.
        //             Balls.ElementAt(Balls.Count - 1).position, Quaternion.identity);
        // }
        // if (other.CompareTag("mul"))
        // {
        //     var NoAdd = (Int16.Parse(other.transform.GetChild(0).name)-1) * Balls.Count;

        //     for (int i = 0; i < NoAdd; i++)
        //     {
        //       GameObject Ball =  Instantiate(Newball, Balls.ElementAt(Gamemanager.GameManagerInstance.Balls.Count - 1).position + new Vector3(0f, 0f, 0.5f),
        //             Quaternion.identity);//deiş
              
        //       Balls.Add(Ball.transform);
        //     }
        //     other.GetComponent<Collider>().enabled = false;
        // }





            // print(Balls.Count);
            // Instantiate(Explosion, Balls.ElementAt(Balls.Count - 1).position, Quaternion.identity);
            // Balls.ElementAt(Balls.Count - 1).gameObject.SetActive(false);
            // Balls.RemoveAt(Balls.Count - 1);
            // other.GetComponent<Collider>().enabled = false;4

            // for (int i = 1; i < Balls.Count; i++)
            // {
            //     var FirstBall = Balls.ElementAt(i - 1);
            //     var SectBall = Balls.ElementAt(i);

            //     var DesireDistance = Vector3.Distance(FirstBall.position,SectBall.position );

            //     if (DesireDistance <= Distance)
            //     {
            //         SectBall.position = new Vector3(Mathf.Lerp(SectBall.position.x,FirstBall.position.x,SwipeSpeed * Time.deltaTime)
            //         ,SectBall.position.y,Mathf.Lerp(SectBall.position.z,FirstBall.position.z + 0.5f,SwipeSpeed * Time.deltaTime));//SectBall
            //     }
            // }


            //  for (int i = 1; i < Balls.Count; i++)
            // {
            //     var FirstBall = Balls.ElementAt(i - 1);
            //     var SectBall = Balls.ElementAt(i);

            //     var DesireDistance = Vector3.Distance(FirstBall.position,SectBall.position );

            //     if (DesireDistance <= Distance)
            //     {
            //         SectBall.position = new Vector3(Mathf.Lerp(SectBall.position.x,FirstBall.position.x,SwipeSpeed * Time.deltaTime)
            //         ,SectBall.position.y,Mathf.Lerp(SectBall.position.z,FirstBall.position.z + 0.5f,SwipeSpeed * Time.deltaTime));
            //     }
            // }  


        // if (other.CompareTag("blue"))
        // {
        //     other.transform.parent = null;
        //     other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
        //     other.gameObject.AddComponent<StackMgr>();
        //     other.gameObject.GetComponent<Collider>().isTrigger = true;
        //     other.tag = gameObject.tag;
        //     other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        //     Balls.Add(other.transform);
        // }
