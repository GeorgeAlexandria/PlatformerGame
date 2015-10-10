using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour
{
    public IRestarterLevel restarter;
    public IFinalizerApplication finalizer;
    public IStarterApplication starter;
    public IContinuerApplication continuer;
    public IBreakerApplication breaker;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
