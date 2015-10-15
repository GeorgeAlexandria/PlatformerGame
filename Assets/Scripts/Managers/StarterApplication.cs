using UnityEngine;
using System.Collections;
using System;

public class StarterApplication : IStarterApplication
{
    public int sceneToStart = 1;

    public void StartApplication()
    {
        //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
        //Invoke("LoadDelayed", fadeColorAnimationClip.length * .5f);
        Application.LoadLevel(sceneToStart);
    }
}
