using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDWin : MonoBehaviour
{
    public Button RestartButton;

    public Button QuitButton;

    public TMP_Text highScoreText;


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


    private void OnEnable()
    {
        var lastHighscore = PlayerPrefs.GetFloat("highscore", float.PositiveInfinity);
        var currentScore = GameManager.Instance.GetScore();

        if (currentScore < lastHighscore)
        {
            // New highscore
            highScoreText.SetText(
                $"Has logrado salvar a la ciudad en <color=#4EA64E>{FormatTime(currentScore)}</color>\n<color=#4EA64E>ES UN NUEVO RECORD!!</color>");

            PlayerPrefs.SetFloat("highscore", currentScore);
        }
        else
        {
            highScoreText.SetText(
                $"Tiempo Actual: <color=#4EA64E>{FormatTime(currentScore)}</color>\nMejor tiempo: <color=#4EA64E>{FormatTime(lastHighscore)}</color>");
        }
    }

    public string FormatTime(float time)
    {
        var seconds = Mathf.FloorToInt(time % 60);
        var minutes = Mathf.FloorToInt(time / 60);

        return $"{minutes:00}:{seconds:00}";
    }

    private IEnumerator ChangeScene(Func<bool> callback)
    {
        yield return new WaitForSeconds(0.4f);

        callback();
    }
}