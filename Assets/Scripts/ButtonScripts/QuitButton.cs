using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitTheGame()
    {
        Debug.LogWarning("oyundan cikildi");
        Application.Quit();
    }

}
