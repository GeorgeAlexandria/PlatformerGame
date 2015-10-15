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
    //public static readonly IGuiManager guiManager;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
