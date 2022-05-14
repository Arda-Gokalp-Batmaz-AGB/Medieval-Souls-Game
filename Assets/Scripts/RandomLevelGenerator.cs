using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RandomLevelGenerator : MonoBehaviour
{
    public GameObject[] roadList;
    public GameObject[] obstacleList;

    public GameObject[] gateList;

    [SerializeField] GameObject roadParent;
    [SerializeField] GameObject roadTile;
    [SerializeField] GameObject winRoadTile;

     public static RandomLevelGenerator RandomLevelGeneratorInstance;
    Vector3 nextSpawnPoint;
    private readonly int objectCountPerRoad = 3;
    public int roadLength;
    void Start()
    {
        //SpawnRoads();
    //private readonly int roadLength = 20;
        Debug.LogWarning("RANDOM LEVEL GENERATOR ÇALIŞIYOR");
        RandomLevelGeneratorInstance = this;
        Gamemanager.GameManagerInstance.difficulity.SetRoadLength();
        CreateRoadList();
        CreateObstacleList();
        CreateGateList();
        nextSpawnPoint = new Vector3(0,0,0);
        SpawnRoads();
        //Debug.LogWarning(roadParent.transform.Find("Gates").transform);
        // for (int i = 0; i < roadList.Length; i++)
        // {
        //     createRoadObjects(roadList[i]);
        // }
        Debug.LogWarning("Road Length = " + roadLength);
    }

    // Update is called once per frame
    void Update()
    {
       // SpawnRoads();
    }

    private void CreateRoadList()
    {
        roadList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject currentRoad = transform.GetChild(i).gameObject;
            roadList[i] = currentRoad;
           // Debug.LogWarning(currentRoad);
        }
    }
    private void CreateObstacleList()
    {
        obstacleList = Resources.LoadAll<GameObject>("Prefabs/Obstacles");
    }
    private void CreateGateList()
    {
        gateList = Resources.LoadAll<GameObject>("Prefabs/Gates");
    }
    
    private GameObject createRoadObjects()//GameObject currentRoad
    {
        // for (int i = 0; i < 1; i++)
        // {
            string objectType = ChooseType();
            GameObject currentObject = null;
            if(string.Equals(objectType,"Obstacles"))
            {
                currentObject = ChooseObsctacle();
            //    Instantiate(currentObject,new Vector3(-1.5f,0.5f,-1.5f),Quaternion.identity,roadParent.transform.Find("Obstacles").transform);
            }
            else if(string.Equals(objectType,"Gates"))
            {
                currentObject = ChooseGates();
           //     Instantiate(currentObject,new Vector3(-1.5f,0.5f,-1.5f),Quaternion.identity,roadParent.transform.Find("Gates").transform);
            }
      //  }
      return currentObject;
    }
    private string ChooseType()
    {
        string [] types = {"Gates","Obstacles","Obstacles"};//oran artırıldı
        int temp = UnityEngine.Random.Range(0,types.Length);
        string choosenType = types[temp];
        return choosenType;
    }
    private GameObject ChooseObsctacle()
    {
        int temp = UnityEngine.Random.Range(0,obstacleList.Length);
        GameObject choosenObstacle = obstacleList[temp];

        return choosenObstacle;
    }

    private GameObject ChooseGates()
    {
        int temp = UnityEngine.Random.Range(0,gateList.Length-1);
        GameObject choosenGate = gateList[temp];
        //GameObject choosenGate = gateList[temp];

        return choosenGate;       
    }

    private void SpawnRoads()
    {
        int initialAddCount = 2;
        GameObject temp = null;
        temp = Instantiate(roadTile,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
        nextSpawnPoint = temp.transform.GetChild(2).transform.position;

        for (int i = 0; i < initialAddCount; i++)
        {
        temp = Instantiate(roadTile,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
        GameObject prefabGate = gateList[gateList.Length-1];
        Instantiate(prefabGate,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
        nextSpawnPoint = temp.transform.GetChild(2).transform.position;
        }

        for (int i = initialAddCount; i < roadLength; i++)
        {
        temp = Instantiate(roadTile,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
        GameObject randomObject = createRoadObjects();
        Instantiate(randomObject,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
        nextSpawnPoint = temp.transform.GetChild(2).transform.position;
        }
        temp = Instantiate(roadTile,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
        nextSpawnPoint = temp.transform.GetChild(2).transform.position;

        Instantiate(winRoadTile,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
    }
}






    //     for (int i = 0; i < initialAddCount; i++)
    //     {
    //     temp = Instantiate(roadTile,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
    //     prefabObject = gateList[0];
    //    // prefabObject.GetComponent<GateManager>().ManualInitiliase("Basic","add");
    //     GameObject g = Instantiate(prefabObject,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
    //     g.GetComponent<GateManager>().SwitchManuality();
    //    // Debug.LogWarning(prefabObject.GetComponent<GateManager>().manuelCreation);
    //    // bool t = initialObject.GetComponent<GateManager>().manuelCreation;
    //     nextSpawnPoint = temp.transform.GetChild(2).transform.position;
    //     }
    //     //prefabObject.GetComponent<GateManager>().SwitchManuality();
    //   //  gateList[0].GetComponent<GateManager>().SwitchManuality();
    //    // gateList[0].GetComponent<GateManager>().InitiliaseGates();
    //     GameObject gs = Instantiate(prefabObject,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
    //     gs.GetComponent<GateManager>().SwitchManuality();
    //     Debug.LogWarning(prefabObject.GetComponent<GateManager>().manuelCreation);









    //     GameObject temp = null;
    //     GameObject prefabObject = gateList[0];
    //     for (int i = 0; i < initialAddCount; i++)
    //     {
    //     temp = Instantiate(roadTile,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
    //     prefabObject = gateList[0];
    //    // prefabObject.GetComponent<GateManager>().ManualInitiliase("Basic","add");
    //    prefabObject.GetComponent<GateManager>().SwitchManuality();
    //     Instantiate(prefabObject,nextSpawnPoint,Quaternion.identity,roadParent.transform.Find("RoadList").transform);
    //    // bool t = initialObject.GetComponent<GateManager>().manuelCreation;
    //     nextSpawnPoint = temp.transform.GetChild(2).transform.position;
    //     }




// public class RandomLevelGenerator : MonoBehaviour
// {
//     public GameObject[] roadList;
//     public GameObject[] obstacleList;

//     public GameObject[] gateList;

//     [SerializeField] GameObject roadParent;
//     private readonly int objectCountPerRoad = 3;
//     void Start()
//     {
//         CreateRoadList();
//         CreateObstacleList();
//         CreateGateList();
//         //Debug.LogWarning(roadParent.transform.Find("Gates").transform);
//         for (int i = 0; i < roadList.Length; i++)
//         {
//             createRoadObjects(roadList[i]);
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//     }

//     private void CreateRoadList()
//     {
//         roadList = new GameObject[transform.childCount];
//         for (int i = 0; i < transform.childCount; i++)
//         {
//             GameObject currentRoad = transform.GetChild(i).gameObject;
//             roadList[i] = currentRoad;
//            // Debug.LogWarning(currentRoad);
//         }
//     }
//     private void CreateObstacleList()
//     {
//         obstacleList = Resources.LoadAll<GameObject>("Prefabs/Obstacles");
//     }
//     private void CreateGateList()
//     {
//         gateList = Resources.LoadAll<GameObject>("Prefabs/Gates");
//     }
    
//     private void createRoadObjects(GameObject currentRoad)
//     {
//         for (int i = 0; i < 1; i++)
//         {
//             string objectType = ChooseType();
//             GameObject currentObject = null;
//             if(string.Equals(objectType,"Obstacles"))
//             {
//                 currentObject = ChooseObsctacle();
//                 Instantiate(currentObject,new Vector3(-1.5f,0.5f,-1.5f),Quaternion.identity,roadParent.transform.Find("Obstacles").transform);
//             }
//             else if(string.Equals(objectType,"Gates"))
//             {
//                 currentObject = ChooseGates();
//                 Instantiate(currentObject,new Vector3(-1.5f,0.5f,-1.5f),Quaternion.identity,roadParent.transform.Find("Gates").transform);
//             }
//         }
//     }
//     private string ChooseType()
//     {
//         string [] types = {"Gates","Obstacles"};
//         int temp = UnityEngine.Random.Range(0,types.Length);
//         string choosenType = types[temp];
//         return choosenType;
//     }
//     private GameObject ChooseObsctacle()
//     {
//         int temp = UnityEngine.Random.Range(0,obstacleList.Length);
//         GameObject choosenObstacle = obstacleList[temp];

//         return choosenObstacle;
//     }

//     private GameObject ChooseGates()
//     {
//         int temp = UnityEngine.Random.Range(0,gateList.Length);
//         GameObject choosenGate = gateList[temp];

//         return choosenGate;       
//     }

//     private void SpawnRoads()
//     {
        
//     }
// }
