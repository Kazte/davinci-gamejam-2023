using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Garbage : MonoBehaviour
{
    public Sprite[] Sprites;

    public SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        SpriteRenderer.sprite = Sprites[Random.Range(0, Sprites.Length)];
    }
}