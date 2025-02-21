using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    //�^�C�}�[���J�E���g���邽�߂̕ϐ�
    float countTimer = 60.0f;
    //�v���C���[�^�O���w��
    public string targetTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //countTimer�ɁA�Q�[�����J�n���Ă���̕b�����i�[
        countTimer -= Time.deltaTime;
        //���݂̕b����\��
        GetComponent<Text>().text = countTimer.ToString("F2");
        //�v���C���[�^�O���擾
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTag);
        //�A�N�e�B�u�ȃv���C���[���J�E���g����ϐ�
        int activeCount = 0;
        //�S�Ă�RigitBody���擾
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        
        //���ɑS�Ĕ�\�����Ƃ���
        //bool allInActive = true;
        foreach (GameObject obj in targetObjects)
        {
            if (obj.activeInHierarchy) // �A�N�e�B�u���ǂ������m�F
            {
                activeCount++;
            }
        }

        // �A�N�e�B�u�ȃI�u�W�F�N�g��1�ɂȂ�����Q�[�����I��
        if (activeCount == 1 || countTimer == 0.0f)
        {
            //Debug.Log("�A�N�e�B�u�ȃv���C���[��1�l�ɂȂ�܂����B�Q�[�����I�����܂��B");
            
            Time.timeScale = 0f;

            GetComponent<Text>().text = countTimer.ToString("You Survived!!");

            //RigitBody�����I�u�W�F�N�g�����ׂĒ�~������
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = true;
            }

            // �R���g���[���[�̔C�ӂ̃{�^���������ꂽ�����`�F�b�N
            if (Input.anyKeyDown)
            {
                // �Q�[���V�[���̖��O���w�肵�ăV�[�������[�h����
                SceneManager.LoadScene("Title");
            }

        }
    }
       
}
