using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // コントローラーの任意のボタンが押されたかをチェック
        if (Input.anyKeyDown)
        {
            // ゲームシーンの名前を指定してシーンをロードする
            SceneManager.LoadScene("GameScene");
        }
    }
}
