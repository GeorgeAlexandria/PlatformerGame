using UnityEngine;
using System.Collections;
using System;

public class BreakerLevel : IBreakerLevel
{
    public void PauseLevel()
    {
        Time.timeScale = 0;
    }
}
