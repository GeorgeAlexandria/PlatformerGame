using UnityEngine;
//using UnityScript.Lang;
//using UnityScript;
//using UnityEngine.Scripting;
//using UnityEngine.Events;
//using UnityEditor;
using System;

using System.Collections;

public class ApplicationManager : MonoBehaviour
{
    #region Delegate
    public delegate void RequestEventHandler();

    public delegate U RequestEventHandler<U>();

    public delegate void ParametrizedRequestEventHandler<T>(T arg);

    public delegate U ParametrizedRequestEventHandler<T, U>(T arg);
    #endregion

    public static GuiManager gui { get; private set; }

    public static HeroScript hero { get; private set; }

    public static LevelManager level { get; private set; }

    public int sceneToStart = 1;

    void Awake()
    {
        //Find only active objects
        gui = FindObjectOfType<GuiManager>();
        hero = FindObjectOfType<HeroScript>();
        level = FindObjectOfType<LevelManager>();

        hero.gameObject.SetActive(false);

        //Find all objects(active, inactive, prefab, assets)
        //var h = Resources.FindObjectsOfTypeAll<HeroScript>();

        gui.StartRequest += StartApplication;
        gui.QuitRequest += QuitApplication;
        gui.PauseRequest += level.PauseLevel;
        gui.PlayRequest += level.PlayLevel;
        gui.RestartRequest += level.RestartLevel;
        gui.LoadRequest += level.LoadLevel;
        gui.DiedRequest += () => level.LoadLevel(1);
        gui.LoadMenuRequest += () =>
        {
            Destroy(FindObjectOfType<CameraScript>());
            Destroy(gui.gameObject);
            Destroy(hero.gameObject);
            Destroy(this.gameObject);
            level.LoadLevel(0);
        };
        gui.LevelRequest += level.NextLevel;
        gui.LastLevelRequest += level.IsLastLevel;

        hero.ChangeCountHeartsRequest += gui.DrawHearts;
        hero.FinishLevel += gui.FinishLevelClick;
        hero.AwakeRequest += level.PauseLevel;
        hero.LoadRequest += level.PlayLevel;
        hero.DiedRequest += gui.DieClick;
    }

    public void StartApplication()
    {
        hero.gameObject.SetActive(true);
        Application.LoadLevel(sceneToStart);
    }

    public void QuitApplication()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
