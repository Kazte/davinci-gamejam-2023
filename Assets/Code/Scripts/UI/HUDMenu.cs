using System;
using System.Collections;
using UnityEditor;
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
    public Button ControlsContinueButton;


    public GameObject creditsContainer;

    public GameObject controlsContainer;

    private void Awake()
    {
        AudioManager.Instance.Play("Menu_Music");
        StartButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Button_Sound");

            StartCoroutine(ChangeScene(() =>
            {
                controlsContainer.SetActive(true);

                return true;
            }));
        });
        
        ControlsContinueButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Button_Sound");
            
            StartCoroutine(ChangeScene(() =>
            {
                SceneManager.LoadScene(sceneBuildIndex: 1);
            
                return true;
            }));
        });

        

        CreditsButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Button_Sound");
            creditsContainer.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
        });

        CreditsBackButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Button_Sound");
            creditsContainer.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        });

        ExitButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Button_Sound");

            StartCoroutine(ChangeScene(() =>
            {
                Application.Quit();

                return true;
            }));
        });
    }

    private IEnumerator ChangeScene(Func<bool> callback)
    {
        yield return new WaitForSeconds(0.4f);

        callback();
    }
}