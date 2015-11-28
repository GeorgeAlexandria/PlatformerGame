﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GuiManager : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject MenuPanel;
    public GameObject RuntimePanel;
    public GameObject MessagePanel;
    public GameObject ShadowPanel;
    public GameObject HeartPanel;
    public GameObject LoadPanel;
    public GameObject OpeningPanel;

    public float ScaleHeart;
    public Sprite Heart;
    public Vector2 Position;

    #region Events
    public event ApplicationManager.RequestEventHandler QuitRequest;

    public event ApplicationManager.RequestEventHandler StartRequest;

    public event ApplicationManager.RequestEventHandler PauseRequest;

    public event ApplicationManager.RequestEventHandler PlayRequest;

    public event ApplicationManager.RequestEventHandler RestartRequest;

    public event ApplicationManager.RequestEventHandler LoadRequest;

    public event ApplicationManager.RequestEventHandler LoadMenuRequest;

    public event ApplicationManager.RequestEventHandler DiedRequest;

    public event ApplicationManager.RequestEventHandler<int> LevelRequest;

    public event ApplicationManager.RequestEventHandler<bool> LastLevelRequest;
    #endregion

    private PanelsManager panels;
    private RuntimeManager runtime;
    private HeartsManager hearts;
    private LoadManager load;
    private MenuManager menu;
    private OpeningManager opening;

    private Image background;

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
    private const string textFinish = "Congratulations!\nYou passed this level!\nDo you want to continue?";
    private const string textPass = "Congratulations!\nYou passed game!\nDo you want to restart game?";
    private const string textDie = "Unfortunately, you died... \nDo you want to restart game?";
    private const string textLevel = "Level ";
    private StateMessage state;

    Dictionary<StateMessage, Action> YesFunctions;

    Dictionary<StateMessage, Action> NoFunctions;

    public GuiManager()
    {
        #region YesFunction
        YesFunctions = new Dictionary<StateMessage, Action> {
        { StateMessage.Die,()=>
        {
            RuntimeHideMessageShadow();
            RuntimeShowPause();
            PlayRequest();
            DiedRequest();
        } },
        { StateMessage.Finish,()=>
        {
             RuntimeHideMessageShadow();
             panels.HideHearts();
             panels.HideRuntime();

             load.ShowImage(textLevel + LevelRequest());
             this.StartCoroutine(1f, load.HideImage);

             this.StartCoroutine(load.GetLoadLength(), () =>
             {
                 LoadRequest();
                 PlayRequest();
                 this.StartCoroutine(0.2f, () =>
                 {
                     panels.ShowHearts();
                     panels.ShowRuntime();
                 });
                 RuntimeShowPause();
             });
        }},
        { StateMessage.None,()=> { } },
        { StateMessage.Quit,()=>
        {
            RuntimeHideMessageShadow();
            RuntimeShowPause();
            PlayRequest();
            panels.HideRuntime();
            panels.HideHearts();
            LoadMenuRequest();
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
        { StateMessage.Die,()=> {NoFunctions[StateMessage.Finish]();  } },
        { StateMessage.Finish,()=>
        {
            YesFunctions[StateMessage.Quit]();
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
        panels = new PanelsManager(OptionPanel, MenuPanel, RuntimePanel, MessagePanel, ShadowPanel, HeartPanel, LoadPanel, OpeningPanel);
        runtime = new RuntimeManager(RuntimePanel.GetComponentsInChildren<Button>(true));
        hearts = new HeartsManager(Heart, Position, ScaleHeart);
        load = new LoadManager(LoadPanel);
        menu = new MenuManager(MenuPanel);
        opening = new OpeningManager(OpeningPanel);

        background = gameObject.GetComponent<Image>();
    }

    public void StartClick()
    {
        menu.DisableButtons();
        menu.FadeMenu();
        this.StartCoroutine(menu.GetFadeLength(), () =>
        {
            load.ShowImage(textLevel + LevelRequest());
            this.StartCoroutine(1f, load.HideImage);
            this.StartCoroutine(load.GetLoadLength(), () =>
            {
                background.enabled = false;
                panels.HideMenu();
                StartRequest();
                this.StartCoroutine(0.2f, () =>
                {
                    panels.ShowHearts();
                    panels.ShowRuntime();
                    PlayRequest();
                });
            });
        });
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
        string text = LastLevelRequest() ? textPass : textFinish;
        panels.ShowMessage(text);
        state = StateMessage.Finish;
    }

    public void DieClick()
    {
        panels.ShowShadow();
        PauseRequest();
        panels.ShowMessage(textDie);
        state = StateMessage.Die;
    }

    public void RuntimeYesClick()
    {
        YesFunctions[state]();
    }

    public void RuntimeNoClick()
    {
        NoFunctions[state]();
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

    public void OpeningClick()
    {
        panels.ShowOpening();
        this.StartCoroutine(opening.GetShowLength(), panels.HideOpening);
    }
}
