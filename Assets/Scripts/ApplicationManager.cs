using UnityEngine;
//using UnityScript.Lang;
//using UnityScript;
//using UnityEngine.Scripting;
//using UnityEngine.Events;
//using UnityEditor;


using System.Collections;

public class ApplicationManager : MonoBehaviour
{
    public static readonly IRestarterLevel restarter = new RestarterLevel();
    public static readonly IFinalizerApplication finalizer = new FinalizerApplication();
    public static readonly IStarterApplication starter = new StarterApplication();
    public static readonly IContinuerLevel continuer = new ContinuerLevel();
    public static readonly IBreakerLevel breaker = new BreakerLevel();
    public static GuiManager gui { get; private set; }
    //public static readonly IGuiManager guiManager;

    void Awake()
    {
        gui = FindObjectOfType<GuiManager>();

        gui.StartRequest += starter.StartApplication;
        gui.QuitRequest += finalizer.QuitApplication;
        gui.PauseRequest += breaker.PauseLevel;
        gui.PlayRequest += continuer.PlayLevel;
    }
}
