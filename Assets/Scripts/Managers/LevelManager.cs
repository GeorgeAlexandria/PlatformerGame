﻿using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public void PauseLevel()
    {
        Time.timeScale = 0;
    }

    public void PlayLevel()
    {
        Time.timeScale = 1;
    }

    public int NextLevel
    {
        get
        {
            return Application.loadedLevel + 1;
        }
    }

    public void LoadLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void LoadLevel(int number)
    {
        Application.LoadLevel(number);
        //Application.LoadLevelAsync(number);
        //LoadRequest();
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        //RestartRequest();
    }

}
