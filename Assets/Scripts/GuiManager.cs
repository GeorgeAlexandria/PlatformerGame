using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject menuPanel;
    public GameObject runtimePanel;
    public GameObject messagePanel;
    public GameObject shadowPanel;

    #region Events
    public delegate void QuitRequestEventHandler();
    public event QuitRequestEventHandler QuitRequest;

    public delegate void StartRequestEventHandler();
    public event StartRequestEventHandler StartRequest;

    public delegate void PauseRequestEventHandler();
    public event PauseRequestEventHandler PauseRequest;

    public delegate void PlayRequestEventHandler();
    public event PlayRequestEventHandler PlayRequest;

    public delegate void RestartRequestEventhandler();
    public event RestartRequestEventHandler RestartRequest;
    #endregion

    private PanelsManager panels;
    private RuntimeManager runtime;

    private const string textRestart = "You really want restart level?";
    private const string textQuit = "You really want quit?";
    private bool isQuitMessage;

    void Awake()
    {
        panels = new PanelsManager(optionPanel, menuPanel, runtimePanel, messagePanel, shadowPanel);
        runtime = new RuntimeManager(runtimePanel.GetComponentsInChildren<Button>(true));
    }

    public void StartClick()
    {
        panels.HideMenu();
        StartRequest();
        panels.ShowRuntime();
    }

    public void OptionClick()
    {
        panels.HideMenu();
        panels.ShowOption();
    }

    public void QuitClick()
    {
        QuitRequest();
    }

    public void BackClick()
    {
        panels.HideOption();
        panels.ShowMenu();
    }

    public void RuntimePauseClick()
    {
        panels.ShowShadow();
        PauseRequest();
        runtime.HidePause();
        runtime.ShowPlay();
    }

    public void RuntimePlayClick()
    {
        panels.HideShadow();
        PlayRequest();
        runtime.HidePlay();
        runtime.ShowPause();
    }

    public void RuntimeRestartClick()
    {
        panels.ShowShadow();
        PauseRequest();
        panels.ShowMessage(textRestart);
        isQuitMessage = false;
    }

    public void RuntimeCloseClick()
    {
        panels.ShowShadow();
        PauseRequest();
        panels.ShowMessage(textQuit);
        isQuitMessage = true;
    }

    public void RuntimeYesClick()
    {
        if (isQuitMessage)
        {
            QuitRequest();
            return;
        }
        RuntimeHideMessageShadow();
        RestartRequest();
        PlayRequest();
        RuntimeShowPause();
    }

    public void RuntimeNoClick()
    {
        RuntimeHideMessageShadow();
        PlayRequest();
        RuntimeShowPause();
    }

    private void RuntimeHideMessageShadow()
    {
        panels.HideMessage();
        panels.HideShadow();
    }

    private void RuntimeShowPause()
    {
        runtime.HidePlay();
        runtime.ShowPause();
    }
}
