using UnityEngine;
using System.Collections;

public interface IStarterApplication
{
    event LoadRequestEventHandler LoadRequest;
    void StartApplication();
}
