using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class RuntimeManager
{
    private readonly Component play;
    private readonly Component pause;
    private readonly Component restart;
    private readonly Component close;

    public RuntimeManager(Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            switch (buttons[i].name)
            {
                case "Play":
                    this.play = buttons[i];
                    break;
                case "Pause":
                    this.pause = buttons[i];
                    break;
                case "Restart":
                    this.restart = buttons[i];
                    break;
                default:
                    this.close = buttons[i];
                    break;
            }
        }
    }

    public void ShowPlay()
    {
        play.gameObject.SetActive(true);
    }

    public void HidePlay()
    {
        play.gameObject.SetActive(false);
    }

    public void ShowPause()
    {
        pause.gameObject.SetActive(true);
    }

    public void HidePause()
    {
        pause.gameObject.SetActive(false);
    }
}
