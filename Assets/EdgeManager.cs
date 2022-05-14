using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
             print("sad");
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
