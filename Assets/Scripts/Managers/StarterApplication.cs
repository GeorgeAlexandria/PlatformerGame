using UnityEngine;
using System.Collections;
using System;

public class StarterApplication : IStarterApplication
{
    public int sceneToStart = 1;

    public event LoadRequestEventHandler LoadRequest;

    public void StartApplication()
    {
        //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
        //Invoke("LoadDelayed", fadeColorAnimationClip.length * .5f);
        Application.LoadLevelAsync(sceneToStart);
        //ApplicationManager.hero.Load();
        //LoadRequest();
    }
}
