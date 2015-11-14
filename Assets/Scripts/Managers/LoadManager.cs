using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class LoadManager
{
    private readonly Animator animator;
    private readonly Text text;

    private const string load = "load";
    private const string reset = "reset";

    public LoadManager(GameObject loadPanel)
    {
        animator = loadPanel.GetComponent<Animator>();
        text = loadPanel.GetComponentInChildren<Text>();
    }

    public void ShowImage(string arg)
    {

        animator.ResetTrigger(reset);
        animator.SetTrigger(load);
        text.text = arg;
    }

    public void HideImage()
    {
        animator.ResetTrigger(load);
        animator.SetTrigger(reset);
    }

    public float GetLoadLength()
    {
        return animator.runtimeAnimatorController.animationClips.First(x => x.name == "Load").length;
    }
}
