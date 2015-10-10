using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManagerPanelsScript : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject menuPanel;
    public GameObject runtimePanel;
    public GameObject messagePanel;
    //private GameObject optionShadow;

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
    }

    public void ShowOption()
    {
        optionPanel.SetActive(true);
    }

    public void HideOption()
    {
        optionPanel.SetActive(false);
    }

    public void ShowRuntime()
    {
        runtimePanel.SetActive(true);
    }

    public void HideRuntime()
    {
        runtimePanel.SetActive(false);
    }

    public void ShowMessage(string arg)
    {
        messagePanel.SetActive(true);
        messagePanel.GetComponentInChildren<Text>().text = arg;
    }

    public void HideMessage()
    {
        messagePanel.SetActive(false);
    }
}
