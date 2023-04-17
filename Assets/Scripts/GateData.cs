using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

/*
    There are two possible ways for each gate object and 
    this class holds the data of these possible ways on the gate object.
*/
public class GateData : MonoBehaviour
{
    public MathFunctionGenerator operation;
    [SerializeField] public GameObject Gate; //Unused variable
    [SerializeField] public GameObject resultObject; //Holds the way hitbox object of gate
    [SerializeField] public GameObject equationTextObject; //Holds operation text
    public TextMeshPro textmeshPro; // Text writer class

    /*
        Initializes text writer on the relevant text object
    */
    void Start()
    {
        textmeshPro = equationTextObject.GetComponent<TextMeshPro>();
    }

    /*
        Updates the text on every frame update
    */
    void Update()
    {
        if(operation != null)
        textmeshPro.SetText(operation.operationEquationText);
        else
        {
            print("ZERO");
        }
    }
}
