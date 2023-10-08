using System;
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
        RestartButton.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });

        QuitButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }


    private void OnEnable()
    {
        var lastHighscore = PlayerPrefs.GetFloat("highscore", 0);
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
                $"Has salvado a la ciudad en <color=#4EA64E>{FormatTime(currentScore)}</color>\nTu mejor tiempo fue <color=#4EA64E>{FormatTime(lastHighscore)}</color>");
        }
    }

    public string FormatTime(float time)
    {
        var seconds = Mathf.FloorToInt(time % 60);
        var minutes = Mathf.FloorToInt(time / 60);

        return $"{minutes:00}:{seconds:00}";
    }
}