using UnityEngine;
using System.Collections;
using System;

public delegate void RestartRequestEventHandler();

public delegate void LoadRequestEventHandler();

public interface IRestarterLevel
{
    void RestartLevel();

    event RestartRequestEventHandler RestartRequest;

    event LoadRequestEventHandler LoadRequest;

    void LoadLevel(int number);
}
