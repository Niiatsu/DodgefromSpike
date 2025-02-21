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
        PlayNormalBGM(); // �Q�[���J�n���ɒʏ�BGM���Đ�
    }

    void Update()
    {
        // �A�N�e�B�u�ȃv���C���[�����`�F�b�N
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
        // "Player"�^�O���t���Ă���I�u�W�F�N�g�����ׂĎ擾
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // �A�N�e�B�u�ȃv���C���[�̐����J�E���g
        int activePlayerCount = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                activePlayerCount++;
            }
        }

        // �A�N�e�B�u�ȃv���C���[��1�l�����̏ꍇ�ɏ���BGM���Đ�
        if (activePlayerCount == 1 && !isVictoryTriggered)
        {
            PlayVictoryBGM();
            isVictoryTriggered = true;
        }

        // �v���C���[���S�Ĕ�\���̏ꍇ�ɔs�kBGM���Đ�
        if (activePlayerCount == 0 && !isDefeatTriggered)
        {
            PlayDefeatBGM();
            isDefeatTriggered = true;
        }
    }
}
