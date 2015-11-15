using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartsManager
{
    private readonly float scaleHeart;
    private readonly Sprite heart;
    private readonly Vector2 position;

    public HeartsManager(Sprite heart, Vector2 position, float scaleHeart)
    {
        this.heart = heart;
        this.position = position;
        this.scaleHeart = scaleHeart;
    }

    public void ChangeCount(int count)
    {
        Image[] hearts = ApplicationManager.gui.heartPanel.GetComponentsInChildren<Image>(true);
        int countHeart = 0;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i].isActiveAndEnabled) countHeart++;
        }

        if (count < countHeart)
        {
            for (int i = count; i < countHeart; i++)
            {
                hearts[i].enabled = false;
            }
        }
        else if (count == countHeart) return;
        else if (count <= hearts.Length)
        {
            for (int i = countHeart; i < count; i++)
            {
                hearts[i].enabled = true;
            }
        }
        else
        {
            int length = count - hearts.Length;
            float height = heart.texture.height * scaleHeart;
            float width = heart.texture.width * scaleHeart;
            for (int i = 0; i < length; i++)
            {
                GameObject temp = new GameObject(string.Format("HeartHero{0}", i));
                temp.AddComponent<Image>();
                temp.GetComponent<Image>().sprite = heart;
                RectTransform rect = temp.GetComponent<RectTransform>();
                rect.SetParent(ApplicationManager.gui.heartPanel.transform);
                rect.sizeDelta *= scaleHeart;
                rect.position = new Vector2(position.x + (i + hearts.Length) * width, position.y);
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -rect.anchoredPosition.y);
            }
        }
    }
}
