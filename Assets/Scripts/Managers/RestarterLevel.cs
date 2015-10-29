using UnityEngine;
using System.Collections;
using System;

public class RestarterLevel : IRestarterLevel
{
    public event LoadRequestEventHandler LoadRequest;
    public event RestartRequestEventHandler RestartRequest;

    public void LoadLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void LoadLevel(int number)
    {
        Application.LoadLevelAsync(number);
        //LoadRequest();
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        //RestartRequest();
    }
}
