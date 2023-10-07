using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Fill()
    {
        image.enabled = true;
    }

    public void Empty()
    {
        image.enabled = false;
    }
}