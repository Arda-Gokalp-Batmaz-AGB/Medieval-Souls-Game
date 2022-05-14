using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MathOperations : MonoBehaviour
{
    public static void PerformOperation(Collider other)
    {
        if (other.CompareTag("result"))
        {
            GateData Gate_data = other.GetComponent<GateData>();
            if(string.Equals(Gate_data.operation.operationType,"add") || string.Equals(Gate_data.operation.operationType,"mul"))
            {
                Gate_data.resultObject.name = Convert.ToString(Gate_data.operation.getResult());
                var NoAdd = Int32.Parse(Gate_data.resultObject.name);

                for (int i = 0; i < Gamemanager.GameManagerInstance.AllocatedBalls.transform.childCount; i++)
                {
                    if(NoAdd <= 0)
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
                        NoAdd-- ;
                    }
                    else
                    {
                    }
                }

                //

                //
                Gate_data.GetComponent<Collider>().enabled = false;
            }
            else if (string.Equals(Gate_data.operation.operationType,"sub") || string.Equals(Gate_data.operation.operationType,"div"))
            {
                Gate_data.resultObject.name = Convert.ToString(Gate_data.operation.getResult());
                var NoSub = Int32.Parse(Gate_data.resultObject.name);

                if(NoSub > Gamemanager.GameManagerInstance.Balls.Count)
                {
                    NoSub=Gamemanager.GameManagerInstance.Balls.Count;
                }
                    for (int i = 0; i < NoSub; i++)
                    {
                    Gamemanager.GameManagerInstance.Balls.ElementAt( Gamemanager.GameManagerInstance.Balls.Count - 1).gameObject.SetActive(false);
                    Gamemanager.GameManagerInstance.Balls.RemoveAt(Gamemanager.GameManagerInstance.Balls.Count - 1);
                    }
                    if(Gamemanager.GameManagerInstance.Balls.Count - 1 >= 0)
                    Instantiate(Gamemanager.GameManagerInstance.Explosion,   Gamemanager.GameManagerInstance.
                        Balls.ElementAt(Gamemanager.GameManagerInstance.Balls.Count - 1).transform.position, Quaternion.identity);

                Gate_data.GetComponent<Collider>().enabled = false;                
            }
        }
        if(other.CompareTag("triggerwin"))    
        {
            Gamemanager.GameManagerInstance.gameWon = true;
            Gamemanager.GameManagerInstance.StartTheGame = false;
            print("YOU WON THE GAME!!!");
        }    
        if(other.CompareTag("triggerwar"))
        {
        Gamemanager.GameManagerInstance.MoveByTouch = false;
        Gamemanager.GameManagerInstance.RoadSpeed = 0;
        Gamemanager.GameManagerInstance.stopMovement = true;
        War.warInstance.StartWar();
        }



//////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////
 
    }
}




    //    if (other.CompareTag("add") || other.CompareTag("mul"))
    //     {
    //         var NoAdd = 0;
    //         if(other.CompareTag("add"))
    //         NoAdd = Int16.Parse(other.transform.GetChild(0).name);
    //         else
    //         {
    //         NoAdd = (Int16.Parse(other.transform.GetChild(0).name)-1) * Gamemanager.GameManagerInstance.Balls.Count;
    //         }
    //        // Destroy(other);

    //         for (int i = 0; i < Gamemanager.GameManagerInstance.AllocatedBalls.transform.childCount; i++)
    //         {
    //             if(NoAdd <= 0)
    //             {
    //                 break;
    //             }
    //             GameObject currentChild = Gamemanager.GameManagerInstance.AllocatedBalls.transform.GetChild(i).gameObject;
    //             if(currentChild.activeInHierarchy == false)//currentChild.GetComponent<Renderer>().enabled==false
    //             {
    //                 //print("nomash");
    //                 currentChild.SetActive(true);
    //                // currentChild.GetComponent<Renderer>().enabled=true;
    //                 Gamemanager.GameManagerInstance.Balls.Add(currentChild);
    //                 NoAdd-- ;
    //             }
    //             else
    //             {
    //             }
    //         }
    //         other.GetComponent<Collider>().enabled = false;
    //     }

    //     if (other.CompareTag("sub") || other.CompareTag("div"))
    //     {
    //         var NoSub = 0;

    //         if(other.CompareTag("sub"))
    //         NoSub = Int16.Parse(other.transform.GetChild(0).name);
    //         else
    //         {
    //            NoSub = (Gamemanager.GameManagerInstance.Balls.Count/ (Int16.Parse(other.transform.GetChild(0).name)));
    //         }

    //         if(NoSub > Gamemanager.GameManagerInstance.Balls.Count)
    //         {
    //             NoSub=Gamemanager.GameManagerInstance.Balls.Count;
    //         }
    //          //print(NoSub);
    //             for (int i = 0; i < NoSub; i++)
    //             {
    //                //Gamemanager.GameManagerInstance.Balls.ElementAt( Gamemanager.GameManagerInstance.Balls.Count - 1).GetComponent<Renderer>().enabled=false;
    //                Gamemanager.GameManagerInstance.Balls.ElementAt( Gamemanager.GameManagerInstance.Balls.Count - 1).gameObject.SetActive(false);
    //                Gamemanager.GameManagerInstance.Balls.RemoveAt(Gamemanager.GameManagerInstance.Balls.Count - 1);
    //             }
    //             if(Gamemanager.GameManagerInstance.Balls.Count - 1 >= 0)
    //             Instantiate(Gamemanager.GameManagerInstance.Explosion,   Gamemanager.GameManagerInstance.
    //                 Balls.ElementAt(Gamemanager.GameManagerInstance.Balls.Count - 1).transform.position, Quaternion.identity);

    //         // if (Gamemanager.GameManagerInstance.Balls.Count <= 0)
    //         // {
    //         //     Gamemanager.GameManagerInstance.StartTheGame = false;
    //         // }
    //         other.GetComponent<Collider>().enabled = false;
    //     }