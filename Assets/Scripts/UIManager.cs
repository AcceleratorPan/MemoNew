using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject pausePanel;
    public Text victoryScoreText;
    public Text defeatScoreText;
    public Text inGameScoreText;
    public Text pauseScoreText;
    public Image pauseButtonImage;
    public Sprite pauseNormalSprite;
    public Sprite pauseHighlightedSprite;

    public Image musicMuteButtonImage;
    public Sprite musicNormalSprite;
    public Sprite musicMutedSprite;

    public Image sfxMuteButtonImage;
    public Sprite sfxNormalSprite;
    public Sprite sfxMutedSprite;

    void Start()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
        pausePanel.SetActive(false);
        UpdateInGameScore(0);
        UpdateMuteButtonUI();
    }

    public void ShowPausePanel(int currentScore)
    {
        pauseScoreText.text = "Score: " + currentScore;
        pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void OnClick_TogglePause()
    {
        if (GameManager.Instance.isGameOver)
        {
            return;
        }
        pauseButtonImage.sprite = pauseHighlightedSprite;
        GameManager.Instance.PauseGame();
    }

    public void OnClick_ResumeButton()
    {
        pauseButtonImage.sprite = pauseNormalSprite;
        GameManager.Instance.ResumeGame();
    }


    public void OnClick_ToggleMusicMute()
    {
        AudioManager.Instance.ToggleMusic();
        UpdateMuteButtonUI();
    }

    public void OnClick_ToggleSfxMute()
    {
        AudioManager.Instance.ToggleSfx();
        UpdateMuteButtonUI();
    }

    private void UpdateMuteButtonUI()
    {
        if (AudioManager.Instance.IsMusicMuted())
        {
            musicMuteButtonImage.sprite = musicMutedSprite;
        }
        else
        {
            musicMuteButtonImage.sprite = musicNormalSprite;
        }

        if (AudioManager.Instance.IsSfxMuted())
        {
            sfxMuteButtonImage.sprite = sfxMutedSprite;
        }
        else
        {
            sfxMuteButtonImage.sprite = sfxNormalSprite;
        }
    }


    public void UpdateInGameScore(int score)
    {
        if (inGameScoreText != null)
        {
            inGameScoreText.text = "Score: " + score;
        }
    }

    public void ShowVictoryPanel(int finalScore)
    {
        defeatPanel.SetActive(false);
        victoryScoreText.text = "Score: " + finalScore.ToString();
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowDefeatPanel(int finalScore)
    {
        victoryPanel.SetActive(false);
        defeatScoreText.text = "Score: " + finalScore.ToString();
        defeatPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void OnClick_Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClick_ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
