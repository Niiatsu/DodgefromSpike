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
        // �R���g���[���[�̔C�ӂ̃{�^���������ꂽ�����`�F�b�N
        if (Input.anyKeyDown)
        {
            // �Q�[���V�[���̖��O���w�肵�ăV�[�������[�h����
            SceneManager.LoadScene("GameScene");
        }
    }
}
