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

        if (currentScore > lastHighscore)
        {
            // New highscore
            highScoreText.SetText(
                $"Has logrado salvar a la ciudad en {FormatTime(currentScore)}\nES UN NUEVO RECORD!!");
        }
        else
        {
            highScoreText.SetText(
                $"Has salvado a la ciudad en {FormatTime(currentScore)}\nTu mejor tiempo fue {FormatTime(lastHighscore)}");
        }
    }

    public string FormatTime(float time)
    {
        var seconds = Mathf.FloorToInt(time % 60);
        var minutes = Mathf.FloorToInt(time / 60);

        return $"{minutes:00}:{seconds:00}";
    }
}