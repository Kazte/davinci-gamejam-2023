using System;
using DG.Tweening;
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

    private void OnDisable()
    {
        transform.DOKill();
    }

    public void Fill()
    {
        image.sprite = FilledSprite;
    }

    public void Empty()
    {
        image.sprite = EmptySprite;
    }

    private bool playingFillEffect;

    public void PlayFillEffect()
    {
        if (!playingFillEffect)
        {
            playingFillEffect = true;
            transform.DOPunchScale(Vector3.one * 0.5f, 0.25f).OnComplete(() => { playingFillEffect = false; });
        }
    }
}