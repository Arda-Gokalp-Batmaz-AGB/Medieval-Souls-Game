using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


/*
    This class manages operations that will be performed on the player's army when the player collides with the gates. 
     Also, it is managing how win and war events trigger.
*/
public class MathOperations : MonoBehaviour
{
    /*
        This method performs the mathematical operation which player's army pass. Also checks if
        win or war events are triggered
    */
    public static void PerformOperation(Collider other)
    {
        if (other.CompareTag("result")) // result tag is attached to the gate ways which has GateData class
        {
            GateData Gate_data = other.GetComponent<GateData>();
            if(string.Equals(Gate_data.operation.operationType,"add") || string.Equals(Gate_data.operation.operationType,"mul"))
            {
                Gate_data.resultObject.name = Convert.ToString(Gate_data.operation.getResult());
                var NoAdd = Int32.Parse(Gate_data.resultObject.name);

                for (int i = 0; i < Gamemanager.GameManagerInstance.AllocatedArmy.transform.childCount; i++)
                {
                    if(NoAdd <= 0)
                    {
                        break;
                    }
                    GameObject currentChild = Gamemanager.GameManagerInstance.AllocatedArmy.transform.GetChild(i).gameObject;
                    if(currentChild.activeInHierarchy == false)
                    {
                        currentChild.SetActive(true);
                        Gamemanager.GameManagerInstance.Army.Add(currentChild);
                        NoAdd-- ;
                    }
                    else
                    {
                    }
                }

                Gate_data.GetComponent<Collider>().enabled = false;
            }
            else if (string.Equals(Gate_data.operation.operationType,"sub") || string.Equals(Gate_data.operation.operationType,"div"))
            {
                Gate_data.resultObject.name = Convert.ToString(Gate_data.operation.getResult());
                var NoSub = Int32.Parse(Gate_data.resultObject.name);

                if(NoSub > Gamemanager.GameManagerInstance.Army.Count)
                {
                    NoSub=Gamemanager.GameManagerInstance.Army.Count;
                }
                    for (int i = 0; i < NoSub; i++)
                    {
                    Gamemanager.GameManagerInstance.Army.ElementAt( Gamemanager.GameManagerInstance.Army.Count - 1).gameObject.SetActive(false);
                    Gamemanager.GameManagerInstance.Army.RemoveAt(Gamemanager.GameManagerInstance.Army.Count - 1);
                    }
                    if(Gamemanager.GameManagerInstance.Army.Count - 1 >= 0)
                    Instantiate(Gamemanager.GameManagerInstance.Explosion,   Gamemanager.GameManagerInstance.
                        Army.ElementAt(Gamemanager.GameManagerInstance.Army.Count - 1).transform.position, Quaternion.identity);

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
    }
}