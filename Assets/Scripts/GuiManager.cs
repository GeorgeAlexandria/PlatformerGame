using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GuiManager : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject menuPanel;
    public GameObject runtimePanel;
    public GameObject messagePanel;
    public GameObject shadowPanel;
    public GameObject heartPanel;

    public float ScaleHeart;
    public Sprite Heart;
    public Vector2 Position;

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

    public delegate void LoadRequestEventHandler();
    public event LoadRequestEventHandler LoadRequest;

    public event LoadRequestEventHandler LoadMenuRequest;
    #endregion

    private PanelsManager panels;
    private RuntimeManager runtime;
    private HeartsManager hearts;

    private enum StateMessage
    {
        None,
        Restart,
        Quit,
        Die,
        Finish
    }

    private const string textRestart = "You really want to restart level?";
    private const string textQuit = "You really want to quit?";
    private const string textFinish = "Congratulations! You passed this level!\nDo you want to continue?";
    private const string textDie = "Unfortunately, you died... \nDo you want to restart game?";
    private StateMessage state;

    Dictionary<StateMessage, Action> YesFunctions;

    Dictionary<StateMessage, Action> NoFunctions;

    public GuiManager()
    {
        #region YesFunction
        YesFunctions = new Dictionary<StateMessage, Action> {
        { StateMessage.Die,()=> { } },
        { StateMessage.Finish,()=>
        {
            RuntimeHideMessageShadow();
            LoadRequest();
            PlayRequest();
            RuntimeShowPause();
        }},
        { StateMessage.None,()=> { } },
        { StateMessage.Quit,()=>
        {
            QuitRequest();
        }},
        { StateMessage.Restart, ()=>
        {
            RuntimeHideMessageShadow();
            RestartRequest();
            PlayRequest();
            RuntimeShowPause();
        }}
    };
        #endregion

        #region NoFunctions
        NoFunctions = new Dictionary<StateMessage, Action> {
        { StateMessage.Die,()=> { NoFunctions[StateMessage.None](); } },
        { StateMessage.Finish,()=>
        {
            RuntimeHideMessageShadow();
            RuntimeShowPause();
            PlayRequest();
            panels.HideRuntime();
            panels.HideHearts();
            LoadMenuRequest();
        } },
        { StateMessage.None,()=>
        {
            RuntimeHideMessageShadow();
            PlayRequest();
            RuntimeShowPause();
        } },
        { StateMessage.Quit,()=> { NoFunctions[StateMessage.None](); } },
        { StateMessage.Restart, ()=> { NoFunctions[StateMessage.None](); } }
    };
        #endregion
    }

    void Awake()
    {
        panels = new PanelsManager(optionPanel, menuPanel, runtimePanel, messagePanel, shadowPanel, heartPanel);
        runtime = new RuntimeManager(runtimePanel.GetComponentsInChildren<Button>(true));
        hearts = new HeartsManager(Heart, Position, ScaleHeart);
    }

    public void StartClick()
    {
        panels.HideMenu();
        StartRequest();
        panels.ShowRuntime();
        panels.ShowHearts();
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
        state = StateMessage.Restart;
    }

    public void RuntimeCloseClick()
    {
        panels.ShowShadow();
        PauseRequest();
        panels.ShowMessage(textQuit);
        state = StateMessage.Quit;
    }

    public void FinishLevelClick()
    {
        panels.ShowShadow();
        PauseRequest();
        panels.ShowMessage(textFinish);
        state = StateMessage.Finish;
    }

    public void RuntimeYesClick()
    {
        YesFunctions[state].Invoke();
    }

    public void RuntimeNoClick()
    {
        NoFunctions[state].Invoke();
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

    public void DrawHearts(int count)
    {
        hearts.ChangeCount(count);
    }
}
