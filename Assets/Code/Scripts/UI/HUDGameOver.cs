using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDGameOver : MonoBehaviour
{
    public Button RestartButton;

    public Button QuitButton;


    private void Awake()
    {
        RestartButton.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });

        QuitButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }
}