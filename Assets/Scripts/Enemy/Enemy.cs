using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//追跡と攻撃
public class Enemy : Movement
{
    //Todo:テスト変数
    public bool Test;//プレーヤーを発見したかどうか

    //プレーヤー関連の情報
    private Transform player;
    private Vector3 playerLastPos, startPos, movementPos;

    //追いかけるスピード/回転の遅延
    [SerializeField]
    private float chaseSpeed = 0.8f, turningDelay = 1f;

    //プレイヤーの位置を最後に把握した時間と次に方向転換可能な時間
    private float lastFollowTime, turningTimeDelay = 1f;

    //一時的な値の保存：向き変更用
    private Vector3 tempScale;

    //攻撃判定
    private bool attacked;

    //クールダウン時間
    [SerializeField]
    private float damageCooldown = 1f;
    private float damageCooldownTimer;

    //攻撃力
    [SerializeField]
    private int damageAmount = 1;

    private Health enemyHealth;

    private Animator animator;

    private int Dell = 1;

    /// <summary>
    /// 初期化関連
    /// </summary>
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerLastPos = player.position;
        startPos = transform.position;
        lastFollowTime = Time.time;
        turningTimeDelay *= turningDelay;
        enemyHealth = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //自身が死亡状態か、プレイヤーのコンポーネントが見つからない場合
        if (!enemyHealth.IsAlive() || !player )
        {
            return;
        }

        MoveAnimation();//Updateなん？？
        TurnAround();
        ChaseingPlayer();
    }

    /// <summary>
    /// プレーヤーを追いかける
    /// 一定時間経過すると発見してまっすぐ進む
    /// </summary>
    private void ChaseingPlayer()
    {
        //プレイや－を発見してる時
        if (Test)
        {
            //攻撃していない
            if (!attacked)
            {
                //攻撃
                Chase();
            }
            else
            {
                //クールダウン経過していない
                if(Time.time < damageCooldownTimer)
                {
                    //初期位置に戻っていく
                    movementPos = startPos - transform.position;
                }
                else//クールダウン経過
                {
                    attacked = false;
                }
            }
           
        }
        else
        {
            //発見してない処場合は初期位置に移動
            movementPos = startPos - transform.position;
            if(Vector3.Distance(transform.position, startPos) < 0.1f)
            {
                //動かない
                movementPos = Vector3.zero;
            }
        }

        //移動させる
        CharacterMovement(movementPos.x, movementPos.y);
    }

    /// <summary>
    /// 発見時追いかける
    /// </summary>
    private void Chase()
    {
        //まっすぐ進んだ判定
        if (Time.time- lastFollowTime > turningTimeDelay)
        {
            playerLastPos = player.transform.position;
            lastFollowTime = Time.time;
        }

        //十分離れてる
        if (Vector3.Distance(transform.position, playerLastPos) > 0.15f) 
        {
            movementPos = (playerLastPos - transform.position).normalized * chaseSpeed;
        }
        else//十分近づいている
        {
            movementPos = Vector3.zero;
        }
    }

    /// <summary>
    /// 向き変更
    /// </summary>
    private void TurnAround()
    {
        tempScale = transform.localScale;
        //プレイヤーの探知
        if (Test)
        {
            if (player.position.x > transform.position.x)
            {
                tempScale.x = Mathf.Abs(tempScale.x);
            }

            if (player.position.x < transform.position.x)
            {
                tempScale.x = -Mathf.Abs(tempScale.x);
            }
        }
        else//探知できていない場合は最初の位置に戻る
        {
            if (startPos.x > transform.position.x)
            {
                tempScale.x = Mathf.Abs(tempScale.x);
            }

            if (startPos.x < transform.position.x)
            {
                tempScale.x = -Mathf.Abs(tempScale.x);
            }
        }

        //上記設定後に反映
        transform.localScale = tempScale;
    }

    /// <summary>
    /// 攻撃処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageCooldownTimer = Time.time + damageCooldown;

            attacked = true;

            collision.GetComponent<Health>().TakeDamage(damageAmount);
               
        }
    }

    /// <summary>
    /// 移動アニメーションを切り替える
    /// </summary>
    private void MoveAnimation()
    {
        //このキャラが動いてるかどうかの判定:Vector2からベクトルの大きさを取得
        if (GetMoveDelta().sqrMagnitude > 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        //これでもいい
        //animator.SetBool("Walk", GetMoveDelta().sqrMagnitude > 0);
    }

}
