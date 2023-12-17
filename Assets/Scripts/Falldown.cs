using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falldown : MonoBehaviour
{
    // 定義物件掉落的速度
    public float speed = 7f;
    // 定義物件的tag類別
    private string tmpTag;

    // 定義ClassManagement的實例
    private ClassManagement classManagement;
    // 在遊戲開始時執行
    void Start()
    {
        // 獲取物件的tag
        tmpTag = gameObject.tag;

        // 獲取ClassManagement的實例
        classManagement = FindObjectOfType<ClassManagement>();
    }
    // 在每一幀更新時執行
    void Update()
    {
        // 物件沿y軸向下移動
        transform.position += Vector3.down * speed * Time.deltaTime;
        // 如果物件的y座標小於-20
        if (transform.position.y < -20)
        {
            // 銷毀物件
            Destroy(gameObject);

            // 將物件的tag類別儲存到ClassManagement中
            classManagement.SaveTag(tmpTag);
        }
    }
}

