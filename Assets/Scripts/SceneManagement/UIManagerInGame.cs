using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManagerInGame : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gameUI;
    public PlayerController player;

    public Button exitButton;
    public Button playAgainButton;

    void Awake()
    {
        ShowGameUI();
        player = FindObjectOfType<PlayerController>();
        player.OnPlayerDeath += ShowGameOverText;
        gameOverPanel.SetActive(false);
        exitButton.onClick.AddListener(SceneLoader.instance.LoadMenuScene);
        exitButton.onClick.AddListener(MusicManager.instance.FadeOut);
        playAgainButton.onClick.AddListener(SceneLoader.instance.ReloadCurrentScene);
    }

    void ShowGameOverText()
    {
        gameUI.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    void ShowGameUI()
    {
        gameUI.SetActive(true);
        gameOverPanel.SetActive(false);
    }
}
