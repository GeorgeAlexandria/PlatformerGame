using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class Utilities
{
    public static Coroutine StartCoroutine(this MonoBehaviour behavior, float time, Action func)
    {
        if (Time.timeScale == 0)
            return behavior.StartCoroutine(DelayFunction(behavior, time, func));
        return behavior.StartCoroutine(DelayFunction(time, func));
    }

    private static IEnumerator DelayFunction(float time, Action func)
    {
        yield return new WaitForSeconds(time);
        func();
    }

    private static IEnumerator DelayFunction(this MonoBehaviour behavior, float time, Action func)
    {
        yield return behavior.StartCoroutine(WaitForRealTime(time));
        func();
    }

    private static IEnumerator WaitForRealTime(float delay)
    {
        while (true)
        {
            float pauseEndTime = Time.realtimeSinceStartup + delay;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return null;
            }
            break;
        }
    }
}
