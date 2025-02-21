using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    //タイマーをカウントするための変数
    float countTimer = 60.0f;
    //プレイヤータグを指定
    public string targetTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //countTimerに、ゲームを開始してからの秒数を格納
        countTimer -= Time.deltaTime;
        //現在の秒数を表示
        GetComponent<Text>().text = countTimer.ToString("F2");
        //プレイヤータグを取得
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTag);
        //アクティブなプレイヤーをカウントする変数
        int activeCount = 0;
        //全てのRigitBodyを取得
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        
        //仮に全て非表示だとする
        //bool allInActive = true;
        foreach (GameObject obj in targetObjects)
        {
            if (obj.activeInHierarchy) // アクティブかどうかを確認
            {
                activeCount++;
            }
        }

        // アクティブなオブジェクトが1つになったらゲームを終了
        if (activeCount == 1 || countTimer == 0.0f)
        {
            //Debug.Log("アクティブなプレイヤーが1人になりました。ゲームを終了します。");
            
            Time.timeScale = 0f;

            GetComponent<Text>().text = countTimer.ToString("You Survived!!");

            //RigitBodyを持つオブジェクトをすべて停止させる
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = true;
            }

            // コントローラーの任意のボタンが押されたかをチェック
            if (Input.anyKeyDown)
            {
                // ゲームシーンの名前を指定してシーンをロードする
                SceneManager.LoadScene("Title");
            }

        }
    }
       
}
