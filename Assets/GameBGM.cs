using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource normalBGM;
    public AudioSource victoryBGM;
    public AudioSource defeatBGM;

    private bool isVictoryTriggered = false;
    private bool isDefeatTriggered = false;

    void Start()
    {
        PlayNormalBGM(); // ゲーム開始時に通常BGMを再生
    }

    void Update()
    {
        // アクティブなプレイヤー数をチェック
        CheckActivePlayerCount();
    }

    public void PlayNormalBGM()
    {
        StopAllBGM();
        normalBGM.Play();
    }

    public void PlayVictoryBGM()
    {
        StopAllBGM();
        victoryBGM.Play();
    }

    public void PlayDefeatBGM()
    {
        StopAllBGM();
        defeatBGM.Play();
    }

    private void StopAllBGM()
    {
        normalBGM.Stop();
        victoryBGM.Stop();
        defeatBGM.Stop();
    }

    private void CheckActivePlayerCount()
    {
        // "Player"タグが付いているオブジェクトをすべて取得
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // アクティブなプレイヤーの数をカウント
        int activePlayerCount = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                activePlayerCount++;
            }
        }

        // アクティブなプレイヤーが1人だけの場合に勝利BGMを再生
        if (activePlayerCount == 1 && !isVictoryTriggered)
        {
            PlayVictoryBGM();
            isVictoryTriggered = true;
        }

        // プレイヤーが全て非表示の場合に敗北BGMを再生
        if (activePlayerCount == 0 && !isDefeatTriggered)
        {
            PlayDefeatBGM();
            isDefeatTriggered = true;
        }
    }
}
