using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//役目
//弾の速度や攻撃力の管理、動く方向の設定や、弾の非表示などを行う
public class Magic : MonoBehaviour
{

    //やること
    //変数作成（RigidBody2Dの格納、移動スピード、消えるまでの時間
    //攻撃力、攻撃判定、削除判定、軌跡の有無、軌跡
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 2.5f, deactiveTimer = 3f;

    [SerializeField]
    private int damageAmount = 25;

    private bool damage;

    [SerializeField]//削除対象のObjかどうか：Pool用
    private bool destroyObj;

    [SerializeField]//軌跡があるのかの判定
    private bool getTrailRenderer;

    private TrailRenderer trail;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (getTrailRenderer)
        {
            trail = GetComponent<TrailRenderer>();
        }
    }

    /// <summary>
    /// ヒエラルキーから有効になった際に呼ばれる
    /// </summary>
    private void OnEnable()
    {
        //ダメージ判定を無くす
        damage = false;

        //発生して秒後に消す
        Invoke("DeactiveMagic", deactiveTimer);
    }

    /// <summary>
    /// 無効になった際に呼ばれる
    /// </summary>
    private void OnDisable()
    {
        //pool用に移動速度を0にする。poolは非表示にして使いまわす
        rb.velocity = Vector2.zero;
        if (getTrailRenderer)
        {
            trail.Clear();
        }
    }

  
    /// <summary>
    /// ある方向に移動させる 速度ベクトル
    /// velocityが1の場合、1秒間に1unitの速さ
    /// 非リアル挙動。リアル挙動はAddForce
    /// </summary>
    /// <param name="direction"></param>
    public void MoveDirection(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    /// <summary>
    /// オブジェクトを非表示にする
    /// </summary>
    void DeactiveMagic()
    {
        //チェックされてると削除させる
        if (destroyObj)
        {
            Destroy(gameObject);
        }
        //poolさせるので非表示
        else
        {
            gameObject.SetActive(false);
        }
    }

    
    /// <summary>
    /// 当たった瞬間に呼ばれる
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || 
            collision.CompareTag("Boss"))
        {
            rb.velocity = Vector2.zero;
            //時間関数のキャンセル
            CancelInvoke("DeactiveMagic");

            if (!damage)
            {
                damage = true;
                collision.GetComponent<Health>().TakeDamage(damageAmount);
            }

            DeactiveMagic();
        }

        //障害物に衝突
        if (collision.CompareTag("Blocking"))
        {
            rb.velocity = Vector2.zero;
            //時間関数のキャンセル
            CancelInvoke("DeactiveMagic");
            DeactiveMagic();
        }
    }

}
