using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HUDMenu : MonoBehaviour
{
    public Button StartButton;
    public Button CreditsButton;
    public Button CreditsBackButton;
    public Button ExitButton;


    public GameObject creditsContainer;

    private void Awake()
    {
        StartButton.onClick.AddListener(() => { SceneManager.LoadScene(sceneBuildIndex: 1); });

        CreditsButton.onClick.AddListener(() =>
        {
            creditsContainer.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
        });

        CreditsBackButton.onClick.AddListener(() =>
        {
            creditsContainer.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        });

        ExitButton.onClick.AddListener(() => { Application.Quit(); });
    }
}