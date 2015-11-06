using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelsManager
{
    private readonly GameObject optionPanel;
    private readonly GameObject menuPanel;
    private readonly GameObject runtimePanel;
    private readonly GameObject messagePanel;
    private readonly GameObject shadowPanel;
    private readonly GameObject heartPanel;
    private readonly GameObject loadPanel;


    public PanelsManager(GameObject optionPanel, GameObject menuPanel,
        GameObject runtimePanel, GameObject messagePanel,
        GameObject shadowPanel, GameObject heartPanel,
        GameObject loadPanel)
    {
        this.optionPanel = optionPanel;
        this.menuPanel = menuPanel;
        this.runtimePanel = runtimePanel;
        this.messagePanel = messagePanel;
        this.shadowPanel = shadowPanel;
        this.heartPanel = heartPanel;
        this.loadPanel = loadPanel;
    }

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

    public void ShowShadow()
    {
        shadowPanel.SetActive(true);
    }

    public void HideShadow()
    {
        shadowPanel.SetActive(false);
    }

    public void ShowHearts()
    {
        heartPanel.SetActive(true);
    }

    public void HideHearts()
    {
        heartPanel.SetActive(false);
    }

    public void ShowLoad()
    {
        loadPanel.SetActive(true);
    }

    public void HideLoad()
    {
        loadPanel.SetActive(false);
    }
}
