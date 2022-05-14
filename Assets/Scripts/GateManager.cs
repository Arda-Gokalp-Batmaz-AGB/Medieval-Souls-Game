using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class GateManager : MonoBehaviour
{
   [SerializeField] public GameObject Gate_1;
   [SerializeField] public GameObject Gate_2;
   public MathFunctionGenerator operation_1;
   public MathFunctionGenerator operation_2;
   [SerializeField] public bool manuelCreation;
   
    void Start()
    {//konumunu belki ayarla
        //Kapıların textini ayarla, result tagına sahip objenin ismini sonuç olarak yap onun altındaki textde işlemi göstersin

        //Debug.LogWarning(manuelCreation);
        InitiliaseGates();
        // GateData Gate_1_data = Gate_1.GetComponent<GateData>();
        // GateData Gate_2_data = Gate_2.GetComponent<GateData>();      
        // if(manuelCreation == false)
        // {
        //     operation_1 = new MathFunctionGenerator();
        //     operation_2 = new MathFunctionGenerator();
        // }    
        // else
        // {
        //     operation_1 = new MathFunctionGenerator("Basic","add");
        //     operation_2 = new MathFunctionGenerator("Basic","add");
        // }

        // Gate_1_data.operation = operation_1;
        // Gate_2_data.operation = operation_2;
        //Debug.LogWarning(Gate_1_data.operation.operationEquationText);
        //Debug.LogWarning(Gate_2_data.operation.operationEquationText);
        
        // var compTemp_2 = Gate_2.GetComponent<MathFunctionGenerator>();
        // compTemp_2 = operation_2;
        // GameObject result_2 = Gate_2.transform.GetChild(0).gameObject;
        // GameObject equality_2 = result_2.transform.GetChild(0).gameObject;
        // TextMeshPro textmeshPro_2 = equality_2.GetComponent<TextMeshPro>();
        // textmeshPro_2.SetText(operation_2.operationEquationText);

        // Gate_1.transform.GetChild(0).name = Convert.ToString(operation_1.operationResult);
        // Gate_2.transform.GetChild(0).name = Convert.ToString(operation_2.operationResult);
    }

    // Update is called once per frame
    void Update()
    {
        if(Gate_1.GetComponent<Collider>().enabled == false || Gate_2.GetComponent<Collider>().enabled == false)
        {
            print("Gate serie deactivated");
            gameObject.SetActive(false);
            //Gate_1.SetActive(false);
            //Gate_2.SetActive(false);
        }
    }

    public void InitiliaseGates()
    {
        GateData Gate_1_data = Gate_1.GetComponent<GateData>();
        GateData Gate_2_data = Gate_2.GetComponent<GateData>();   
       // Debug.LogWarning(manuelCreation);   
        if(manuelCreation == false)
        {
            operation_1 = new MathFunctionGenerator();
            operation_2 = new MathFunctionGenerator();
        }    
        else
        {
            operation_1 = new MathFunctionGenerator("Basic","add");
            operation_2 = new MathFunctionGenerator("Basic","add");
        }

        Gate_1_data.operation = operation_1;
        Gate_2_data.operation = operation_2;
    }

    // public void ManualInitiliase(String type,String operation)
    // {
    //     GateData Gate_1_data = Gate_1.GetComponent<GateData>();
    //     GateData Gate_2_data = Gate_2.GetComponent<GateData>();  
    //     operation_1 = new MathFunctionGenerator(type,operation);
    //     operation_2 = new MathFunctionGenerator(type,operation);
    //     Gate_1_data.operation = operation_1;
    //     Gate_2_data.operation = operation_2;
    //     Debug.LogWarning(manuelCreation);
    // }

    // public void SwitchManuality()
    // {
    //     if(manuelCreation == true)
    //         manuelCreation = false;
    //     if(manuelCreation == false)
    //         manuelCreation = true;
    // }
}



    //    operation_1 = new MathFunctionGenerator();
    //     operation_2 = new MathFunctionGenerator();
    //     print(manuelCreation);

    //     GateData Gate_1_data = Gate_1.GetComponent<GateData>();
    //     GateData Gate_2_data = Gate_2.GetComponent<GateData>();           


    //     Gate_1_data.operation = operation_1;
    //     Gate_2_data.operation = operation_2;