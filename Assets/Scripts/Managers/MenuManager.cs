using UnityEngine;
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
        buttons.Select(x => x.enabled = true);
    }

    public void DisableButtons()
    {
        buttons.Select(x => x.enabled = false);
    }

    public void FadeMenu()
    {
        animator.SetTrigger(fade);
    }
}
