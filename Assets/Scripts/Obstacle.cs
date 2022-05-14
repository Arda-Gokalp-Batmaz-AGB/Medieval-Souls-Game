using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool moveable;
    [SerializeField] private GameObject parentObject;
    private string obstacleType;
    private Vector3 currentRotateDirection;
    [SerializeField] private bool rotateRight;
    void Start()
    {
        this.currentRotateDirection = Vector3.forward;
        this.obstacleType = parentObject.tag;//taga göre yap
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
        // if(moveable == true)
        //     moveObstacle();
    }

    void FixedUpdate()
    {
        if(moveable == true)
            moveObstacle();        
    }

    private void moveObstacle()
    {
        Vector3 rotationDir = new Vector3(0,0,0);
        float speed = 0.0f;
        if(string.Equals(obstacleType,"RotatingObstacle"))
        {
            //rotationDir = Vector3.down;
            if(rotateRight == true)
           // rotationDir = Vector3.forward;
           rotationDir = Vector3.down;
            else
            {
           // rotationDir = Vector3.forward * -1;
           rotationDir = Vector3.down * -1;
            }
            speed = 70f;
            transform.Rotate(rotationDir * speed * Time.deltaTime);
           //transform.RotateAround(new Vector3(-1.40f,0.45f,-0.143f),rotationDir,speed * Time.deltaTime);
          //  print("qwe");
        }
        if(string.Equals(obstacleType,"UpdownObstacle"))
        {
            float currentAngle = transform.rotation.z;
            float currentAngle_2 = transform.rotation.x;
            speed = 76f;
          //  print(transform.rotation.x);     
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
            //transform.RotateAround(new Vector3(3.5f,0f,-2f),currentRotateDirection, speed * Time.deltaTime);  //3.5f,0.5f,-2f
            transform.RotateAround(gameObject.transform.GetChild(0).transform.position,currentRotateDirection, speed * Time.deltaTime);  //3.5f,0.5f,-2f
            }      
            // else if ( currentAngle > 0f)
            // {
            //     currentRotateDirection = currentRotateDirection * -1;

            // }
        }
    }

}





        // if(string.Equals(obstacleType,"UpdownObstacle"))
        // {
        //     float currentAngle = transform.rotation.z;
        //     if (currentAngle <= -0.70f)
        //     {
        //    // rotationDir = Vector3.forward;
        //     speed = 70f;
        //     //transform.Rotate(rotationDir * speed * Time.deltaTime,Space.World); 
        //     transform.RotateAround(new Vector3(-3.5f,0.5f,-2f),currentRotateDirection = Vector3.forwardtationDir, speed * Time.deltaTime);       
        //     }
        //     if(currentAngle <= 0f)
        //     {
        //     rotationDir = Vector3.forward * -1;
        //     speed = 70f;
        //     //transform.Rotate(rotationDir * speed * Time.deltaTime,Space.World); 
        //     transform.RotateAround(new Vector3(-3.5f,0.5f,-2f),currentRotateDirection, speed * Time.deltaTime);       
        //     print(currentAngle);
        //     }
        //     transform.RotateAround(new Vector3(-3.5f,0.5f,-2f),currentRotateDirection, speed * Time.deltaTime);       
        // }