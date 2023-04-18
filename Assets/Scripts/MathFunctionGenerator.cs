using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

/*
    This class is used in order to generate random mathematical operations 
    which will be shown to the player on the possible ways on the gate objects. 
*/
public class MathFunctionGenerator
{
    public string operationEquationText;
    public int operationResult;
    public string opComplexity; // Complexity of operation
    public string operationType; // Holds operation type such as addition, multiplication
    private int operationValue; // The value which will be used in the operation
    private readonly int maxSUBandSUM = 10; // Max possible sum and sub value
    private readonly int maxDIVandMUL = 4; // Max divisible and multiply value
    private bool manual = false; // States if it is manually generated operation

    /*
        Constructor of the class. Creates a random math oepration with random complexity
    */
    public MathFunctionGenerator()
    {
        manual = false;
        ChooseOperationComplexity(); // Chooses complexity of operation
        createMathematicalFunction();
    }
    /*
        Constructor with two parameters which allows set complexity and operation manually
        @param manuelComplex "Basic" or "Complex" operation
        @param manuelOp "add","sub","mul","div" math operations
    */
    public MathFunctionGenerator(string manuelComplex,string manuelOp)
    {
        manual = true;
        this.opComplexity = manuelComplex;
        this.operationType = manuelOp;
        createMathematicalFunction();
    }

    /*
        Creates a new mathematical operation according to the current complexity
    */
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
        {
             if(manual == false)
            {
            ChooseBasicOperation();
            }
            CreateBasicOperation();
        }
        
    }

    /*
        Operations can be basic or complex. Function chooses one of them randomly
    */
    private void ChooseOperationComplexity()
    {
        string [] opComplexityTypes = {"Basic","Complex"};
        int temp = UnityEngine.Random.Range(0,opComplexityTypes.Length);
        this.opComplexity = opComplexityTypes[temp];
    }

    /*
        Creates a basic operation by using the chosen operation type either randomly or manually
    */
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
            operationEquationText = "x / "  + this.operationValue;   
        }

    }

    /*
        Chooses a basic oepration randomly
    */
    private void ChooseBasicOperation()
    {
        string [] mathOps = {"add","sub","mul","div","add"};
        int temp = UnityEngine.Random.Range(0,mathOps.Length);
        string op = mathOps[temp];
        this.operationType = op;
    }
    /*
        Calculates the operation result depending on the operation type and it's value
    */
    public int getResult()
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
            this.operationResult = Gamemanager.GameManagerInstance.Army.Count * (operationValue -1);
        }
        else if (string.Equals(operationType,"div"))
        {
            this.operationResult = Gamemanager.GameManagerInstance.Army.Count-Convert.ToInt32(Gamemanager.GameManagerInstance.Army.Count / (operationValue));
        }
        return operationResult;
    }

    /*
        Complex operation generator, it is under construction
    */
    private void CreateComplexOperation()
    {
        CreateBasicOperation();
    }
}