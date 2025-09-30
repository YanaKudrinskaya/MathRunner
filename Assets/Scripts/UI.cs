using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _levelText, _scoreText, _messageText;
    [SerializeField] private GameObject _gamePanel, _menuPanel;
    [SerializeField] private Animator _uiPlayerAnimator;
    [SerializeField] private Button _button;


    private void Start()
    {
        UpdateScore();
        UpdateLevel();
    }
    public void UpdateScore()
    {
        _scoreText.text = Stats.Score.ToString("D4"); 
    }

    public void UpdateLevel()
    {
        _levelText.text = Stats.Level.ToString();
    }

  
    public void UpdateLoose(String str) 
    {
        Stats.Attempts--;
        if (Stats.Attempts == 0) 
        {
            _messageText.text = str;
            Stats.Attempts = 3;
            Stats.ResetAllStats();
        }
        else if(Stats.Attempts == 1) _messageText.text = str + " У тебя осталась последняя попытка";
        else _messageText.text = str + " У тебя осталось две попытки";
        ClosePanel(_gamePanel);
        OpenPanel(_menuPanel);
        _uiPlayerAnimator.SetTrigger("No");
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Victory()
    {
        _messageText.text = "Отлично! Ты выиграл!";
        ClosePanel(_gamePanel);
        OpenPanel(_menuPanel);
        _uiPlayerAnimator.SetTrigger("Victory");
        _button.enabled = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        Stats.Life = 3;
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
