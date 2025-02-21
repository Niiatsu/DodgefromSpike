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
    //プレイヤー4人のTranformを格納
    public Transform[] players;
    public AudioClip Sound1;
    AudioSource AS;

    //スケールが大きくなる速度
    public Vector3 ScaleChange = new Vector3(0.025f, 0.025f, 0.025f);
    //ボールが動く強さ
    public float Force = 1000.0f;
    //動く方向を変更する時間間隔
    public float ChangeTime = 2.0f;
    //ランダムな方向
    private Vector3 randamDirection;
    //タイマー
    private float Timer = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        //Rigitbodyの取得
        rb = GetComponent<Rigidbody>();
        //AudioSourceコンポーネントの取得
        AS = GetComponent<AudioSource>();
        //コルーチンを開始
        StartCoroutine(MoveToWardsRandomPlayer());
        //ランダムな方向の初期化
        //SetRandomDirection();
    }

    IEnumerator MoveToWardsRandomPlayer()
    {
        while(true)
        {
            //ランダムにプレイヤーを選ぶ
            Transform targetPlayer = GetRandomPlayer();

            if(targetPlayer != null)
            {
                //ランダムに選ばれたプレイヤーの距離を計算
                Vector3 direction = (targetPlayer.position - transform.position).normalized;
                //指定したスピードでプレイヤーに移動
                rb.AddForce(direction * Force);
            }

            //ChangeTime待機（2秒待機）
            yield return new WaitForSeconds(ChangeTime);

        }
    }

    Transform GetRandomPlayer()
    {
        // アクティブなプレイヤーをリストに追加
        List<Transform> activePlayers = new List<Transform>();
        foreach (Transform player in players)
        {
            if (player.gameObject.activeInHierarchy) // プレイヤーがアクティブか確認
            {
                activePlayers.Add(player);
            }
        }

        // アクティブなプレイヤーがいれば、ランダムに1人を選んで返す
        if (activePlayers.Count > 0)
        {
            int randomIndex = Random.Range(0, activePlayers.Count);
            return activePlayers[randomIndex];
        }

        // アクティブなプレイヤーがいない場合はnullを返す
        return null;
    }

    //void SetRandomDirection()
    //{
    //    //ランダムなx方向
    //    float x = Random.Range(-1.0f, 1.0f);
    //    //ランダムなz方向
    //    float z = Random.Range(-1.0f, 1.0f);

    //    randamDirection = new Vector3(x, 0.0f, z).normalized;
    //}

    // Update is called once per frame
    void Update()
    {
        ////現在の位置を取得
        //Vector3 position = transform.position;
        ////X座標の範囲を制限
        //position.x = Mathf.Clamp(position.x, -7.8f, 7.8f);
        ////Y座標は0.5に固定
        //position.y = 0.5f;
        ////Z座標の範囲を制限
        //position.z = Mathf.Clamp(position.z, -6.8f, 8.8f);
        ////制限された位置をオブジェクトに適応
        //transform.position = position;

        //rb.AddForce(randamDirection * Force);
        Timer += Time.deltaTime;

        if(Timer > ChangeTime)
        {
            //新しいランダムな方向に変更
            //SetRandomDirection();
            //ランダムに選出したプレイヤーに向かっていく
            MoveToWardsRandomPlayer();
            //ボールの強さを50fずつ強くする
            Force += 50.0f;
            //ボールをどんどん大きくしていく。
            transform.localScale += ScaleChange;
            //タイマーをリセット
            Timer = 0.0f;
        }

        Vector3 velocity = rb.velocity;
        velocity.y = 0.0f;
        rb.velocity=velocity;

        

    }
    private void OnCollisionEnter(Collision collision)
    {
        //オブジェクトが壁に接触した時に何をさせるか
        if (collision.gameObject.CompareTag("Wall"))
        {
            //WallHitSEを流す
            AS.PlayOneShot(Sound1);
            //Debug.Log("HIt");
            //Vector3 reflectedVelocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
            //rb.velocity = reflectedVelocity;
            //rb.AddForce(reflectedVelocity);
        }
    }
}
