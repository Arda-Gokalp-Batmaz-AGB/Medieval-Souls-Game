using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    This is a class that is used in order to manage how obstacles in the game are moving and rendering. 
     On the prefab obstacles, each obstacle object regardless of its type is using this script class.
*/
public class Obstacle : MonoBehaviour
{
    private bool moveable; // States if obstacle is moveable
    [SerializeField] private GameObject parentObject; //Obstacles can be present in obstacles groups this parent indicates the group of obstacle
    private string obstacleType; // There are 3 types of obstacles
    private Vector3 currentRotateDirection; // Indicates obstacle is rotating in which axis
    [SerializeField] private bool rotateRight; // States the direction of rotation

    /*
         This method runs on the obstacle object created. According to the type of obstacle, sets its moveable variable.
    */
    void Start()
    {
        this.currentRotateDirection = Vector3.forward;
        this.obstacleType = parentObject.tag;
        if(string.Equals(obstacleType,"StandingObstacle"))
        {
            moveable = false;
        }
        if(string.Equals(obstacleType,"RotatingObstacle") || string.Equals(obstacleType,"UpdownObstacle"))
        {
            moveable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    /*
        In order to prevent frame misses on the obstacles, 
        objects are rendered in the fixedupdate in this way it updates in every fixed time.
    */
    void FixedUpdate()
    {
        if(moveable == true)
            moveObstacle();        
    }

    /*
        There are 3 types of obstacles, 2 of them which are "Rotating Obstacle" and "Updown Obstacle" which are moveable. 
        This function controls how moveable objects rotate and move in the game road.
    */
    private void moveObstacle()
    {
        Vector3 rotationDir = new Vector3(0,0,0);
        float speed = 0.0f;
        if(string.Equals(obstacleType,"RotatingObstacle"))
        {
            if(rotateRight == true)
           rotationDir = Vector3.down;
            else
            {
           rotationDir = Vector3.down * -1;
            }
            speed = 70f;
            transform.Rotate(rotationDir * speed * Time.deltaTime);
        }
        if(string.Equals(obstacleType,"UpdownObstacle"))
        {
            float currentAngle = transform.rotation.z;
            float currentAngle_2 = transform.rotation.x;
            speed = 76f;
            if ((currentAngle <= -0.75f || currentAngle > 0f ) && rotateRight == true)
            {
                currentRotateDirection = currentRotateDirection * -1;
            }
            if ((currentAngle_2 <= -0.75f || currentAngle_2 > 0f ) && rotateRight == false)
            {
                currentRotateDirection = currentRotateDirection * -1;
            }
            if(rotateRight==true)
            transform.RotateAround(gameObject.transform.GetChild(0).transform.position,currentRotateDirection, speed * Time.deltaTime); // new Vector3(-3.5f,0f,-2f) -3.5f,0.5f,-2f
            else
            {
            transform.RotateAround(gameObject.transform.GetChild(0).transform.position,currentRotateDirection, speed * Time.deltaTime);  //3.5f,0.5f,-2f
            }      
        }
    }
}