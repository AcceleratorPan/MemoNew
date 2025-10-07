using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager uiManager;

    private int enemyCount;
    private int score = 0;
    private bool isGameOver = false;

    void Start()
    {
        Instance = this;
        SceneManager.LoadSceneAsync(2,LoadSceneMode.Additive);
    }

    public void RegisterEnemy()
    {
        enemyCount++;
        Debug.Log("一个敌人已登记，当前敌人总数: " + enemyCount);
    }

    public void EnemyDestroyed(int addedScore)
    {
        if (isGameOver) return;

        enemyCount--;
        score += addedScore;
        uiManager.UpdateInGameScore(score);
        Debug.Log("一个敌人被消灭，剩余敌人: " + enemyCount);

        if (enemyCount <= 0)
        {
            isGameOver = true;
            Debug.Log("胜利！");
            uiManager.ShowVictoryPanel(score);
        }
    }

    public void PlayerDied()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("失败！");
        uiManager.ShowDefeatPanel(score);
    }
}
