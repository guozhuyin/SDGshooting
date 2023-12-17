using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class StartManager : MonoBehaviour
{
    // 定義遊戲場景的名稱
    public string gameSceneName = "GameScene";

    // 定義角色選擇的按鈕
    public Button[] characterButtons;

    // 定義開始遊戲的按鈕
    public Button startButton;

    // 定義顯示角色分數的文字
    public TextMeshProUGUI scoreText;

    // 定義選擇的角色的索引
    private int selectedCharacter = -1;

    // 定義各角色的分數的字典
    public Dictionary<int, int> characterScores = new Dictionary<int, int>();

    // 在遊戲開始時執行
    void Start()
    {
        // 為每個角色按鈕添加點擊事件
        for (int i = 0; i < characterButtons.Length; i++)
        {
            // 獲取按鈕的索引
            int index = i;

            // 添加點擊事件，調用SelectCharacter方法
            characterButtons[i].onClick.AddListener(() => SelectCharacter(index));
        }

        // 為開始按鈕添加點擊事件，調用StartGame方法
        startButton.onClick.AddListener(StartGame);

        // 從PlayerPrefs中讀取各角色的分數
        LoadScores();

        // 顯示各角色的分數
        ShowScores();
    }

    // 定義一個方法，用於選擇角色
    private void SelectCharacter(int index)
    {
        // 如果選擇的角色與當前選擇的角色不同
        if (index != selectedCharacter)
        {
            // 更新選擇的角色的索引
            selectedCharacter = index;

            // 為選擇的角色按鈕添加高亮效果
            HighlightButton(characterButtons[index], true);

            // 為其他角色按鈕移除高亮效果
            for (int i = 0; i < characterButtons.Length; i++)
            {
                if (i != index)
                {
                    HighlightButton(characterButtons[i], false);
                }
            }

            // 啟用開始按鈕
            startButton.interactable = true;
        }
    }

    // 定義一個方法，用於為按鈕添加或移除高亮效果
    private void HighlightButton(Button button, bool highlight)
    {
        // 獲取按鈕的圖片組件
        Image image = button.GetComponent<Image>();

        // 如果要添加高亮效果
        if (highlight)
        {
            // 將按鈕的圖片顏色設置為白色
            image.color = Color.white;
        }
        else
        {
            // 否則，將按鈕的圖片顏色設置為灰色
            image.color = Color.gray;
        }
    }

    // 定義一個方法，用於開始遊戲
    private void StartGame()
    {
        // 如果選擇了角色
        if (selectedCharacter != -1)
        {
            // 將選擇的角色的索引儲存到PlayerPrefs中
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);

            // 載入遊戲場景
            SceneManager.LoadScene(gameSceneName);
        }
    }

    // 定義一個方法，用於從PlayerPrefs中讀取各角色的分數
    private void LoadScores()
    {
        // 為每個角色讀取分數
        for (int i = 0; i < characterButtons.Length; i++)
        {
            // 定義一個鍵，用於儲存分數
            string key = "CharacterScore" + i;

            // 如果PlayerPrefs中存在該鍵
            if (PlayerPrefs.HasKey(key))
            {
                // 從PlayerPrefs中讀取分數
                int score = PlayerPrefs.GetInt(key);

                // 將分數加入字典中
                characterScores.Add(i, score);
            }
            else
            {
                // 否則，將分數設置為零
                characterScores.Add(i, 0);
            }
        }
    }

    // 定義一個方法，用於顯示各角色的分數
    private void ShowScores()
    {
        // 定義一個字串，用於儲存分數
        string scoreString = "";

        // 為每個角色添加分數
        for (int i = 0; i < characterButtons.Length; i++)
        {
            // 添加角色名稱和分數
            scoreString += "Character " + (i + 1) + ": " + characterScores[i] + "\n";
        }

        // 顯示分數
        scoreText.text = scoreString;
    }
}