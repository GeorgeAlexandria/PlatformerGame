using UnityEngine;
using System.Collections;
using System.Linq;

public class OpeningManager
{
    private readonly Animator animator;

    public OpeningManager(GameObject OpeningPanel)
    {
        animator = OpeningPanel.GetComponent<Animator>();
    }

    public float GetOpeningLength()
    {
        return animator.runtimeAnimatorController.animationClips.First(x => x.name == "Show").length;
    }
}