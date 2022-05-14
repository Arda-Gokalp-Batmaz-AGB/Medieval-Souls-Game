using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class StackMgr : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       // print(other.tag);
        if(other.CompareTag("obstacle"))
        {
            print("çarpıt");
            Gamemanager.GameManagerInstance.Balls.Remove(gameObject);
            Gamemanager.GameManagerInstance.ReOrganiseBalls();
            //StartCoroutine(Gamemanager.GameManagerInstance.Example());

            gameObject.SetActive(false);
        }
        else
        {
            MathOperations.PerformOperation(other);
        }
        // if(other.CompareTag("Player"))
        //    {
        //     print("sad");
        // }
    }


}


















        // if (other.CompareTag("add") || other.CompareTag("mul"))
        // {
        //     var NoAdd = 0;
        //     if(other.CompareTag("add"))
        //     NoAdd = Int16.Parse(other.transform.GetChild(0).name);
        //     else
        //     {
        //     NoAdd = (Int16.Parse(other.transform.GetChild(0).name)-1) * Gamemanager.GameManagerInstance.Balls.Count;
        //     }
        //    // Destroy(other);
        //     for (int i = 0; i < NoAdd; i++)
        //     {
        //         GameObject Ball =  Instantiate(Gamemanager.GameManagerInstance.Newball, Gamemanager.GameManagerInstance.Balls.ElementAt(Gamemanager.GameManagerInstance.Balls.Count - 1).position + new Vector3(0f, 0f, 0.5f),
        //             Quaternion.identity);//Gamemanager.GameManagerInstance.Balls.Count - 1
        //         Gamemanager.GameManagerInstance.Balls.Add(Ball.transform);
        //     }
        //     other.GetComponent<Collider>().enabled = false;
        // }

        // if (other.CompareTag("sub") || other.CompareTag("div"))
        // {
        //     var NoSub = 0;

        //     if(other.CompareTag("sub"))
        //     NoSub = Int16.Parse(other.transform.GetChild(0).name);
        //     else
        //     {
        //        NoSub = (Gamemanager.GameManagerInstance.Balls.Count/ (Int16.Parse(other.transform.GetChild(0).name)));
        //     }

        //     if(NoSub > Gamemanager.GameManagerInstance.Balls.Count)
        //     {
        //         NoSub=Gamemanager.GameManagerInstance.Balls.Count;
        //     }
        //   //  if (Gamemanager.GameManagerInstance.Balls.Count > NoSub)
        //  //   {
        //      print(NoSub);
        //         for (int i = 0; i < NoSub; i++)
        //         {
        //            Gamemanager.GameManagerInstance.Balls.ElementAt( Gamemanager.GameManagerInstance.Balls.Count - 1).gameObject.SetActive(false);
        //            Gamemanager.GameManagerInstance.Balls.RemoveAt(Gamemanager.GameManagerInstance.Balls.Count - 1);
        //         }
        //         Instantiate(Gamemanager.GameManagerInstance.Explosion,   Gamemanager.GameManagerInstance.
        //             Balls.ElementAt(Gamemanager.GameManagerInstance.Balls.Count - 1).position, Quaternion.identity);
        //  //   }

        //     if (Gamemanager.GameManagerInstance.Balls.Count <= 0)
        //     {
        //         Gamemanager.GameManagerInstance.StartTheGame = false;
        //     }
        //     other.GetComponent<Collider>().enabled = false;
        // }























        // if (other.CompareTag("blue"))
        // {
        //      other.transform.parent = null;
        //      other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
        //      other.gameObject.AddComponent<StackMgr>();
        //      other.gameObject.GetComponent<Collider>().isTrigger = true;
        //      other.tag = gameObject.tag;
        //      other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        //      Gamemanager.GameManagerInstance.Balls.Add(other.transform);
        // }
        