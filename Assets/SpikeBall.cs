using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SpikeBall : MonoBehaviour
{
    //Rigitbody
    private Rigidbody rb;
    //�v���C���[4�l��Tranform���i�[
    public Transform[] players;
    public AudioClip Sound1;
    AudioSource AS;

    //�X�P�[�����傫���Ȃ鑬�x
    public Vector3 ScaleChange = new Vector3(0.025f, 0.025f, 0.025f);
    //�{�[������������
    public float Force = 1000.0f;
    //����������ύX���鎞�ԊԊu
    public float ChangeTime = 2.0f;
    //�����_���ȕ���
    private Vector3 randamDirection;
    //�^�C�}�[
    private float Timer = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        //Rigitbody�̎擾
        rb = GetComponent<Rigidbody>();
        //AudioSource�R���|�[�l���g�̎擾
        AS = GetComponent<AudioSource>();
        //�R���[�`�����J�n
        StartCoroutine(MoveToWardsRandomPlayer());
        //�����_���ȕ����̏�����
        //SetRandomDirection();
    }

    IEnumerator MoveToWardsRandomPlayer()
    {
        while(true)
        {
            //�����_���Ƀv���C���[��I��
            Transform targetPlayer = GetRandomPlayer();

            if(targetPlayer != null)
            {
                //�����_���ɑI�΂ꂽ�v���C���[�̋������v�Z
                Vector3 direction = (targetPlayer.position - transform.position).normalized;
                //�w�肵���X�s�[�h�Ńv���C���[�Ɉړ�
                rb.AddForce(direction * Force);
            }

            //ChangeTime�ҋ@�i2�b�ҋ@�j
            yield return new WaitForSeconds(ChangeTime);

        }
    }

    Transform GetRandomPlayer()
    {
        // �A�N�e�B�u�ȃv���C���[�����X�g�ɒǉ�
        List<Transform> activePlayers = new List<Transform>();
        foreach (Transform player in players)
        {
            if (player.gameObject.activeInHierarchy) // �v���C���[���A�N�e�B�u���m�F
            {
                activePlayers.Add(player);
            }
        }

        // �A�N�e�B�u�ȃv���C���[������΁A�����_����1�l��I��ŕԂ�
        if (activePlayers.Count > 0)
        {
            int randomIndex = Random.Range(0, activePlayers.Count);
            return activePlayers[randomIndex];
        }

        // �A�N�e�B�u�ȃv���C���[�����Ȃ��ꍇ��null��Ԃ�
        return null;
    }

    //void SetRandomDirection()
    //{
    //    //�����_����x����
    //    float x = Random.Range(-1.0f, 1.0f);
    //    //�����_����z����
    //    float z = Random.Range(-1.0f, 1.0f);

    //    randamDirection = new Vector3(x, 0.0f, z).normalized;
    //}

    // Update is called once per frame
    void Update()
    {
        ////���݂̈ʒu���擾
        //Vector3 position = transform.position;
        ////X���W�͈̔͂𐧌�
        //position.x = Mathf.Clamp(position.x, -7.8f, 7.8f);
        ////Y���W��0.5�ɌŒ�
        //position.y = 0.5f;
        ////Z���W�͈̔͂𐧌�
        //position.z = Mathf.Clamp(position.z, -6.8f, 8.8f);
        ////�������ꂽ�ʒu���I�u�W�F�N�g�ɓK��
        //transform.position = position;

        //rb.AddForce(randamDirection * Force);
        Timer += Time.deltaTime;

        if(Timer > ChangeTime)
        {
            //�V���������_���ȕ����ɕύX
            //SetRandomDirection();
            //�����_���ɑI�o�����v���C���[�Ɍ������Ă���
            MoveToWardsRandomPlayer();
            //�{�[���̋�����50f����������
            Force += 50.0f;
            //�{�[�����ǂ�ǂ�傫�����Ă����B
            transform.localScale += ScaleChange;
            //�^�C�}�[�����Z�b�g
            Timer = 0.0f;
        }

        Vector3 velocity = rb.velocity;
        velocity.y = 0.0f;
        rb.velocity=velocity;

        

    }
    private void OnCollisionEnter(Collision collision)
    {
        //�I�u�W�F�N�g���ǂɐڐG�������ɉ��������邩
        if (collision.gameObject.CompareTag("Wall"))
        {
            //WallHitSE�𗬂�
            AS.PlayOneShot(Sound1);
            //Debug.Log("HIt");
            //Vector3 reflectedVelocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
            //rb.velocity = reflectedVelocity;
            //rb.AddForce(reflectedVelocity);
        }
    }
}
