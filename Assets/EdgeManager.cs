using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is used for limiting the movements 
     of the user by not allowing the user to move on out of the bounds of the game road.
*/
public class EdgeManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If it is a player, does not allows moving out of road
        if(other.CompareTag("Player"))
        {
             var player = Gamemanager.GameManagerInstance.transform.position;
             player = new Vector3(Mathf.Lerp(player.x, player.x -0.5f, Time.deltaTime * (Gamemanager.GameManagerInstance.SwipeSpeed + 10f)), player.y, player.z);
             Gamemanager.GameManagerInstance.transform.position = player;
             print(Gamemanager.GameManagerInstance.transform.position.x);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
