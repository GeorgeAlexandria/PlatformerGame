﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class MenuManager
{
    private readonly Animator animator;

    private const string fade = "fade";

    private readonly Button[] buttons;

    public MenuManager(GameObject menuPanel)
    {
        animator = menuPanel.GetComponent<Animator>();
        buttons = menuPanel.GetComponentsInChildren<Button>();
    }

    public void EnableButtons()
    {
        buttons.ToList().ForEach(x => x.enabled = true);
    }

    public void DisableButtons()
    {
        buttons.ToList().ForEach(x => x.enabled = false);
    }

    public void FadeMenu()
    {
        animator.SetTrigger(fade);
    }

    public float GetFadeLength()
    {
        return animator.runtimeAnimatorController.animationClips.First(x => x.name == "Fade").length;
    }
}
