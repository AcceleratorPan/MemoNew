using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Color buttonNormalColor = Color.white;
    public Color buttonHighlightedColor = Color.yellow;
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public Text victoryScoreText;
    public Text defeatScoreText;
    public Text inGameScoreText;
    public Image pauseButtonImage;
    public Image musicMuteButtonImage;
    public Image sfxMuteButtonImage;
    private bool isPaused = false;
    void Start()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
        UpdateInGameScore(0);
        UpdateMuteButtonUI();
    }

    public void OnClick_TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // 暂停游戏
            Time.timeScale = 0f;
            // 高亮按钮
            pauseButtonImage.color = buttonHighlightedColor;
        }
        else
        {
            // 恢复游戏
            Time.timeScale = 1f;
            // 恢复按钮颜色
            pauseButtonImage.color = buttonNormalColor;
        }
    }

    public void OnClick_ToggleMusicMute()
    {
        AudioManager.Instance.ToggleMusic();
        UpdateMuteButtonUI();
    }

    /// <summary>
    /// 切换音效静音
    /// </summary>
    public void OnClick_ToggleSfxMute()
    {
        AudioManager.Instance.ToggleSfx();
        UpdateMuteButtonUI();
    }

    private void UpdateMuteButtonUI()
    {
        if (AudioManager.Instance.IsMusicMuted())
        {
            musicMuteButtonImage.color = buttonHighlightedColor;
        }
        else
        {
            musicMuteButtonImage.color = buttonNormalColor;
        }

        if (AudioManager.Instance.IsSfxMuted())
        {
            sfxMuteButtonImage.color = buttonHighlightedColor;
        }
        else
        {
            sfxMuteButtonImage.color = buttonNormalColor;
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
