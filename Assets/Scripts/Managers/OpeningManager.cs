using UnityEngine;
using System.Collections;
using System.Linq;

public class OpeningManager
{

    private readonly Animator animator;

    private const string load = "load";
    private const string reset = "reset";

    public OpeningManager(GameObject OpeningPanel)
    {
        animator = OpeningPanel.GetComponent<Animator>();
    }

    public void ShowImage(string arg)
    {

        animator.ResetTrigger(reset);
        animator.SetTrigger(load);
    }

    public void HideImage()
    {
        animator.ResetTrigger(load);
        animator.SetTrigger(reset);
    }

    public float GetShowLength()
    {
        return animator.runtimeAnimatorController.animationClips.First(x => x.name == "Show").length;
    }
}
