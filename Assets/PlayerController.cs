using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //�A�j���[�^�[�p�ϐ�
    public Animator animator;
    private Vector3 direction;

    public float runForce = 7.5f;
    //public float jumpForce = 15.0f; // �W�����v�̋����𒲐����邽�߂̕ϐ�
    private Rigidbody rb;
    private bool isGrounded; // �L�����N�^�[���n�ʂɂ��邩�ǂ����𔻒肷��ϐ�
    [SerializeField] private int padNo = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Rigitbody�R���|�[�l���g���擾
        rb = GetComponent<Rigidbody>();
        //animator�̃R���|�[�l���g�̎擾
        animator = GetComponent<Animator>();

    }

    //�v���C���[�̈ړ�
    void Update()
    {
        if(Gamepad.all.Count <= padNo)
        {
            return;
        }
        var pad = Gamepad.all[padNo];
        //�X�y�[�X�L�[�������Ă��邩�A�n�ʂɂ���ꍇ�W�����v
        //if (pad.buttonSouth.isPressed && isGrounded)
        //{
        //    Jump();
        //}

        Vector2 lstickValue = pad.leftStick.ReadValue();
        float vert = lstickValue.y;
        float horiz = lstickValue.x;

        direction = new Vector3(horiz, 0, vert);

        if (lstickValue.magnitude > 0.1f)
        {
            // �I�u�W�F�N�g�̑O�����ړ������Ɍ�����
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position += direction.normalized * runForce * Time.deltaTime;
            //lstickValue = transform.TransformDirection(lstickValue);

            Vector3 movement = new Vector3(horiz, 0.0f, vert);
            //rb.velocity = movement * runForce;
            // �L�����N�^�[�������Ă��邩���m�F���AAnimator�̃p�����[�^�[���X�V
            animator.SetBool("Run",true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
        }
       
       
    }

    //�v���C���[�̃W�����v
    //private void Jump()
    //{
    //    //������ɗ͂������ăW�����v
    //    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //    isGrounded = false; //�󒆂ɋ���̂Œn�ʂ��痣�ꂽ
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //�v���C���[���n�ʂɐڐG��������isGrounded��true�ɂ���B
        //if(collision.gameObject.CompareTag("Ground"))
        //{
        //    isGrounded = true;
        //}

        //�v���C���[���{�[���ɐڐG�������ɑޏꂳ����B
        if(collision.gameObject.CompareTag("Ball"))
        {
            gameObject.SetActive(false);
        }

    }
}
