using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public void PauseLevel()
    {
        Time.timeScale = 0;
    }

    public void PlayLevel()
    {
        Time.timeScale = 1;
    }

    public int NextLevel()
    {
        return Application.loadedLevel == 5 ? 1 : Application.loadedLevel + 1;
    }

    public bool IsLastLevel()
    {
        return Application.loadedLevel == 5;
    }

    public void LoadLevel()
    {
        Application.LoadLevel(NextLevel());
    }

    public void LoadLevel(int number)
    {
        Application.LoadLevel(number);
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
