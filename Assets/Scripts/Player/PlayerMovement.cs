using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの移動やアニメーション、キャラの向きなど管理
/// </summary>
public class PlayerMovement : Movement
{
    private float moveX, moveY;

    private Camera mainCam;

    //マウスの位置と方向
    private Vector2 mousePos, direction;

    //プレーヤーの向きを変えるための一時的な格納変数
    private Vector3 tempScale;

    private Animator animator;

    private PlayerMagicSquareManager playerMagicSquareManager;

    private void Awake()
    {
        //カメラ直で取得
        mainCam = Camera.main;
        animator = GetComponent<Animator>();
        playerMagicSquareManager = GetComponent<PlayerMagicSquareManager>();
    }

    /// <summary>
    /// キー入力の受け取り
    /// どのマシンでも一律で呼ばれるUpdate！！
    /// </summary>
    private void FixedUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        PlayerTurning();
        CharacterMovement(moveX, moveY);
    }

    /// <summary>
    /// キャラが向くべき方向の関数
    /// </summary>
    private void PlayerTurning()
    {
        //スクリーン座標をゲーム座標にして取得
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //正規化させる。(1、1)　(-1，1)とかに変換
        direction = new Vector2(mousePos.x - transform.position.x,
                                mousePos.y - transform.position.y).normalized;

        PlayerAnimation(direction.x, direction.y);
    }

    /// <summary>
    /// アニメーションの変更
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void PlayerAnimation(float x, float y)
    {
        //.5の数を偶数に合わせる　10.5なら10に　11.5なら12に
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        //自身の向きの退避
        tempScale = transform.localScale;

        if (x > 0)
        {
            //絶対値
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else if (x < 0)
        {
            //絶対値を-にする
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        //向きの変更
        transform.localScale = tempScale;

        //アニメーションの設定上初期化させる
        x = Mathf.Abs(x);

        animator.SetFloat("FaceX", x);
        animator.SetFloat("FaceY", y);

        DirectionMagicSquare(x, y);
    }

    //プレイヤーの向いてる方向に合わせて渡す引き数を変え、魔法陣の切り替え関数を呼ぶ関数
    private void DirectionMagicSquare(float x, float y)
    {
        //Animatorから向きを確認
        //side
        if (x == 1f && y == 0f)
        {
            playerMagicSquareManager.Activate(0);
        }

        //up
        if (x == 0f && y == 1f)
        {
            playerMagicSquareManager.Activate(1);
        }

        //front
        if (x == 0f && y == -1f)
        {
            playerMagicSquareManager.Activate(2);
        }

        //side_up
        if (x == 1f && y == 1f)
        {
            playerMagicSquareManager.Activate(3);
        }

        //side_down
        if (x == 1f && y == -1f)
        {
            playerMagicSquareManager.Activate(4);
        }
    }
}
