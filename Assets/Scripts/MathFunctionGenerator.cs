using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
public class MathFunctionGenerator
{
    public string operationEquationText;
    public int operationResult;
    public string opComplexity;
    public string operationType;
    private int operationValue;
    private readonly int maxSUBandSUM = 10;
    private readonly int maxDIVandMUL = 4;
    private bool manual = false;
    public MathFunctionGenerator()
    {
        manual = false;
        ChooseOperationComplexity();
        createMathematicalFunction();
    }

    public MathFunctionGenerator(string manuelComplex,string manuelOp)
    {
        manual = true;
        this.opComplexity = manuelComplex;
        this.operationType = manuelOp;
        createMathematicalFunction();
    }
    public void createMathematicalFunction()
    {
        if(opComplexity == "Basic")
        {
            if(manual == false)
            {
            ChooseBasicOperation();
            }
            CreateBasicOperation();
        }
        else if (opComplexity == "Complex")
        {//UNDER COSTRUCTION
             if(manual == false)
            {
            ChooseBasicOperation();
            }
            CreateBasicOperation();
        }
       // Debug.Log(opComplexity);
        
    }
    private void ChooseOperationComplexity()
    {
        string [] opComplexityTypes = {"Basic","Complex"};
        int temp = UnityEngine.Random.Range(0,opComplexityTypes.Length);
        this.opComplexity = opComplexityTypes[temp];
    }

    private void CreateBasicOperation()
    {
        if(string.Equals(operationType,"add"))
        {
            this.operationValue = UnityEngine.Random.Range(1,maxSUBandSUM);
            operationEquationText = "x + "  + this.operationValue;
        } 
        else if (string.Equals(operationType,"sub"))
        {
            this.operationValue = UnityEngine.Random.Range(1,maxSUBandSUM);
            operationEquationText = "x - "  + this.operationValue;
        }
        else if (string.Equals(operationType,"mul"))
        {
            this.operationValue = UnityEngine.Random.Range(2,maxDIVandMUL);
            operationEquationText = "x * "  + this.operationValue;            
        }
        else if (string.Equals(operationType,"div"))
        {
            this.operationValue = UnityEngine.Random.Range(2,maxDIVandMUL);
            //this.operationValue = Gamemanager.GameManagerInstance.Balls.Count-Convert.ToInt32(Gamemanager.GameManagerInstance.Balls.Count / (operationCoefficient));
            operationEquationText = "x / "  + this.operationValue;   
        }
       // Debug.Log(operationType);
        //Debug.LogWarning(operationEquationText);
    }
    private void ChooseBasicOperation()
    {
        string [] mathOps = {"add","sub","mul","div","add"};
        int temp = UnityEngine.Random.Range(0,mathOps.Length);
        string op = mathOps[temp];
        this.operationType = op;
    }
    public int getResult()//eklencek çıkcak miktarı döner
    {
        if(string.Equals(operationType,"add"))
        {
            this.operationResult = operationValue;
        } 
        else if (string.Equals(operationType,"sub"))
        {
            this.operationResult = operationValue;
        }
        else if (string.Equals(operationType,"mul"))
        {
            this.operationResult = Gamemanager.GameManagerInstance.Balls.Count * (operationValue -1);
        }
        else if (string.Equals(operationType,"div"))
        {
            this.operationResult = Gamemanager.GameManagerInstance.Balls.Count-Convert.ToInt32(Gamemanager.GameManagerInstance.Balls.Count / (operationValue));
        }
        return operationResult;
    }
    private void CreateComplexOperation()
    {
        CreateBasicOperation();//şimdilik
    }
}





    // private void CreateBasicOperation()
    // {
    //     string [] mathOps = {"add","sub","mul","div"};
    //     int temp = UnityEngine.Random.Range(0,mathOps.Length);
    //     string op = mathOps[temp];

    //     if(string.Equals(op,"add"))
    //     {
    //         this.operationResult = UnityEngine.Random.Range(0,maxSUBandSUM);
    //         operationEquationText = "x + "  + operationResult;
    //     } 
    //     else if (string.Equals(op,"sub"))
    //     {
    //         this.operationResult = UnityEngine.Random.Range(0,maxSUBandSUM);
    //         operationEquationText = "x - "  + operationResult;
    //     }
    //     else if (string.Equals(op,"mul"))
    //     {
    //         int operationCoefficient = UnityEngine.Random.Range(1,maxDIVandMUL);
    //         Debug.Log(Gamemanager.GameManagerInstance.Balls.Count);
    //         this.operationResult = Gamemanager.GameManagerInstance.Balls.Count * (operationCoefficient -1);
    //         operationEquationText = "x * "  + operationCoefficient;            
    //     }
    //     else if (string.Equals(op,"div"))
    //     {
    //         int operationCoefficient = UnityEngine.Random.Range(1,maxDIVandMUL);
    //         this.operationResult = Convert.ToInt32(Gamemanager.GameManagerInstance.Balls.Count / (operationCoefficient));
    //         operationEquationText = "x / "  + operationCoefficient;   
    //     }
    //     Debug.Log(op);
    // }