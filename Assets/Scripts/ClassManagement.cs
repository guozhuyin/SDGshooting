using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 引入場景管理的命名空間
using TMPro;

public class ClassManagement : MonoBehaviour
{
    // 定義按鍵與物件類別的對應關係
    private Dictionary<KeyCode, string> keyToClass = new Dictionary<KeyCode, string>()
    {
        {KeyCode.A, "Trash"},
        {KeyCode.S, "Plastic"},
        {KeyCode.D, "Paper"},
        // {KeyCode.None, ""}
    };
    

    // 定義錯誤的tag名稱與次數的字典
    private Dictionary<string, int> wrongTags = new Dictionary<string, int>();
    // 定義各類別的分數的字典
    private Dictionary<string, int> classScores = new Dictionary<string, int>();

    // 定義分數
    private int score = 0;
    // 定義遊戲時間
    public float time;
    // 定義遊戲是否結束的標誌
    public bool isGameOver = false;
    private int selectedCharacter;
    // 定義錯誤音效
    public AudioClip errorSound;
    private AudioSource audioSource;
    public GameObject resultPanel; // 顯示錯誤次數的TextMeshPro物件
    public Texture2D centerCursor;

    // 定義TextMeshPro物件
    public TextMeshProUGUI trashScore; // 顯示trash分數的TextMeshPro物件
    public TextMeshProUGUI plasticScore; // 顯示plastic分數的TextMeshPro物件
    public TextMeshProUGUI paperScore; // 顯示paper分數的TextMeshPro物件
    public TextMeshProUGUI timeText; // 顯示時間的TextMeshPro物件
    public TextMeshProUGUI wrongScore; // 顯示錯誤次數的TextMeshPro物件

    // 初始化
    void Start()
    {
        // 獲取音效組件
        audioSource = GetComponent<AudioSource>();
        // 從PlayerPrefs中讀取選擇的角色的索引
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");        Cursor.SetCursor(centerCursor, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        SaveScore();
        // 如果遊戲沒有結束
        if (!isGameOver)
        {
            // 遊戲時間減少
            time -= Time.deltaTime;

            // 如果遊戲時間小於零
            if (time < 0)
            {
                // 遊戲結束
                isGameOver = true;

                // 顯示遊戲結果
                ShowResults();
            }
            else
            {
                // 否則，顯示遊戲時間
                timeText.text = "Time: " + Mathf.Round(time) + "s";
                // 如果按下了某個按鍵
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    // 獲取滑鼠準心下的物件
                    GameObject target = GetObjectUnderCursor();
                    // 如果沒有物件
                    if (target == null || target.CompareTag("Untagged"))
                    {
                        // 播放錯誤音效
                        audioSource.PlayOneShot(errorSound);
                    }
                    else
                    {
                        // 獲取物件的tag
                        string tmptag = target.tag;
                        // 獲取按下的按鍵
                        KeyCode key = GetPressedKey();
                        // 如果按鍵與物件類別匹配
                        if (keyToClass[key] == tmptag && keyToClass[key] != "")
                        {
                            // 分數加一
                            score++;
                            // 銷毀物件
                            Destroy(target);
                            // 更新TextMeshPro物件的文字
                            UpdateTextMeshPro(tmptag);
                        }
                        else
                        {
                            // 播放錯誤音效
                            audioSource.PlayOneShot(errorSound);
                            if (tmptag != "Untagged")
                            {
                                // 如果錯誤的tag已經存在字典中
                                if (wrongTags.ContainsKey(tmptag))
                                {
                                    // 錯誤次數加一
                                    wrongTags[tmptag]++;
                                    // 銷毀物件
                                    Destroy(target);
                                }
                                else
                                {
                                    // 否則，將錯誤的tag加入字典，並設置錯誤次數為一
                                    wrongTags.Add(tmptag, 1);
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            // 如果遊戲結束，顯示遊戲結果
            ShowResults();
        }
    }

    // 獲取滑鼠準心下的物件
    GameObject GetObjectUnderCursor()
    {
        // 定義一個射線，從攝像機發射到滑鼠位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 定義一個射線碰撞信息
        RaycastHit hit;
        // 如果射線碰到了物件
        if (Physics.Raycast(ray, out hit))
        {
            // 返回碰撞的物件
            return hit.collider.gameObject;
        }
        else
        {
            // 否則，返回空
            return null;
        }
    }

    // 獲取按下的按鍵
    KeyCode GetPressedKey()
    {
        // 遍歷所有的按鍵
        foreach (KeyCode key in keyToClass.Keys)
        {
            // 如果按鍵被按下
            if (Input.GetKeyDown(key))
            {
                // 返回按鍵
                return key;
            }
        }
        // 如果沒有按鍵被按下，返回空
        return KeyCode.None;
    }

    // 更新TextMeshPro物件的文字
    void UpdateTextMeshPro(string tmptag)
    {
        // 根據tag選擇對應的TextMeshPro物件
        TextMeshProUGUI tmp = null;
        switch (tmptag)
        {
            case "Trash":
                tmp = trashScore;
                break;
            case "Plastic":
                tmp = plasticScore;
                break;
            case "Paper":
                tmp = paperScore;
                break;
        }
        // 如果找到了TextMeshPro物件
        if (tmp != null)
        {
            // 獲取原來的文字
            string text = tmp.text;

            // 將文字中的數字加一
            int num = int.Parse(text) + 1;

            // 將文字更新為新的數字
            tmp.text = num.ToString();
        }
    }
    // 定義一個方法，用於將錯誤的tag儲存到字典中
    public void SaveTag(string tmptag)
    {
        if (isGameOver != true)
        {
            // 如果錯誤的tag已經存在字典中
            if (wrongTags.ContainsKey(tmptag))
            {
                // 錯誤次數加一
                wrongTags[tmptag]++;
            }
            else
            {
                // 否則，將錯誤的tag加入字典，並設置錯誤次數為一
                wrongTags.Add(tmptag, 1);
            }
        }
    }
    // 定義一個方法，用於顯示遊戲結果
    private void ShowResults()
    {
        resultPanel.SetActive(true);
        // 定義一個字串，用於儲存結果
        string result = "";

        // 加入分數
        result += "<b>Score </b>" + "\n" + score + "\n";

        // 加入錯誤次數
        result += "<b>Wrong Scores </b>\n";
        foreach (var pair in wrongTags)
        {
            // result += pair.Key + ": " + pair.Value + "\n";
            if (pair.Key == "Trash")
            {
                result += "<color=#FF0000>Trash </color>" + pair.Value + "\n";
            }
            else if (pair.Key == "Plastic")
            {
                result += "<color=#002060>Plastic </color>" + pair.Value + "\n";
            }
            else if (pair.Key == "Paper")
            {
                result += "<color=#4472C4>Paper </color>" + pair.Value + "\n";
            }
        }

        // 顯示結果
        wrongScore.text = result;
        // Invoke("LoadScene", 10);
    }
    // 定義一個方法，用於將分數儲存到PlayerPrefs中
    private void SaveScore()
    {
        // 定義一個鍵，用於儲存分數
        string key = "CharacterScore" + selectedCharacter;

        // 如果PlayerPrefs中存在該鍵
        if (PlayerPrefs.HasKey(key))
        {
            // 從PlayerPrefs中讀取分數
            int previousScore = PlayerPrefs.GetInt(key);

            // 如果當前分數大於之前的分數
            if (score > previousScore)
            {
                // 將當前分數儲存到PlayerPrefs中
                PlayerPrefs.SetInt(key, score);
            }
        }
        else
        {
            // 否則，將當前分數儲存到PlayerPrefs中
            PlayerPrefs.SetInt(key, score);
        }
    }
    // private void LoadScene()
    // {
    //     SceneManager.LoadScene("StartScene");
    // }
}
