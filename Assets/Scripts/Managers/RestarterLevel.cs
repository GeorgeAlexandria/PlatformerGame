using UnityEngine;
using System.Collections;
using System;

public class RestarterLevel : IRestarterLevel
{
    public void LoadLevel(int number)
    {
        Application.LoadLevel(number);
        //Invoke event
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        //Invoke event
    }
}
