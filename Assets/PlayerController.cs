using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //アニメーター用変数
    public Animator animator;
    private Vector3 direction;

    public float runForce = 7.5f;
    //public float jumpForce = 15.0f; // ジャンプの強さを調整するための変数
    private Rigidbody rb;
    private bool isGrounded; // キャラクターが地面にいるかどうかを判定する変数
    [SerializeField] private int padNo = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Rigitbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
        //animatorのコンポーネントの取得
        animator = GetComponent<Animator>();

    }

    //プレイヤーの移動
    void Update()
    {
        if(Gamepad.all.Count <= padNo)
        {
            return;
        }
        var pad = Gamepad.all[padNo];
        //スペースキーを押しているかつ、地面にいる場合ジャンプ
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
            // オブジェクトの前方を移動方向に向ける
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position += direction.normalized * runForce * Time.deltaTime;
            //lstickValue = transform.TransformDirection(lstickValue);

            Vector3 movement = new Vector3(horiz, 0.0f, vert);
            //rb.velocity = movement * runForce;
            // キャラクターが動いているかを確認し、Animatorのパラメーターを更新
            animator.SetBool("Run",true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
        }
       
       
    }

    //プレイヤーのジャンプ
    //private void Jump()
    //{
    //    //上方向に力を加えてジャンプ
    //    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //    isGrounded = false; //空中に居るので地面から離れた
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //プレイヤーが地面に接触した時にisGroundedをtrueにする。
        //if(collision.gameObject.CompareTag("Ground"))
        //{
        //    isGrounded = true;
        //}

        //プレイヤーがボールに接触した時に退場させる。
        if(collision.gameObject.CompareTag("Ball"))
        {
            gameObject.SetActive(false);
        }

    }
}
