using UnityEngine;
using System.Collections;

public class StartAplicationScript : MonoBehaviour
{
    public int sceneToStart = 1;

    public void StartClick()
    {
        //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
        //Invoke("LoadDelayed", fadeColorAnimationClip.length * .5f);
        Application.LoadLevel(sceneToStart);
    }
}
