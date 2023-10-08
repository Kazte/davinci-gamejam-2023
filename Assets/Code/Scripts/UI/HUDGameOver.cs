using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDGameOver : MonoBehaviour
{
    public Button RestartButton;

    public Button QuitButton;

    private void Awake()
    {
        RestartButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Button_Sound");
            StartCoroutine(ChangeScene(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return true;
            }));
        });

        QuitButton.onClick.AddListener(
            () =>
            {
                AudioManager.Instance.Play("Button_Sound");
                StartCoroutine(ChangeScene(() =>
                {
                    SceneManager.LoadScene(0);
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