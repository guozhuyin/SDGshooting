using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    // 定義一個陣列，用於儲存預製物件
    public GameObject[] prefabs;

    // 定義一個變數，用於儲存要出現的預製物件的數量
    public int numberOfPrefabs = 3;

    // 定義一個變數，用於儲存隨機秒數的最小值
    public float minTime = 3f;

    // 定義一個變數，用於儲存隨機秒數的最大值
    public float maxTime = 8f;

    // 定義一個變數，用於儲存x座標的最小值
    public float minX = -30f;

    // 定義一個變數，用於儲存x座標的最大值
    public float maxX = 30f;

    // 定義一個變數，用於儲存y座標的值
    private float y = 20f;

    // 定義一個變數，用於儲存z座標的值
    public float z = 25f;

    // public GameObject resultPanel;
    public ClassManagement classManagement;
    bool isGameOver;

    // 在遊戲開始時執行
    void Start()
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        classManagement = FindObjectOfType<ClassManagement>();

        // 創建一個位置向量
        Vector3 position = new Vector3(0, y, z);

        // 創建一個旋轉四元數
        Quaternion rotation = Quaternion.identity;
        // 隨機選擇一個預製物件
        Instantiate(prefab, position, rotation);
        
        // 開始調用SpawnPrefab方法
        StartCoroutine(SpawnPrefab());
    }
    void Update()
    {
        isGameOver = classManagement.isGameOver;
    }

    // 定義一個協程，用於隨機秒數出現預製物件
    IEnumerator SpawnPrefab()
    {
        // 無限循環
        while (isGameOver != true)
        {
            // 等待隨機秒數
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            // 隨機選擇要生成的預製物件的數量
            int count = Random.Range(1, 5);

            // 定義一個變數，用於儲存上一個預製物件的x座標
            float lastX = 0f;

            // 使用一個迴圈，生成指定的數量的預製物件
            for (int i = 0; i < count; i++)
            {
                // 隨機選擇一個預製物件
                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

                // 隨機選擇一個x座標，並確保與上一個預製物件的x座標至少相差1
                float x = Random.Range(minX, maxX);
                while (Mathf.Abs(x - lastX) < 2.5f)
                {
                    x = Random.Range(minX, maxX);
                }

                // 創建一個位置向量
                Vector3 position = new Vector3(x, y, z);

                // 創建一個旋轉四元數
                Quaternion rotation = Quaternion.identity;

                // 在指定的位置和旋轉實例化預製物件
                Instantiate(prefab, position, rotation);

                // 更新上一個預製物件的x座標
                lastX = x;
            }
        }
    }
}
