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
        Image[] hearts = ApplicationManager.gui.heartPanel.GetComponentsInChildren<Image>();
        int countHeart = 0;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (!hearts[i].isActiveAndEnabled)
            {
                countHeart = i - 1 > 0 ? i - 1 : 0;
                break;
            }
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
                GameObject temp = new GameObject();
                temp.AddComponent<Image>();
                //temp.AddComponent<CanvasRenderer>();
                //temp.AddComponent<RectTransform>();
                temp.GetComponent<Image>().sprite = heart;
                RectTransform rect = temp.GetComponent<RectTransform>();
                rect.SetParent(ApplicationManager.gui.heartPanel.transform);
                rect.sizeDelta *= scaleHeart;
                rect.position = new Vector2(position.x + (i + hearts.Length) * height, position.y);
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -rect.anchoredPosition.y);
            }
        }
    }
}
