using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject leaderboardPanel;
    public GameObject tutorialPanel;
    public GameObject recyclePanel;
    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void GameScene()
    {
        // 載入遊戲場景
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        // 退出遊戲
        Application.Quit();
    }
    public void Restart()
    {
        // 重新載入當前場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitRecycle()
    {
        recyclePanel.SetActive(false);
    }
    public void MenuPanel()
    {
        startPanel.SetActive(true);
        tutorialPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        recyclePanel.SetActive(false);
    }
    public void Leaderboard()
    {
        startPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        recyclePanel.SetActive(false);
    }
    public void Tutorial()
    {
        startPanel.SetActive(false);
        tutorialPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
        recyclePanel.SetActive(false);
    }
    public void Recycle()
    {
        startPanel.SetActive(false);
        tutorialPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
        recyclePanel.SetActive(true);
    }
}