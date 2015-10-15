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
    #endregion

    private PanelsManager panels;
    private RuntimeManager runtime;

    private const string textRestart = "You really want restart level?";
    private const string textQuit = "You really want quit?";

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
    }

    public void RuntimeCloseClick()
    {
        panels.ShowShadow();
        PauseRequest();
        panels.ShowMessage(textQuit);
    }
}
