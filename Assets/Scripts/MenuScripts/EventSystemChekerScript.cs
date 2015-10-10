using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EventSystemChekerScript : MonoBehaviour
{
    void OnLevelWasLoaded()
    {
        if (!FindObjectOfType<EventSystem>())
        {
            GameObject obj = new GameObject("EventSystem");
            obj.AddComponent<EventSystem>();
            obj.AddComponent<StandaloneInputModule>();
            obj.AddComponent<TouchInputModule>();
        }
    }
}
