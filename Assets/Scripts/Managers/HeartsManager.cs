using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class HeartsManager
{
    public void ChangeCount(int count)
    {
        GameObject[] hearts = ApplicationManager.gui.HeartPanel.GetComponentsInChildren<Image>(true).Select(x => x.gameObject).ToArray();
        int countHeart = 0;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i].activeSelf) countHeart++;
        }

        if (count < countHeart)
        {
            for (int i = count; i < countHeart; i++)
            {
                hearts[i].SetActive(false);
            }
        }
        else if (count == countHeart) return;
        for (int i = countHeart; i < count; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }
}
