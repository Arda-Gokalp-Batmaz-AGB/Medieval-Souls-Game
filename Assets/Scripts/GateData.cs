using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class GateData : MonoBehaviour
{
    public MathFunctionGenerator operation;
    [SerializeField] public GameObject Gate;
    [SerializeField] public GameObject resultObject;
    [SerializeField] public GameObject equationTextObject;
    //public GateData data;
    public TextMeshPro textmeshPro;
    void Start()
    {
       //operation = null;
        textmeshPro = equationTextObject.GetComponent<TextMeshPro>();
        //data = Gate.GetComponent<GateData>();
        //operation = gameObject.AddComponent<MathFunctionGenerator>();
        //GameObject result_1 = Gate_1.transform.GetChild(0).gameObject;
        //GameObject equality_1 = result_1.transform.GetChild(0).gameObject;
        //TextMeshPro textmeshPro_1 = equationTextObject.GetComponent<TextMeshPro>();        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogWarning(operation);
        if(operation != null)
        textmeshPro.SetText(operation.operationEquationText);
        else
        {
            print("ZERO");
        }
    }
}
