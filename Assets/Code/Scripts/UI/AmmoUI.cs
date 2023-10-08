using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    private Image image;

    public Sprite FilledSprite;
    public Sprite EmptySprite;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Fill()
    {
        image.sprite = FilledSprite;
    }

    public void Empty()
    {
        image.sprite = EmptySprite;
    }
}