using UnityEngine;
using System.Collections;
using System;

public class ContinuerLevel : IContinuerLevel
{
    public void PlayLevel()
    {
        Time.timeScale = 1;
    }
}